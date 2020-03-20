using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Core.Contexts.Interfaces;
using Dapper.Core.Extensions.Select;
using Gomi.Infraestrutura.Interfaces;

namespace Gomi.Dados.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IDapperContext DbContext;

        protected Repository(IDapperContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Add(T entity)
        {
            DbContext.Insert(entity);
        }

        public void Update(T entity)
        {
            DbContext.Update(entity);
        }

        public void Delete(T entity)
        {
            DbContext.Delete(entity);
        }

        public async Task<T> GetById(int id)
        {
            return await DbContext.GetById<T>(id);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            var tableName = SelectMethodExtension.GetTableName(typeof(T));
            var sql = $"select * from {tableName}";
            return await DbContext.QueryManyAsync<T>(sql);
        }
    }
}
