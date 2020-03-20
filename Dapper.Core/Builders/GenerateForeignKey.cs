namespace Dapper.Core.Builders
{
    public class GenerateForeignKey
    {
        private string _alter;
        private string _constraint;
        private string _tableName;
        private string _referenceTableName;
        private string _foreignKeyField;
        private string _referenceField;

        public static GenerateForeignKey Init() => new GenerateForeignKey();

        public GenerateForeignKey Alter(string tableName)
        {
            _tableName = tableName;
            _alter = $"alter table {tableName}";
            return this;
        }

        public GenerateForeignKey AddConstraint()
        {
            _constraint = $"add constraint {_tableName}_{_referenceTableName}_fk";
            return this;
        }

        public GenerateForeignKey AddFieldForeignKey(string field)
        {
            _foreignKeyField = field;
            return this;
        }

        public GenerateForeignKey AddReference(string tableName)
        {
            _referenceTableName = tableName;
            return this;
        }

        public GenerateForeignKey AddFieldReference(string field)
        {
            _referenceField = field;
            return this;
        }


        public string Build()
        {
            return $"{_alter} {_constraint} foreign key ({_foreignKeyField}) references {_referenceTableName} ({_referenceField});";
        }
    }
}
