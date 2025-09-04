using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Net;

namespace Vaelastrasz.Library.Extensions
{
    public static class StringContentExtensions
    {
        public static StringContent AsJson(this object o) => new StringContent(WebUtility.HtmlEncode(JsonConvert.SerializeObject(o)), Encoding.UTF8, "application/json");
    }
}