using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper.Core.Extensions.Database;

namespace Dapper.Core.Extensions.Delete
{
    public static class DeleteExtension
    {
        public static void DeleteOne<TAdapter, T>(this IDbConnection connection, T obj) where TAdapter : IDatabaseAdapter
        {
            var entities = obj.GetAllEntities();
            foreach (var entity in entities)
            {
                try
                {
                    connection.Delete<TAdapter>(entity, entity.GetType());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static void DeleteMany<TAdapter, T>(this IDbConnection connection, IEnumerable<T> objs)
            where TAdapter : IDatabaseAdapter
            where T : class
        {
            foreach (var obj in objs)
                DeleteOne<TAdapter, T>(connection, obj);
        }

        public static async Task DeleteOneAsync<TAdapter, T>(this IDbConnection connection, T obj) where TAdapter : IDatabaseAdapter
        {
            await Task.Factory.StartNew(() => connection.DeleteOne<TAdapter, T>(obj));
        }

        public static async Task DeleteManyAsync<TAdapter, T>(this IDbConnection connection, IEnumerable<T> objs)
            where TAdapter : IDatabaseAdapter
            where T : class
        {
            foreach (var obj in objs)
                await DeleteOneAsync<TAdapter, T>(connection, obj);
        }

        public static void DeleteAll<T>(this IDbConnection connection)
        {
            connection.DeleteAll(typeof(T));
        }
    }
}