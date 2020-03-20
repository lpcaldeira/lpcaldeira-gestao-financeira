namespace Dapper.Core.Builders
{
    public class GenerateSequence
    {
        public static GenerateSequence Init() => new GenerateSequence();

        public string Build(string sequenceName)
        {
            return $"create sequence {sequenceName}";
        }
    }
}