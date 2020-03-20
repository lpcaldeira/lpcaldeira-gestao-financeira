namespace Dapper.Core.Contexts.Database.Fields
{
    public class Field
    {
        public Field(string fieldName, string fieldType, bool isNotNull)
        {
            FieldName = fieldName;
            FieldType = fieldType;
            IsNotNull = isNotNull;
        }

        public string FieldName { get; }
        public string FieldType { get; }
        public bool IsNotNull { get; }
    }
}