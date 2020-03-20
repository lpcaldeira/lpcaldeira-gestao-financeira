namespace Dapper.Core.Builders
{
    public class GeneratePrimaryKey
    {
        private string _alter;
        private string _constraint;
        private string _tableName;
        private string _keyFields;

        public static GeneratePrimaryKey Init() => new GeneratePrimaryKey();

        public GeneratePrimaryKey Alter(string tableName)
        {
            _tableName = tableName;
            _alter = $"alter table {tableName}";
            return this;
        }

        public GeneratePrimaryKey AddConstraint()
        {
            _constraint = $"add constraint {_tableName}_Pk";
            return this;
        }

        public GeneratePrimaryKey AddKeyFields(params string[] fields)
        {   
            _keyFields = string.Join(",", fields);
            return this;
        }

        public string Build()
        {
            return $"{_alter} {_constraint} primary key ({_keyFields});";
        }
    }
}
