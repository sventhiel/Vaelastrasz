using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Vaelastrasz.Library.Extensions
{
    public static class FileInfoExtensions
    {
        public static string GetExtension(this FileInfo @this)
        {
            return Path.GetExtension(@this.FullName);
        }

        public static string GetFileNameWithoutExtension(this FileInfo @this)
        {
            return Path.GetFileNameWithoutExtension(@this.FullName);
        }
    }
}
