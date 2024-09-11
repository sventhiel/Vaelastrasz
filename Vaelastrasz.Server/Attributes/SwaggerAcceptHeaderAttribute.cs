namespace Vaelastrasz.Server.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SwaggerAcceptHeaderAttribute : Attribute
    {
        public string[] AcceptableMediaTypes { get; }

        public SwaggerAcceptHeaderAttribute(params string[] acceptableMediaTypes)
        {
            AcceptableMediaTypes = acceptableMediaTypes;
        }
    }
}
