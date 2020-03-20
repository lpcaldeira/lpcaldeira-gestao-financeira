using System.Collections.Generic;
using System.Linq;
using Dapper.Core.Builders;
using Dapper.Core.Contexts.Database;
using Dapper.Core.Contexts.Database.Fields;
using Dapper.Core.Contexts.Database.Tables;

namespace Dapper.PostgreSql.Contexts.Database
{
    public class PostgresDatabase : DapperDatabase
    {
        public override string GenerateScriptCreateTable(Table table)
            => CreateTable.Init().Create(table.TableName).AddFields(table.Fields, table.SequenceName).Build();

        public override string GenerateScriptPrimaryKey(Table table)
        {
            return GeneratePrimaryKey.Init().Alter(table.TableName).AddConstraint()
                .AddKeyFields(table.Fields.OfType<FieldKey>().Select(f => f.FieldName).ToArray()).Build();
        }

        public override string GenerateScriptCreateSequence(Table table)
        {
            return GenerateSequence.Init().Build(table.SequenceName);
        }

        public override IEnumerable<string> GenerateScriptsForeignKeys(Table table)
        {
            return table.Fields.OfType<FieldReferenceKey>()
                .Select(field => GenerateForeignKey.Init()
                    .Alter(table.TableName)
                    .AddConstraint()
                    .AddFieldForeignKey(field.FieldName)
                    .AddReference(field.ReferenceTableName)
                    .AddFieldReference(field.ReferenceFieldName)
                    .Build());
        }
    }
}