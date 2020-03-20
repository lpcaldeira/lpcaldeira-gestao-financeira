using System;

namespace Dapper.Core.Attributes
{
    public class Precision : Attribute
    {
        public int Valor { get; }
        public int Scale { get; }

        public Precision(int precision = 15, int scale = 4)
        {
            Valor = precision;
            Scale = scale;
        }
    }
}