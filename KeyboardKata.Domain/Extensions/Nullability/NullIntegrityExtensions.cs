using System.Collections;
using System.Reflection;
using System.Text;

namespace KeyboardKata.Domain.Extensions.Nullability
{
    public static class NullIntegrityExtensions
    {
        public static bool HasNullIntegrity<T>(this T target) where T : notnull
        {
            return target.HasNullIntegrity(out string _);
        }

        public static bool HasNullIntegrity<T>(this T target, out string errorSummary) where T : notnull
        {
            StringBuilder errorSummaryBuilder = new();

            bool result = target.HasNullIntegrity(errorSummaryBuilder, new Stack<string>());

            errorSummary = errorSummaryBuilder.ToString();

            return result;
        }

        public static bool HasNullIntegrity<T>(this T target, StringBuilder errorSummaryBuilder, Stack<string> path) where T : notnull
        {
            Type type = target.GetType();

            if (type.IsValueType)
            {
                return true;
            }

            MemberInfo[] members = type.GetMembers();

            NullabilityInfoContext context = new();

            foreach (MemberInfo member in members)
            {
                object? value;
                NullabilityInfo info;

                switch (member)
                {
                    case PropertyInfo property:
                        if (property.GetGetMethod()?.GetParameters().Length > 0)
                        {
                            continue;
                        }

                        value = property.GetValue(target);
                        info = context.Create(property);

                        break;

                    case FieldInfo field:
                        value = field.GetValue(target);
                        info = context.Create(field);

                        if (field.IsStatic)
                        {
                            continue;
                        }

                        break;

                    default:
                        continue;
                }

                path.Push(member.Name);

                if (value is null)
                {
                    if (info.ReadState != NullabilityState.Nullable)
                    {
                        string pathToProperty = string.Join(".", path.Reverse());
                        errorSummaryBuilder.AppendLine($"Property at path {pathToProperty} was null!");
                        break;
                    }
                }
                else
                {
                    if (value is IEnumerable enumerable)
                    {
                        if (enumerable.GetType().GenericTypeArguments.Length > 0)
                        {
                            NullabilityInfo genericInfo = info.GenericTypeArguments[0];

                            if (genericInfo.ReadState != NullabilityState.Nullable)
                            {
                                IEnumerator enumerator = enumerable.GetEnumerator();

                                bool elementIntegrity = true;
                                int count = 0;

                                while (enumerator.MoveNext())
                                {
                                    if (enumerator.Current is null)
                                    {
                                        string pathToProperty = string.Join(".", path.Reverse());

                                        errorSummaryBuilder.AppendLine($"Element at path {pathToProperty}[{count}] was null!");
                                    }

                                    count++;
                                }

                                if (!elementIntegrity)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    bool nestedIntegrity = value.HasNullIntegrity(errorSummaryBuilder, path);

                    if (!nestedIntegrity)
                    {
                        break;
                    }
                }
            }

            if (members.Any())
            {
                path.Pop();
            }

            return errorSummaryBuilder.Length == 0;
        }
    }
}
