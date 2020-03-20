using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using Dapper.Core.Contexts.Database.Fields;
using Dapper.Core.Extensions.Database;

namespace Dapper.Core.Contexts.Database
{
    public class DapperField
    {
        private static IDictionary<Type, RelationDataType> _relationDataTypes;

        public static void SetarRelationDataTypes(
            IEnumerable<RelationDataType> relationDataTypes)
        {
            _relationDataTypes = relationDataTypes.ToDictionary(r => r.SystemType);
        }

        private static Field CriarField(string fieldName, string fieldType, bool isNotNull)
        {
            return new Field(fieldName, fieldType, isNotNull);
        }

        private static Field CriarFieldKey(string fieldName, string fieldType)
        {
            return new FieldKey(fieldName, fieldType);
        }

        private static Field CriarFieldReferenceKey(string fieldName, string fieldType, bool isNotNull, string referenceTableName, string referenceFieldName, int length = 0)
        {
            return new FieldReferenceKey(fieldName, fieldType, isNotNull, length, referenceTableName, referenceFieldName);
        }

        private static Field CriarFieldPrecision(string fieldName, string fieldType, bool isNotNull, int precision, int scale)
        {
            return new FieldPrecision(fieldName, fieldType, isNotNull, precision, scale);
        }

        private static Field CriarFieldLength(string fieldName, string fieldType, bool isNotNull, int length)
        {
            return new FieldLength(fieldName, fieldType, isNotNull, length);
        }

        public static Field Criar(PropertyInfo propertyInfo)
        {
            var type = propertyInfo.PropertyType;
            if (type.BaseType == typeof(Enum)) type = type.BaseType;
            var attributes = propertyInfo.GetCustomAttributes(true);
            var relationDataType = _relationDataTypes[type];

            if (type == typeof(long) || type == typeof(int) || type == typeof(int?) || type == typeof(short))
                if (attributes.FieldIsKey())
                    return CriarFieldKey(propertyInfo.Name, relationDataType.DataType);
                else if (attributes.FieldIsReferenceKey())
                {
                    var reference = attributes.FieldReferenceKey();
                    var referenceTableName = reference.ReferenceType.GetCustomAttributes(true).TableAttribute();
                    var referenceFieldName = reference.ReferenceType.FieldKeyName();

                    return CriarFieldReferenceKey(propertyInfo.Name, relationDataType.DataType, attributes.FieldIsNotNull(),
                        referenceTableName.Name, referenceFieldName);
                }
                else return CriarField(propertyInfo.Name, relationDataType.DataType, attributes.FieldIsNotNull());

            if (type == typeof(bool) || type == typeof(byte[]) || type == typeof(DateTime) || type == typeof(DateTime?) ||
                type == typeof(TimeSpan) || type == typeof(IPAddress))
                return CriarField(propertyInfo.Name, relationDataType.DataType, attributes.FieldIsNotNull());

            if (type == typeof(float) || type == typeof(double) || type == typeof(decimal))
            {
                var precision = attributes.FieldPrecision();

                return CriarFieldPrecision(propertyInfo.Name, relationDataType.DataType,
                    attributes.FieldIsNotNull(), precision.Valor, precision.Scale);
            }

            var length = attributes.FieldLength();

            return CriarFieldLength(propertyInfo.Name, relationDataType.DataType, attributes.FieldIsNotNull(), length.Valor);
        }


    }
}