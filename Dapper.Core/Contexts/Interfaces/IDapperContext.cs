using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Dapper.Core.Contexts.Interfaces
{
    public interface IDapperContext
    {
        Task CreateDatabaseAsync(Assembly assemblyOfModels);
        void Insert<T>(T entity) where T : class;
        Task InsertAsync<T>(T entity) where T : class;
        void Insert<T>(IEnumerable<T> entities) where T : class;
        Task InsertAsync<T>(IEnumerable<T> entities) where T : class;
        void Update<T>(T entity) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;
        void Update<T>(IEnumerable<T> entities) where T : class;
        Task UpdateAsync<T>(IEnumerable<T> entities) where T : class;
        void Delete<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
        void Delete<T>(IEnumerable<T> entities) where T : class;
        Task DeleteAsync<T>(IEnumerable<T> entities) where T : class;
        void DeleteAll<T>() where T : class;
        Task<T> GetById<T>(int id) where T : class;
        IEnumerable<TFirst> Query<TFirst, TSecond>(string sql, DynamicParameters parameters = null);
        IEnumerable<TFirst> Query<TFirst, TSecond, TThird>(string sql, DynamicParameters parameters = null);
        IEnumerable<TFirst> Query<TFirst, TSecond, TThird, TFourth>(string sql, DynamicParameters parameters = null);
        IEnumerable<TFirst> Query<TFirst, TSecond, TThird, TFourth, TFifth>(string sql, DynamicParameters parameters = null);
        IEnumerable<TFirst> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(string sql, DynamicParameters parameters = null);
        IEnumerable<TFirst> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(string sql, DynamicParameters parameters = null);
        Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond>(string sql, DynamicParameters parameters = null);
        Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird>(string sql, DynamicParameters parameters = null);
        Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird, TFourth>(string sql, DynamicParameters parameters = null);
        Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth>(string sql, DynamicParameters parameters = null);
        Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(string sql, DynamicParameters parameters = null);
        Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(string sql, DynamicParameters parameters = null);
        Task<IEnumerable<T>> QueryManyAsync<T>(string sql, DynamicParameters parameters = null);
        Task<T> QueryOneAsync<T>(string sql, DynamicParameters parameters = null);
        void ExecuteScript(string script);
        IEnumerable<T> Query<T>(string sql, DynamicParameters parameters);
        IEnumerable<T> Query<T>(string sql);
        IEnumerable<dynamic> Query(string sql, DynamicParameters parameters);
        IEnumerable<dynamic> Query(string sql);
    }
}