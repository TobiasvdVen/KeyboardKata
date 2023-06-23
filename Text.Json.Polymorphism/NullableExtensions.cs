using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Text.Json.Polymorphism
{
    internal static class NullableExtensions
    {
        private const byte nullOblivious = 0;
        private const byte notNullable = 1;
        private const byte nullable = 2;

        internal static bool IsProperty(this MemberInfo memberInfo)
        {
            return memberInfo is PropertyInfo;
        }

        internal static bool IsField(this MemberInfo memberInfo)
        {
            return memberInfo is FieldInfo;
        }

        internal static bool IsPropertyOrField(this MemberInfo memberInfo)
        {
            return memberInfo.IsProperty() || memberInfo.IsField();
        }

        internal static bool IsNullable(this PropertyInfo propertyInfo)
        {
            Type type = propertyInfo.PropertyType;

            if (type.IsValueType)
            {
                return type.IsNullableValueType();
            }

            return propertyInfo.IsNullableMember();
        }

        internal static bool IsNullable(this FieldInfo fieldInfo)
        {
            Type type = fieldInfo.FieldType;

            if (type.IsValueType)
            {
                return type.IsNullableValueType();
            }

            return fieldInfo.IsNullableMember();
        }

        internal static bool IsNullableMember(this MemberInfo memberInfo)
        {
            if (!memberInfo.IsPropertyOrField())
            {
                return false;
            }

            byte? nullable = GetNullableAttributeByte(memberInfo.CustomAttributes);

            if (nullable == notNullable)
            {
                return false;
            }

            if (nullable == nullOblivious || nullable == NullableExtensions.nullable)
            {
                return true;
            }

            if (memberInfo.DeclaringType != null)
            {
                byte? declaringTypeNullable = GetNullableContextAttributeByte(memberInfo.DeclaringType.CustomAttributes);

                if (declaringTypeNullable == notNullable)
                {
                    return false;
                }

                if (declaringTypeNullable == nullOblivious || declaringTypeNullable == NullableExtensions.nullable)
                {
                    return true;
                }

                if (memberInfo.DeclaringType.DeclaringType != null)
                {
                    byte? secondDeclaringTypeNullable = GetNullableContextAttributeByte(memberInfo.DeclaringType.DeclaringType.CustomAttributes);

                    if (secondDeclaringTypeNullable == notNullable)
                    {
                        return false;
                    }

                    if (secondDeclaringTypeNullable == nullOblivious || secondDeclaringTypeNullable == NullableExtensions.nullable)
                    {
                        return true;
                    }
                }

                byte? declaringInfoNullable = GetNullableContextAttributeByte(memberInfo.DeclaringType.GetTypeInfo().CustomAttributes);

                if (declaringInfoNullable == notNullable)
                {
                    return false;
                }

                if (declaringInfoNullable == nullOblivious || declaringInfoNullable == NullableExtensions.nullable)
                {
                    return true;
                }
            }

            return true;
        }

        internal static bool IsNullable(this ParameterInfo parameterInfo)
        {
            byte? nullable = GetNullableAttributeByte(parameterInfo.CustomAttributes);

            if (nullable == notNullable)
            {
                return false;
            }

            if (nullable == nullOblivious || nullable == NullableExtensions.nullable)
            {
                return true;
            }

            byte? memberNullable = GetNullableContextAttributeByte(parameterInfo.Member.CustomAttributes);

            if (memberNullable == notNullable)
            {
                return false;
            }

            if (memberNullable == nullOblivious || memberNullable == NullableExtensions.nullable)
            {
                return true;
            }

            if (parameterInfo.Member.DeclaringType != null)
            {
                byte? declaringTypeNullable = GetNullableContextAttributeByte(parameterInfo.Member.DeclaringType.CustomAttributes);

                if (declaringTypeNullable == notNullable)
                {
                    return false;
                }

                if (declaringTypeNullable == nullOblivious || declaringTypeNullable == NullableExtensions.nullable)
                {
                    return true;
                }

                if (parameterInfo.Member.DeclaringType.DeclaringType != null)
                {
                    byte? secondDeclaringTypeNullable = GetNullableContextAttributeByte(parameterInfo.Member.DeclaringType.DeclaringType.CustomAttributes);

                    if (secondDeclaringTypeNullable == notNullable)
                    {
                        return false;
                    }

                    if (secondDeclaringTypeNullable == nullOblivious || secondDeclaringTypeNullable == NullableExtensions.nullable)
                    {
                        return true;
                    }
                }

                byte? declaringInfoNullable = GetNullableContextAttributeByte(parameterInfo.Member.DeclaringType.GetTypeInfo().CustomAttributes);

                if (declaringInfoNullable == notNullable)
                {
                    return false;
                }

                if (declaringInfoNullable == nullOblivious || declaringInfoNullable == NullableExtensions.nullable)
                {
                    return true;
                }
            }

            return true;
        }

        private static byte? GetNullableAttributeByte(IEnumerable<CustomAttributeData> customAttributes)
        {
            CustomAttributeData? attribute = customAttributes
                .OfType<CustomAttributeData>()
                .FirstOrDefault(c => c.AttributeType.FullName == "System.Runtime.CompilerServices.NullableAttribute");

            if (attribute != null)
            {
                CustomAttributeTypedArgument contructorArguments = attribute.ConstructorArguments.FirstOrDefault();

                if (contructorArguments.ArgumentType == typeof(byte))
                {
                    return (byte?)contructorArguments.Value;
                }
            }

            return null;
        }

        private static byte? GetNullableContextAttributeByte(IEnumerable<CustomAttributeData> customAttributes)
        {
            CustomAttributeData? attribute = customAttributes
                .OfType<CustomAttributeData>()
                .FirstOrDefault(c => c.AttributeType.FullName == "System.Runtime.CompilerServices.NullableContextAttribute");

            if (attribute != null)
            {
                CustomAttributeTypedArgument contructorArguments = attribute.ConstructorArguments.FirstOrDefault();

                if (contructorArguments.ArgumentType == typeof(byte))
                {
                    return (byte?)contructorArguments.Value;
                }
            }

            return null;
        }

        private static bool IsNullableValueType(this Type type)
        {
            if (!type.IsValueType)
            {
                return false;
            }

            if (!type.IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
