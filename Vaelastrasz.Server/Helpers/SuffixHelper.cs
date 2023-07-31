using Fare;
using System.Text.RegularExpressions;
using Vaelastrasz.Server.Models;

namespace Vaelastrasz.Server.Helpers
{
    public class SuffixHelper
    {
        public static string Create(string pattern, Dictionary<string, string> placeholders = null)
        {
            try
            {
                // check placeholders and replace them
                if (placeholders != null)
                {
                    foreach (var placeholder in placeholders)
                    {
                        pattern = pattern.Replace(placeholder.Key, placeholder.Value);
                    }
                }

                // create a random suffix that matches the pattern and return it
                Xeger xeger = new Xeger($"{pattern}", new Random());
                var suffix = xeger.Generate();

                return suffix;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool Validate(string suffix, string pattern, Dictionary<string, string> placeholders = null)
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
                Regex rg = new Regex($"{pattern}");
                return rg.IsMatch(suffix);
            }
            catch
            {
                return false;
            }
        }
    }
}
