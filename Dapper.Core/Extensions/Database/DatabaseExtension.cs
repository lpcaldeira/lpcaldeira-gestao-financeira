using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper.Core.Attributes;
using Dapper.Core.Contexts.Database;
using Dapper.Core.Contexts.Database.Tables;

namespace Dapper.Core.Extensions.Database
{
    public static class DatabaseExtension
    {
        public static Task CreateDatabase(this IDbConnection connection,
            Assembly assemblyOfModels,
            IEnumerable<RelationDataType> relationsDataType,
            DapperDatabase dapperDatabase)
        {
            return Task.Factory.StartNew(() =>
            {
                var tablesTypes = assemblyOfModels.GetTypes()
                    .Where(a => a.GetCustomAttributes(true).OfType<TableAttribute>().Any());

                DapperField.SetarRelationDataTypes(relationsDataType);

                var dapperTables = (from tableType in tablesTypes
                                    select DapperTable.Criar(tableType)).ToList();

                CreateSequences(dapperTables, dapperDatabase, connection);
                CreateTables(dapperTables, dapperDatabase, connection);
                CreatePrimaryKeys(dapperTables, dapperDatabase, connection);
                CreateForeignKeys(dapperTables, dapperDatabase, connection);
            });
        }

        private static void CreateSequences(IEnumerable<Table> tables, DapperDatabase dapperDatabase, IDbConnection connection)
        {
            foreach (var table in tables)
                ExecuteScript(dapperDatabase.GenerateScriptCreateSequence(table), connection);
        }

        private static void CreateTables(IEnumerable<Table> tables, DapperDatabase dapperDatabase, IDbConnection connection)
        {
            foreach (var table in tables)
                ExecuteScript(dapperDatabase.GenerateScriptCreateTable(table), connection);
        }

        private static void CreatePrimaryKeys(IEnumerable<Table> tables, DapperDatabase dapperDatabase, IDbConnection connection)
        {
            foreach (var table in tables)
                ExecuteScript(dapperDatabase.GenerateScriptPrimaryKey(table), connection);
        }

        private static void CreateForeignKeys(IEnumerable<Table> tables, DapperDatabase dapperDatabase, IDbConnection connection)
        {
            foreach (var table in tables)
                foreach (var script in dapperDatabase.GenerateScriptsForeignKeys(table))
                    ExecuteScript(script, connection);
        }

        private static void ExecuteScript(string script, IDbConnection connection)
        {
            if (connection.State == ConnectionState.Closed) connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    connection.Execute(script);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}
