namespace Dapper.Core.Contexts.Database.Fields
{
    public class FieldLength : Field
    {
        public FieldLength(string fieldName, string fieldType, bool isNotNull, int length) 
            : base(fieldName, fieldType, isNotNull)
        {
            Length = length != 0 ? length : 255;
        }

        public int Length { get; }
    }
}