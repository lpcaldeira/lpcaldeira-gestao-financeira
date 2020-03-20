using System;
using System.Collections.Generic;

namespace Dapper.Core.Contexts.Database
{
    public class RelationDataType
    {
        public RelationDataType(Type systemType, string dataType)
        {
            SystemType = systemType;
            DataType = dataType;
        }

        public Type SystemType { get; }
        public string DataType { get; }
    }

    public abstract class DapperDataType
    {
        protected IEnumerable<RelationDataType> RelationDataTypes { get; set; }

        public abstract DapperDataType SetarRelationDataTypes();

        public IEnumerable<RelationDataType> ObterRelationDataTypes()
        {
            return RelationDataTypes;
        }
    }
}