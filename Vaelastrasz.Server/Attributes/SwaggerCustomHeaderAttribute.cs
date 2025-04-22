namespace Vaelastrasz.Server.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SwaggerCustomHeaderAttribute : Attribute
    {
        public SwaggerCustomHeaderAttribute(string headerName, params string[] acceptableTypes)
        {
            HeaderName = headerName;
            AcceptableTypes = acceptableTypes;
        }

        public string[] AcceptableTypes { get; }

        public string HeaderName { get; }
    }
}