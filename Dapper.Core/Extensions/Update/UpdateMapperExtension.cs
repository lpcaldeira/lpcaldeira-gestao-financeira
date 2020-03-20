using System;
using System.Data;
using System.Linq;
using System.Text;
using Dapper.Core.Attributes;
using Dapper.Core.Extensions.Database;

namespace Dapper.Core.Extensions.Update
{
    public static class UpdateMapperExtension
    {
        public static bool Update<TAdapter>(this IDbConnection connection, object entityToUpdate, Type type, IDbTransaction transaction = null, int? commandTimeout = null)
            where TAdapter : IDatabaseAdapter
        {
            if (type.IsArray)
                type = type.GetElementType();
            else if (type.IsGenericType)
                type = type.GetGenericArguments()[0];

            var keyProperties = MapperExtension.GetKeyPropertiesCache(type).ToList();
            if (keyProperties.Count == 0)
                throw new ArgumentException("Entity must have at least one [Key]");

            var tableAttr = type.GetCustomAttributes(false)
                .SingleOrDefault(attr => attr.GetType() == typeof(TableAttribute)) as dynamic;

            string tableName = tableAttr?.Name;
            if (string.IsNullOrEmpty(tableName))
                throw new Exception("Table atribute not found");

            var sb = new StringBuilder();
            sb.AppendFormat("update {0} set ", tableName);

            var adapter = Activator.CreateInstance<TAdapter>() as IDatabaseAdapter;
            var allProperties = MapperExtension.GetTypeProperties(type);
            var nonIdProps = allProperties.Except(keyProperties).ToList();

            for (var i = 0; i < nonIdProps.Count; i++)
            {
                var property = nonIdProps[i];
                adapter.AppendColumnNameEqualsValue(sb, property.Name.ToLower());
                if (i < nonIdProps.Count - 1)
                    sb.AppendFormat(", ");
            }

            sb.Append(" where ");
            for (var i = 0; i < keyProperties.Count; i++)
            {
                var property = keyProperties[i];
                adapter.AppendColumnNameEqualsValue(sb, property.Name.ToLower());
                if (i < keyProperties.Count - 1)
                    sb.AppendFormat(" and ");
            }

            var updated = connection.Execute(sb.ToString(), entityToUpdate, commandTimeout: commandTimeout, transaction: transaction);
            return updated > 0;
        }
    }
}