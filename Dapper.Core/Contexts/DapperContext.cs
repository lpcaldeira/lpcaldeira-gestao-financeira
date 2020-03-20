using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using Dapper.Core.Contexts.Interfaces;
using Dapper.Core.Extensions.Database;
using Dapper.Core.Extensions.Delete;
using Dapper.Core.Extensions.Insert;
using Dapper.Core.Extensions.Select;
using Dapper.Core.Extensions.Update;

namespace Dapper.Core.Contexts
{
    public abstract class DapperContext<TAdapter> : IDapperContext where TAdapter : IDatabaseAdapter
    {
        protected readonly IDbConnection DbConnection;

        protected DapperContext(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        public abstract Task CreateDatabaseAsync(Assembly assemblyOfModels);

        public void Insert<T>(T entity) where T : class
        {
            DbConnection.InsertOne<TAdapter, T>(entity);
        }

        public async Task InsertAsync<T>(T entity) where T : class
        {
            await DbConnection.InsertOneAsync<TAdapter, T>(entity);
        }

        public void Insert<T>(IEnumerable<T> entities) where T : class
        {
            DbConnection.InsertMany<TAdapter, T>(entities);
        }

        public async Task InsertAsync<T>(IEnumerable<T> entities) where T : class
        {
            await DbConnection.InsertManyAsync<TAdapter, T>(entities);
        }

        public void Update<T>(T entity) where T : class
        {
            DbConnection.UpdateOne<TAdapter, T>(entity);
        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            await DbConnection.UpdateOneAsync<TAdapter, T>(entity);
        }

        public void Update<T>(IEnumerable<T> entities) where T : class
        {
            DbConnection.UpdateMany<TAdapter, T>(entities);
        }

        public async Task UpdateAsync<T>(IEnumerable<T> entities) where T : class
        {
            await DbConnection.UpdateManyAsync<TAdapter, T>(entities);
        }

        public void Delete<T>(T entity) where T : class
        {
            DbConnection.DeleteOne<TAdapter, T>(entity);
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            await DbConnection.DeleteOneAsync<TAdapter, T>(entity);
        }

        public void Delete<T>(IEnumerable<T> entities) where T : class
        {
            DbConnection.DeleteMany<TAdapter, T>(entities);
        }

        public async Task DeleteAsync<T>(IEnumerable<T> entities) where T : class
        {
            await DbConnection.DeleteManyAsync<TAdapter, T>(entities);
        }

        public void DeleteAll<T>() where T : class
        {
            DbConnection.DeleteAll<T>();
        }

        public async Task<T> GetById<T>(int id) where T : class
        {
            return await DbConnection.GetOneById<T>(id);
        }

        public IEnumerable<TFirst> Query<TFirst, TSecond>(string sql, DynamicParameters parameters = null)
        {
            return DbConnection.Query<TFirst, TSecond>(sql, parameters);
        }

        public IEnumerable<TFirst> Query<TFirst, TSecond, TThird>(string sql, DynamicParameters parameters = null)
        {
            return DbConnection.Query<TFirst, TSecond, TThird>(sql, parameters);
        }

        public IEnumerable<TFirst> Query<TFirst, TSecond, TThird, TFourth>(string sql, DynamicParameters parameters = null)
        {
            return DbConnection.Query<TFirst, TSecond, TThird, TFourth>(sql, parameters);
        }

        public IEnumerable<TFirst> Query<TFirst, TSecond, TThird, TFourth, TFifth>(string sql, DynamicParameters parameters = null)
        {
            return DbConnection.Query<TFirst, TSecond, TThird, TFourth, TFifth>(sql, parameters);
        }

        public IEnumerable<TFirst> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(string sql, DynamicParameters parameters = null)
        {
            return DbConnection.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(sql, parameters);
        }

        public IEnumerable<TFirst> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(string sql, DynamicParameters parameters = null)
        {
            return DbConnection.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(sql, parameters);
        }

        public async Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond>(string sql, DynamicParameters parameters = null)
        {
            return await DbConnection.QueryAsync<TFirst, TSecond>(sql, parameters);
        }

        public async Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird>(string sql, DynamicParameters parameters = null)
        {
            return await DbConnection.QueryAsync<TFirst, TSecond, TThird>(sql, parameters);
        }

        public async Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird, TFourth>(string sql, DynamicParameters parameters = null)
        {
            return await DbConnection.QueryAsync<TFirst, TSecond, TThird, TFourth>(sql, parameters);
        }

        public async Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth>(string sql, DynamicParameters parameters = null)
        {
            return await DbConnection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth>(sql, parameters);
        }

        public async Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(string sql, DynamicParameters parameters = null)
        {
            return await DbConnection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(sql, parameters);
        }

        public async Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(string sql, DynamicParameters parameters = null)
        {
            return await DbConnection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh> (sql, parameters);
        }

        public async Task<IEnumerable<T>> QueryManyAsync<T>(string sql, DynamicParameters parameters = null)
        {
            return await DbConnection.QueryAsync<T>(sql, parameters);
        }

        public async Task<T> QueryOneAsync<T>(string sql, DynamicParameters parameters = null)
        {
            return await DbConnection.QueryFirstOrDefaultAsync<T>(sql, parameters);
        }

        public void ExecuteScript(string script)
        {
            if (DbConnection.State == ConnectionState.Closed) DbConnection.Open();
            using (var transaction = DbConnection.BeginTransaction())
            {
                try
                {
                    DbConnection.Execute(script);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }

        public IEnumerable<T> Query<T>(string sql, DynamicParameters parameters)
        {
            if (DbConnection.State == ConnectionState.Closed)
                DbConnection.Open();
            return DbConnection.Query<T>(sql, parameters);
        }

        public IEnumerable<T> Query<T>(string sql)
        {
            if (DbConnection.State == ConnectionState.Closed)
                DbConnection.Open();
            return DbConnection.Query<T>(sql);
        }

        public IEnumerable<dynamic> Query(string sql, DynamicParameters parameters)
        {
            if (DbConnection.State == ConnectionState.Closed)
                DbConnection.Open();
            return DbConnection.Query(sql, parameters);
        }

        public IEnumerable<dynamic> Query(string sql)
        {
            if (DbConnection.State == ConnectionState.Closed)
                DbConnection.Open();
            return DbConnection.Query(sql);
        }
    }
}