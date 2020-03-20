using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dapper.Core.Attributes;
using Dapper.Core.Enums;

namespace Dapper.Core.Extensions
{
    public static class MapperExtension
    {
        public static IEnumerable<PropertyInfo> GetTypeProperties(Type type)
        {
            var referenceProperties = type.GetProperties().Where(property => property.GetCustomAttributes(true)
                .OfType<Reference>().Any(att => att.CascadeType == CascadeType.CascadeNone));
            var ignoredProperties = type.GetProperties().Where(property => property.GetCustomAttributes(true)
                .OfType<Ignore>().Any());

            var properties = type.GetProperties().Where(p => p.GetCustomAttributes(true).Any(a => a.GetType() != typeof(Reference)) 
                || !p.GetCustomAttributes(true).Any()).ToArray();

            return properties.Except(referenceProperties).Except(ignoredProperties).ToList();
        }

        public static IEnumerable<PropertyInfo> GetKeyPropertiesCache(Type type)
        {
            var allProperties = GetTypeProperties(type).ToList();
            var keyProperties = allProperties.Where(p => p.GetCustomAttributes(true).Any(a => a is Key)).ToList();

            if (keyProperties.Count != 0) return keyProperties;
            var idProp = allProperties.Find(p => string.Equals(p.Name, "id", StringComparison.CurrentCultureIgnoreCase));
            if (idProp != null)
                keyProperties.Add(idProp);

            return keyProperties;
        }
    }
}