namespace Vaelastrasz.Server.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SwaggerCustomHeaderAttribute : Attribute
    {
        public string HeaderName { get; }
        public string[] AcceptableTypes { get; }

        public SwaggerCustomHeaderAttribute(string headerName, params string[] acceptableTypes)
        {
            HeaderName = headerName;
            AcceptableTypes = acceptableTypes;
        }
    }
}
