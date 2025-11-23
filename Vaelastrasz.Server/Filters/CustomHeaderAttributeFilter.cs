namespace Vaelastrasz.Server.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomHeaderAttribute : Attribute
    {
        public string HeaderName { get; }
        public IReadOnlyList<string> AcceptableTypes { get; }

        public CustomHeaderAttribute(string headerName, params string[] acceptableTypes)
        {
            HeaderName = headerName;
            AcceptableTypes = acceptableTypes;
        }
    }
}