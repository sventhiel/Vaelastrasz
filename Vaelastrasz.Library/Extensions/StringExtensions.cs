using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Vaelastrasz.Library.Extensions
{
    public static class StringExtensions
    {
        public static string Replace(string text, Dictionary<string, string> replacements)
        {
            return Regex.Replace(text, "(" + String.Join("|", replacements.Keys) + ")", delegate (Match m) { return replacements[m.Value]; });
        }
    }
}