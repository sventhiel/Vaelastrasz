using Fare;
using System.Text.RegularExpressions;

namespace Vaelastrasz.Server.Helpers
{
    public class DOIHelper
    {
        public static string Create(string prefix, string pattern, Dictionary<string, string> placeholders = null)
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

                Xeger xeger = new Xeger($"{pattern}", new Random());
                var suffix = xeger.Generate();

                return $"{prefix}/{suffix}";
            }
            catch (Exception)
            {
                throw;
            }
        }

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