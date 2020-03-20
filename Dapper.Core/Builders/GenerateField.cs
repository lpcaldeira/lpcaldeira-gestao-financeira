using Dapper.Core.Contexts.Database.Fields;

namespace Dapper.Core.Builders
{
    public class GenerateField
    {
        public static GenerateField Init() => new GenerateField();

        private string IsNotNull(bool isNotNull)
            => isNotNull ? " not null" : string.Empty;

        private string BuildField(Field field)
            => $"{field.FieldName} {field.FieldType}{IsNotNull(field.IsNotNull)}";

        private string BuildFieldKey(Field field, string sequenceName)
            => $"{field.FieldName} {field.FieldType} default nextval('{sequenceName}') {IsNotNull(field.IsNotNull)}";

        private string BuildFieldLength(FieldLength fieldLength)
            => $"{fieldLength.FieldName} {fieldLength.FieldType}" +
               $"({fieldLength.Length}){IsNotNull(fieldLength.IsNotNull)}";

        private string BuildFieldPrecision(FieldPrecision fieldPrecision)
            => $"{fieldPrecision.FieldName} {fieldPrecision.FieldType}" +
               $"({fieldPrecision.Precision}, {fieldPrecision.Scale}){IsNotNull(fieldPrecision.IsNotNull)}";

        public string Build(Field field, string sequenceName)
        {
            switch (field)
            {
                case FieldKey key:
                    return BuildFieldKey(key, sequenceName);
                case FieldLength length:
                    return BuildFieldLength(length);
                case FieldPrecision precision:
                    return BuildFieldPrecision(precision);
                default:
                    return BuildField(field);
            }
        }
    }
}