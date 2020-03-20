using System;
using Dapper.Core.Enums;

namespace Dapper.Core.Attributes
{
    public class Reference : Attribute
    {
        public Reference(Type referenceType, RelationType relationType, CascadeType cascadeType = CascadeType.CascadeNone)
        {
            ReferenceType = referenceType;
            RelationType = relationType;
            CascadeType = cascadeType;
        }

        public Type ReferenceType { get; private set; }
        public RelationType RelationType { get; private set; }
        public CascadeType CascadeType { get; private set; }
    }
}