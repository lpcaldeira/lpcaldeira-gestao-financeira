namespace Dapper.Core.Contexts.Database.Fields
{
    public class FieldReferenceKey : Field
    {
        public FieldReferenceKey(string fieldName, string fieldType, bool isNotNull, int length,
            string referenceTableName, string referenceFieldName)
            : base(fieldName, fieldType, isNotNull)
        {
            ReferenceTableName = referenceTableName;
            ReferenceFieldName = referenceFieldName;
        }

        public string ReferenceTableName { get; }
        public string ReferenceFieldName { get; }
    }
}