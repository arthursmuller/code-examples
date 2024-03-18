using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Infraestrutura.Utils
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T value) where T : System.Enum
        {
            Type type = value.GetType();
            string name = System.Enum.GetName(type, value);

            if (name != null)
            {
                FieldInfo field = type.GetField(name);

                if (field != null)
                {
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                    return attr?.Description ?? null;
                }
            }
            return null;
        }

        public static T GetEnumByDescription<T>(this string value) where T : System.Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == value)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == value)
                        return (T)field.GetValue(null);
                }
            }

            return default(T);
        }

        public static IEnumerable<T> ValoresEnum<T>() where T : System.Enum
        {
            return System.Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}