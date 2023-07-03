using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace KeyboardKata.Domain.Extensions.Nullability
{
    internal abstract class NullIntegrityTester
    {
        public abstract string ErrorSummary { get; }
        public abstract bool TestIntegrity();
    }

    internal class NullIntegrityTester<T> : NullIntegrityTester where T : notnull
    {
        private readonly T _target;
        private readonly StringBuilder _errorSummaryBuilder;
        private readonly Stack<string> _path;

        public NullIntegrityTester(T target)
            : this(target, new StringBuilder(), new Stack<string>())
        {

        }

        public NullIntegrityTester(T target, StringBuilder errorSummaryBuilder, Stack<string> path)
        {
            _target = target;
            _errorSummaryBuilder = errorSummaryBuilder;
            _path = path;
        }

        public override string ErrorSummary => _errorSummaryBuilder.ToString();

        public override bool TestIntegrity()
        {
            Type type = _target.GetType();

            if (type.IsValueType)
            {
                return true;
            }

            MemberInfo[] members = type.GetMembers();

            foreach (MemberInfo member in members)
            {
                if (MemberHasValue(member, out object? value, out NullabilityInfo? info))
                {
                    _path.Push($".{member.Name}");

                    if (!TestMemberIntegrity(value, info))
                    {
                        return false;
                    }

                    _path.Pop();
                }
            }

            return _errorSummaryBuilder.Length == 0;
        }

        private bool MemberHasValue(MemberInfo member, out object? value, [NotNullWhen(true)] out NullabilityInfo? info)
        {
            NullabilityInfoContext context = new();

            value = null;
            info = null;

            switch (member)
            {
                case PropertyInfo property:
                    if (property.GetGetMethod()?.GetParameters().Length > 0)
                    {
                        return false;
                    }

                    value = property.GetValue(_target);
                    info = context.Create(property);

                    return true;

                case FieldInfo field:
                    value = field.GetValue(_target);
                    info = context.Create(field);

                    return !field.IsStatic;
            }

            return false;
        }

        private bool TestMemberIntegrity(object? value, NullabilityInfo info)
        {
            if (value is null)
            {
                if (info.ReadState != NullabilityState.Nullable)
                {
                    string pathToProperty = string.Concat(_path.Reverse());
                    _errorSummaryBuilder.AppendLine($"Property at path {pathToProperty} was null!");

                    return false;
                }
            }
            else
            {
                if (value is IEnumerable enumerable)
                {
                    if (enumerable.GetType().GenericTypeArguments.Length > 0)
                    {
                        NullabilityInfo genericInfo = info.GenericTypeArguments[0];

                        IEnumerator enumerator = enumerable.GetEnumerator();

                        bool elementIntegrity = true;
                        int count = 0;

                        while (enumerator.MoveNext())
                        {
                            _path.Push($"[{count}]");

                            if (enumerator.Current is null && genericInfo.ReadState != NullabilityState.Nullable)
                            {
                                string pathToProperty = string.Concat(_path.Reverse());

                                _errorSummaryBuilder.AppendLine($"Element at path {pathToProperty} was null!");
                            }

                            if (enumerator.Current is not null)
                            {
                                NullIntegrityTester elementTester = NullIntegrityTesterFactory.FromTarget(enumerator.Current, _errorSummaryBuilder, _path);
                                elementIntegrity |= elementTester.TestIntegrity();
                            }

                            _path.Pop();

                            count++;

                        }

                        if (!elementIntegrity)
                        {
                            return false;
                        }
                    }
                }

                NullIntegrityTester nestedTester = NullIntegrityTesterFactory.FromTarget(value, _errorSummaryBuilder, _path);

                return nestedTester.TestIntegrity();
            }

            return true;
        }
    }
}
