using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Core.Extensions.Database;

namespace Dapper.Core.Extensions.Insert
{
    public static class InsertExtension
    {
        public static void InsertOne<TAdapter, T>(this IDbConnection connection, T obj) where TAdapter : IDatabaseAdapter
        {
            var entities = obj.GetAllEntities().ToList();
            foreach (var entity in entities)
            {
                try
                {
                    var keyId = connection.Insert<TAdapter>(entity, entity.GetType());
                    entity.UpdateEntities(keyId);
                    entities.UpdateKeyReferences(entity.GetType(), keyId);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static void InsertMany<TAdapter, T>(this IDbConnection connection, IEnumerable<T> objs)
            where TAdapter : IDatabaseAdapter
            where T : class
        {
            foreach (var obj in objs)
                InsertOne<TAdapter, T>(connection, obj);
        }

        public static async Task InsertOneAsync<TAdapter, T>(this IDbConnection connection, T obj) where TAdapter : IDatabaseAdapter
        {
            await Task.Factory.StartNew(() => connection.InsertOne<TAdapter, T>(obj));
        }

        public static async Task InsertManyAsync<TAdapter, T>(this IDbConnection connection, IEnumerable<T> objs)
            where TAdapter : IDatabaseAdapter
            where T : class
        {
            foreach (var obj in objs)
                await InsertOneAsync<TAdapter, T>(connection, obj);
        }
    }
}