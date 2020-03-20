using System.Collections.Generic;
using Dapper.Core.Contexts.Database.Tables;

namespace Dapper.Core.Contexts.Database
{
    public abstract class DapperDatabase
    {
        public abstract string GenerateScriptCreateTable(Table table);
        public abstract string GenerateScriptPrimaryKey(Table table);
        public abstract string GenerateScriptCreateSequence(Table table);
        public abstract IEnumerable<string> GenerateScriptsForeignKeys(Table table);
    }
}