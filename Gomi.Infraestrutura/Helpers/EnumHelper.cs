using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Gomi.Infraestrutura.Helpers
{
    public static class EnumHelper
    {
        public static string Description(this Enum value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var description = value.ToString();
            var fieldInfo = value.GetType().GetField(description);
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
                description = attributes[0].Description;

            return description;
        }

        public static IList<KeyValuePair<Enum, string>> ToList<T>() where T : struct
        {
            var type = typeof(T);

            if (!type.IsEnum)
                throw new ArgumentException("T must be an enum");

            return Enum.GetValues(type)
                .OfType<Enum>()
                .Select(e => new KeyValuePair<Enum, string>(e, e.Description()))
                .ToArray();
        }

        public static T GetValueFromDescription<T>(string description) where T : struct
        {
            var type = typeof(T);

            if (!type.IsEnum)
                throw new ArgumentException("T must be an enum");

            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentOutOfRangeException(nameof(description));
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}