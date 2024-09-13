namespace Vaelastrasz.Server.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SwaggerCustomHeaderAttribute : Attribute
    {
        public string[] AcceptableTypes { get; }

        public SwaggerCustomHeaderAttribute(params string[] acceptableTypes)
        {
            AcceptableTypes = acceptableTypes;
        }
    }
}
