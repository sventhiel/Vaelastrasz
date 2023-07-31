using Fare;
using System.Text.RegularExpressions;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Server.Models;

namespace Vaelastrasz.Server.Helpers
{
    public class DOIHelper
    {
        public static bool Validate(string doi, string prefix, string pattern, Dictionary<string, string> placeholders = null)
        {
            try
            {
                if (placeholders != null)
                {
                    foreach (var placeholder in placeholders)
                    {
                        pattern = pattern.Replace(placeholder.Key, placeholder.Value);
                    }
                }
                Regex rg = new Regex($"{prefix}/{pattern}");
                return rg.IsMatch(doi);
            }
            catch
            {
                return false;
            }
        }
    }
}