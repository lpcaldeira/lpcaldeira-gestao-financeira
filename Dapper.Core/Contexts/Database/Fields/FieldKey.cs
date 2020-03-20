namespace Dapper.Core.Contexts.Database.Fields
{
    public class FieldKey : Field
    {
        public FieldKey(string fieldName, string fieldType) : base(fieldName, fieldType, true) { }
    }
}