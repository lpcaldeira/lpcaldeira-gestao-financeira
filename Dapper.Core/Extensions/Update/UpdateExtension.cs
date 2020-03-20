using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper.Core.Extensions.Database;

namespace Dapper.Core.Extensions.Update
{
    public static class UpdateExtension
    {
        public static void UpdateOne<TAdapter, T>(this IDbConnection connection, T obj) where TAdapter : IDatabaseAdapter
        {
            var entities = obj.GetAllEntities();
            foreach (var entity in entities)
            {
                try
                {
                    connection.Update<TAdapter>(entity, entity.GetType());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static void UpdateMany<TAdapter, T>(this IDbConnection connection, IEnumerable<T> objs)
            where TAdapter : IDatabaseAdapter
            where T : class
        {
            foreach (var obj in objs)
                UpdateOne<TAdapter, T>(connection, obj);
        }

        public static async Task UpdateOneAsync<TAdapter, T>(this IDbConnection connection, T obj) where TAdapter : IDatabaseAdapter
        {
            await Task.Factory.StartNew(() => connection.UpdateOne<TAdapter, T>(obj));
        }

        public static async Task UpdateManyAsync<TAdapter, T>(this IDbConnection connection, IEnumerable<T> objs)
            where TAdapter : IDatabaseAdapter
            where T : class
        {
            foreach (var obj in objs)
                await UpdateOneAsync<TAdapter, T>(connection, obj);
        }
    }
}