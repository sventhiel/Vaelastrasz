//using Microsoft.OpenApi;
//using Swashbuckle.AspNetCore.SwaggerGen;
//using System.Runtime.Serialization;

//namespace Vaelastrasz.Server.Filters
//{
//    public class EnumSchemaFilter : ISchemaFilter
//    {
//        public void Apply(IOpenApiSchema model, SchemaFilterContext context)
//        {
//            if (context.Type.IsEnum)
//            {
//                model.Enum.Clear();
//                foreach (string enumName in Enum.GetNames(context.Type))
//                {
//                    System.Reflection.MemberInfo? memberInfo = context.Type.GetMember(enumName).FirstOrDefault(m => m.DeclaringType == context.Type);
//                    EnumMemberAttribute? enumMemberAttribute = memberInfo?.GetCustomAttributes(typeof(EnumMemberAttribute), false).OfType<EnumMemberAttribute>().FirstOrDefault();
//                    string label = enumMemberAttribute == null || string.IsNullOrWhiteSpace(enumMemberAttribute.Value)
//                     ? enumName
//                     : enumMemberAttribute.Value;
//                    model.Enum.Add(label);
//                }
//            }
//        }
//    }
//}