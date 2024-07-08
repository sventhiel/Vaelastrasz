﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Vaelastrasz.Library.Extensions
{
    public static class StringExtensions
    {
        public static string Replace(this string text, Dictionary<string, string> replacements)
        {
            return Regex.Replace(text, "(" + String.Join("|", replacements.Keys) + ")", delegate (Match m) { return replacements[m.Value]; });
        }

        public static string GetPrefix(this string text)
        {
            return text.Split('/')[0];
        }

        public static string GetSuffix(this string text)
        {
            return text.Split('/')[1];
        }
    }
}