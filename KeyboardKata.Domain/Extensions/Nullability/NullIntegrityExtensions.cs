using System.Reflection;

namespace KeyboardKata.Domain.Extensions.Nullability
{
    public static class NullIntegrityExtensions
    {
        public static bool HasNullIntegrity<T>(this T target) where T : notnull
        {
            Type type = target.GetType();

            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object? value = property.GetValue(target);

                if (value is null)
                {
                    if (!property.IsNullable())
                    {
                        return false;
                    }
                }
                else
                {
                    bool nestedIntegrity = value.HasNullIntegrity();

                    if (!nestedIntegrity)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
