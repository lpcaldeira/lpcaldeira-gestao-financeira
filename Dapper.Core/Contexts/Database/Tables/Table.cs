using System.Collections.Generic;
using Dapper.Core.Contexts.Database.Fields;

namespace Dapper.Core.Contexts.Database.Tables
{
    public class Table
    {
        public Table(string tableName, string sequenceName, IEnumerable<Field> fields)
        {
            TableName = tableName;
            SequenceName = sequenceName;
            Fields = fields;
        }

        public string TableName { get; }
        public string SequenceName { get; }
        public IEnumerable<Field> Fields { get; }
    }
}