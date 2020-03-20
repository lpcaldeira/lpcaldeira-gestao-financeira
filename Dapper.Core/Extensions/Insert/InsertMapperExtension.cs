using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper.Core.Attributes;
using Dapper.Core.Extensions.Database;

namespace Dapper.Core.Extensions.Insert
{
    public static class InsertMapperExtension
    {
        public static int Insert<TAdapter>(this IDbConnection connection, object entity, Type type, IDbTransaction transaction = null, int? commandTimeout = null) 
            where TAdapter : IDatabaseAdapter
        {
            var isList = false;

            if (type.IsArray)
            {
                isList = true;
                type = type.GetElementType();
            }
            else if (type.IsGenericType && type.GetTypeInfo().ImplementedInterfaces.Any(ti =>
                         ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
            {
                isList = true;
                type = type.GetGenericArguments()[0];
            }

            var tableAttr = type.GetCustomAttributes(false)
                .SingleOrDefault(attr => attr.GetType() == typeof(TableAttribute)) as dynamic;
            var tableName = tableAttr?.Name;

            var adapter = Activator.CreateInstance<TAdapter>() as IDatabaseAdapter;
            var sbColumnList = new StringBuilder(null);
            var allProperties = MapperExtension.GetTypeProperties(type);
            var keyProperties = MapperExtension.GetKeyPropertiesCache(type).ToList();
            var allPropertiesExceptKeyAndComputed = allProperties.Except(keyProperties).ToList();

            for (var i = 0; i < allPropertiesExceptKeyAndComputed.Count; i++)
            {
                var property = allPropertiesExceptKeyAndComputed[i];
                adapter.AppendColumnName(sbColumnList, property.Name.ToLower());
                if (i < allPropertiesExceptKeyAndComputed.Count - 1)
                    sbColumnList.Append(", ");
            }

            var sbParameterList = new StringBuilder(null);
            for (var i = 0; i < allPropertiesExceptKeyAndComputed.Count; i++)
            {
                var property = allPropertiesExceptKeyAndComputed[i];
                sbParameterList.AppendFormat("@{0}", property.Name.ToLower());
                if (i < allPropertiesExceptKeyAndComputed.Count - 1)
                    sbParameterList.Append(", ");
            }

            int returnVal;
            var wasClosed = connection.State == ConnectionState.Closed;
            if (wasClosed) connection.Open();

            if (!isList)  
            {
                returnVal = adapter.Insert(connection, transaction, commandTimeout, tableName, sbColumnList.ToString(),
                    sbParameterList.ToString(), keyProperties, entity);
            }
            else
            {
                var cmd = $"insert into {tableName} ({sbColumnList}) values ({sbParameterList})";
                returnVal = connection.Execute(cmd, entity, transaction, commandTimeout);
            }
            if (wasClosed) connection.Close();
            return returnVal;
        }
    }
}