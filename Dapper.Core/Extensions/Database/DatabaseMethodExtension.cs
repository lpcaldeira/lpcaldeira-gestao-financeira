using System;
using System.Linq;
using Dapper.Core.Attributes;
using Dapper.Core.Contexts.Database.Tables;

namespace Dapper.Core.Extensions.Database
{
    public static class DatabaseMethodExtension
    {
        public static bool FieldIsKey(this object[] atributes)
        {
            return atributes.OfType<Key>().Any();
        }

        public static bool FieldIsReferenceKey(this object[] attributes)
        {
            return attributes.OfType<ReferenceKey>().Any();
        }

        public static bool FieldIsNotNull(this object[] attributes)
        {
            return attributes.OfType<IsNotNull>().Any();
        }

        public static TableAttribute TableAttribute(this object[] attributes)
        {
            return attributes.OfType<TableAttribute>().FirstOrDefault();
        }

        public static Table Table(this object[] attributes)
        {
            return attributes.OfType<Table>().FirstOrDefault();
        }

        public static ReferenceKey FieldReferenceKey(this object[] attributes)
        {
            return attributes.OfType<ReferenceKey>().FirstOrDefault();
        }

        public static Precision FieldPrecision(this object[] attributes)
        {
            return attributes.OfType<Precision>().FirstOrDefault();
        }

        public static Length FieldLength(this object[] attributes)
        {
            return attributes.OfType<Length>().FirstOrDefault();
        }

        public static string FieldKeyName(this Type type)
        {
            return type.GetProperties().FirstOrDefault(property =>
                property.GetCustomAttributes(true).OfType<Key>().Any())?.Name;
        }
    }
}