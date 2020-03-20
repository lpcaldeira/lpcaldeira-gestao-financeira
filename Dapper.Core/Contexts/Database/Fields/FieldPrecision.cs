namespace Dapper.Core.Contexts.Database.Fields
{
    public class FieldPrecision : Field
    {
        public FieldPrecision(string fieldName, string fieldType, bool isNotNull, int precision, int scale)
            : base(fieldName, fieldType, isNotNull)
        {
            Precision = precision != 0 ? precision : 15;
            Scale = scale != 0 ? scale : 4;
        }

        public int Precision { get; }
        public int Scale { get; }
    }
}