using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using Dapper.Core.Contexts;
using Dapper.Core.Extensions.Database;
using Dapper.PostgreSql.Contexts.Database;

namespace Dapper.PostgreSql.Contexts
{
    public class PostgresDbContext : DapperContext<PostgresAdapter>
    {
        public PostgresDbContext(IDbConnection dbConnection)
            : base(dbConnection) { }

        public override Task CreateDatabaseAsync(Assembly assemblyOfModels)
        {
            return DbConnection.CreateDatabase(assemblyOfModels,
                PostgresDataType.Init()
                    .SetarRelationDataTypes()
                    .ObterRelationDataTypes(),
                new PostgresDatabase());
        }
    }
}