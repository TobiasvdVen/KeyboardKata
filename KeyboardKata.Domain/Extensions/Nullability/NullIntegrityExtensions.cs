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

            foreach (MemberInfo member in members)
            {
                object? value;

                switch (member)
                {
                    case PropertyInfo property:
                        if (property.GetGetMethod()?.GetParameters().Length > 0)
                        {
                            continue;
                        }

                        value = property.GetValue(target);
                        break;

                    case FieldInfo field:
                        value = field.GetValue(target);

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
                    if (!member.IsNullableMember())
                    {
                        string pathToProperty = string.Join(".", path.Reverse());
                        errorSummaryBuilder.AppendLine($"Property at path {pathToProperty} was null!");
                        break;
                    }
                }
                else
                {
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
