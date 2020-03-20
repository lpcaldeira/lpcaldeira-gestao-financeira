using System;
using System.Data;
using System.Linq;
using System.Text;
using Dapper.Core.Attributes;
using Dapper.Core.Extensions.Database;

namespace Dapper.Core.Extensions.Delete
{
    public static class DeleteMapperExtension
    {
        public static bool Delete<TAdapter>(this IDbConnection connection, object entityToDelete, Type type, IDbTransaction transaction = null, int? commandTimeout = null)
            where TAdapter : IDatabaseAdapter
        {
            if (entityToDelete == null)
                throw new ArgumentException("Cannot Delete null Object", nameof(entityToDelete));

            if (type.IsArray)
                type = type.GetElementType();
            else if (type.IsGenericType)
                type = type.GetGenericArguments()[0];

            var keyProperties = MapperExtension.GetKeyPropertiesCache(type).ToList();
            if (keyProperties.Count == 0)
                throw new ArgumentException("Entity must have at least one [Key]");

            var tableAttr = type.GetCustomAttributes(false)
                .SingleOrDefault(attr => attr.GetType() == typeof(TableAttribute)) as dynamic;
            var tableName = tableAttr?.Name;
            if (string.IsNullOrEmpty(tableName))
                throw new Exception("Table atribute not found");

            var sb = new StringBuilder();
            sb.AppendFormat("delete from {0} where ", tableName);

            var adapter = Activator.CreateInstance<TAdapter>() as IDatabaseAdapter;

            for (var i = 0; i < keyProperties.Count; i++)
            {
                var property = keyProperties[i];
                adapter.AppendColumnNameEqualsValue(sb, property.Name.ToLower()); 
                if (i < keyProperties.Count - 1)
                    sb.AppendFormat(" and ");
            }
            var deleted = connection.Execute(sb.ToString(), entityToDelete, transaction, commandTimeout);
            return deleted > 0;
        }

        public static bool DeleteAll(this IDbConnection connection, Type type, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            string name;
            var tableAttr = type.GetCustomAttributes(false)
                .SingleOrDefault(attr => attr.GetType() == typeof(TableAttribute)) as dynamic;

            if (tableAttr != null)
            {
                name = tableAttr.Name;
            }
            else
            {
                name = type.Name + "s";
                if (type.IsInterface && name.StartsWith("I"))
                    name = name.Substring(1);
            }
            
            var statement = $"delete from {name}";
            var deleted = connection.Execute(statement, null, transaction, commandTimeout);
            return deleted > 0;
        }
    }
}