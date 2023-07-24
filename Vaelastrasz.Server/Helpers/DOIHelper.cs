using Fare;
using System.Text.RegularExpressions;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Server.Helpers
{
    public class DOIHelper
    {
        public static ReadDOIModel Create(string prefix, string pattern, Dictionary<string, string> placeholders)
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

                return new ReadDOIModel() { DOI = $"{prefix}/{suffix}" };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool Validate(ReadDOIModel model, string prefix, string pattern, Dictionary<string, string> placeholders = null)
        {
            return Validate(model.DOI, prefix, pattern, placeholders);
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