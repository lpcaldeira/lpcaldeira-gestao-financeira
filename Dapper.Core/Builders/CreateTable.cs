using System.Collections.Generic;
using System.Linq;
using Dapper.Core.Contexts.Database.Fields;

namespace Dapper.Core.Builders
{
    public class CreateTable
    {
        private string _create;
        private IEnumerable<string> _fields;

        public static CreateTable Init() => new CreateTable();

        public CreateTable Create(string tableName)
        {
            _create = $"create table {tableName}";
            return this;
        }

        public CreateTable AddFields(IEnumerable<Field> fields, string sequenceName)
        {
            _fields = (from field in fields
                select GenerateField.Init().Build(field, sequenceName)).ToList();

            return this;
        }

        public string Build()
        {
            return $"{_create}({string.Join(",", _fields)});";
        }
    }
}