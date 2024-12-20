﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Vaelastrasz.Library.Extensions
{
    public static class StringExtensions
    {
        public static string GetPrefix(this string text)
        {
            if (text.Contains("/"))
                return text.Split('/')[0];

            return text;
        }

        public static string GetSuffix(this string text)
        {
            if (text.Contains("/"))
                return text.Split('/')[1];

            return text;
        }

        public static bool IsValidRegex(this string text)
        {
            bool isValid = true;

            if ((text != null) && (text.Trim().Length > 0))
            {
                try
                {
                    Regex.Match("", text);
                }
                catch (ArgumentException)
                {
                    // BAD PATTERN: Syntax error
                    isValid = false;
                }
            }
            else
            {
                //BAD PATTERN: Pattern is null or blank
                isValid = false;
            }

            return (isValid);
        }

        public static string Replace(this string text, Dictionary<string, string> replacements)
        {
            return Regex.Replace(text, "(" + String.Join("|", replacements.Keys) + ")", delegate (Match m) { return replacements[m.Value]; });
        }
    }
}