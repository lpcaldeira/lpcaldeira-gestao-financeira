using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Dapper.Core.Extensions.Database
{
    public interface IDatabaseAdapter
    {
        int Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName, string columnList, string parameterList, IEnumerable<PropertyInfo> keyProperties, object entityToInsert);
        void AppendColumnName(StringBuilder sb, string columnName);
        void AppendColumnNameEqualsValue(StringBuilder sb, string columnName);
    }
}