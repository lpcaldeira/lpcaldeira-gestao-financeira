using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Core.Extensions.Select.Dapper.Mapper;

namespace Dapper.Core.Extensions.Select
{
    public static class SelectExtension
    {
        public static async Task<T> GetOneById<T>(this IDbConnection connection, int id) where T : class
        {
            var key = SelectMethodExtension.GetSingleKey<T>(nameof(GetOneById));
            var tableName = SelectMethodExtension.GetTableName(typeof(T));
            var sql = $"select * from {tableName} where {key.Name} = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            var result = new List<object[]>();

            var entity = await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            if (entity == null) return null;

            var entityResult = new List<object> { entity };
            await SetReferences(connection, entity, entityResult);
            result.Add(entityResult.ToArray());

            return SelectMethodExtension.MapAllEntities<T>(result).FirstOrDefault();
        }

        private static async Task SetReferences(IDbConnection connection, object entity, List<object> entityResult)
        {
            var properties = entity.GetType().GetProperties();
            var referenceTypes = new List<Type>();
            SelectMethodExtension.GetReferenceProperties(entity.GetType(), ref referenceTypes);
            var references = properties.Where(x => referenceTypes.Any(t => t == x.PropertyType));

            foreach (var reference in references)
            {
                var tableReferenceName = SelectMethodExtension.GetTableName(reference.PropertyType);
                var referenceKey = properties.FirstOrDefault(x => x.Name.Equals($"Id{reference.Name}"));
                var idReference = referenceKey.GetValue(entity) ?? 0;

                if (entityResult.Any(x =>
                {
                    var valor = (int)x.GetType().GetProperty("Id").GetValue(x);
                    return x.GetType() == reference.PropertyType
                           &&  valor.Equals((int)idReference);
                })) continue;

                var sqlReferenceTable = $"select * from {tableReferenceName} where id = {idReference}";
                var referenceValue = await connection.QueryAsync(sqlReferenceTable, new[] {reference.PropertyType}, obj => obj);
                if (referenceValue == null || !referenceValue.Any()) continue;
                var restult = referenceValue.Select(x => x.FirstOrDefault()).FirstOrDefault();
                await SetReferences(connection, restult, entityResult);
                entityResult.Add(restult);
            }
        }

        public static IEnumerable<TFirst> Query<TFirst, TSecond>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.Query<TFirst, TSecond, TFirst>(cnn, sql, MappingCache<TFirst, TSecond>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static IEnumerable<TFirst> Query<TFirst, TSecond, TThird>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.Query<TFirst, TSecond, TThird, TFirst>(cnn, sql, MappingCache<TFirst, TSecond, TThird>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static IEnumerable<TFirst> Query<TFirst, TSecond, TThird, TFourth>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFirst>(cnn, sql, MappingCache<TFirst, TSecond, TThird, TFourth>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static IEnumerable<TFirst> Query<TFirst, TSecond, TThird, TFourth, TFifth>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFifth, TFirst>(cnn, sql, MappingCache<TFirst, TSecond, TThird, TFourth, TFifth>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static IEnumerable<TFirst> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TFirst>(cnn, sql, MappingCache<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static IEnumerable<TFirst> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TFirst>(cnn, sql, MappingCache<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static async Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await SqlMapper.QueryAsync<TFirst, TSecond, TFirst>(cnn, sql, MappingCache<TFirst, TSecond>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static async Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await SqlMapper.QueryAsync<TFirst, TSecond, TThird, TFirst>(cnn, sql, MappingCache<TFirst, TSecond, TThird>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static async Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird, TFourth>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await SqlMapper.QueryAsync<TFirst, TSecond, TThird, TFourth, TFirst>(cnn, sql, MappingCache<TFirst, TSecond, TThird, TFourth>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static async Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await SqlMapper.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TFirst>(cnn, sql, MappingCache<TFirst, TSecond, TThird, TFourth, TFifth>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static async Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await SqlMapper.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TFirst>(cnn, sql, MappingCache<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static async Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await SqlMapper.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TFirst>(cnn, sql, MappingCache<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }
    }
}