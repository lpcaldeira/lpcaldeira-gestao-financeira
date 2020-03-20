using System;

namespace Dapper.Core.Attributes
{
    public class Key : Attribute
    {
        public string SequenceName { get; }

        public Key(string sequenceName)
        {
            SequenceName = sequenceName;
        }
    }
}