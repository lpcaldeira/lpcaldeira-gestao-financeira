using System;

namespace Dapper.Core.Attributes
{
    public class Length : Attribute
    {
        public int Valor { get; }

        public Length(int length)
        {
            Valor = length;
        }
    }
}