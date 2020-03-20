namespace Dapper.Core.Extensions
{
    public static class HelperExtension
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
    }
}
