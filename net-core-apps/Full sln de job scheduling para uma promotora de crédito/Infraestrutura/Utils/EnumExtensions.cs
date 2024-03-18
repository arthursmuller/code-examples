using System;
using System.ComponentModel;
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
    }
}