using System;

namespace Dapper.Core.Attributes
{
    public class ReferenceKey : Attribute
    {
        public ReferenceKey(Type referenceType)
        {
            ReferenceType = referenceType;
        }

        public Type ReferenceType { get; }
    }
}