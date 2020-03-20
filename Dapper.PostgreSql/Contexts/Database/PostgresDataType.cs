using System;
using System.Collections.Generic;
using System.Net;
using Dapper.Core.Contexts.Database;
using NpgsqlTypes;

namespace Dapper.PostgreSql.Contexts.Database
{
    public class PostgresDataType : DapperDataType
    {
        public static DapperDataType Init() => new PostgresDataType();

        public override DapperDataType SetarRelationDataTypes()
        {
            RelationDataTypes = new List<RelationDataType>
            {
                new RelationDataType(typeof(long), NpgsqlDbType.Bigint.ToString()),
                new RelationDataType(typeof(bool), NpgsqlDbType.Boolean.ToString()),
                new RelationDataType(typeof(byte[]), NpgsqlDbType.Bytea.ToString()),
                new RelationDataType(typeof(DateTime), NpgsqlDbType.Timestamp.ToString()),
                new RelationDataType(typeof(DateTime?), NpgsqlDbType.Timestamp.ToString()),
                new RelationDataType(typeof(double), NpgsqlDbType.Double.ToString()),
                new RelationDataType(typeof(int), NpgsqlDbType.Integer.ToString()),
                new RelationDataType(typeof(int?), NpgsqlDbType.Integer.ToString()),
                new RelationDataType(typeof(decimal), NpgsqlDbType.Numeric.ToString()),
                new RelationDataType(typeof(float), NpgsqlDbType.Real.ToString()),
                new RelationDataType(typeof(short), NpgsqlDbType.Smallint.ToString()),
                new RelationDataType(typeof(string), NpgsqlDbType.Varchar.ToString()),
                new RelationDataType(typeof(TimeSpan), NpgsqlDbType.Interval.ToString()),
                new RelationDataType(typeof(IPAddress), NpgsqlDbType.Inet.ToString()),
                new RelationDataType(typeof(Guid), NpgsqlDbType.Uuid.ToString()),
                new RelationDataType(typeof(Array), NpgsqlDbType.Array.ToString()),
                new RelationDataType(typeof(Enum), NpgsqlDbType.Varchar.ToString())
            };

            return this;
        }
    }
}