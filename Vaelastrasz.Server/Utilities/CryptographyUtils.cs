using System.Security.Cryptography;
using System.Text;

namespace Vaelastrasz.Server.Utilities
{

    /// <summary>
    /// 
    /// </summary>
    public static class CryptographyUtils
    {
        /// <summary>
        /// ASCII to prevent further investigation of 1 or 2 byte characters.
        /// </summary>
        /// <param name="t1"></param>
        /// <returns></returns>
        public static string GetSHA512HashAsBase64(string t1)
        {
            using (var sha512 = SHA512.Create())
            {
                byte[] saltedPassword = Encoding.Latin1.GetBytes(t1);
                return Convert.ToBase64String(sha512.ComputeHash(saltedPassword));
            }
        }

        public static string GetSHA512HashAsBase64(string t1, string t2)
        {
            using (var sha512 = SHA512.Create())
            {
                byte[] saltedPassword = (Encoding.Latin1.GetBytes(t1).Concat(Encoding.Latin1.GetBytes(t2))).ToArray();
                return Convert.ToBase64String(sha512.ComputeHash(saltedPassword));
            }
        }

        public static string GetRandomHexadecimalString(int size)
        {
            var rnd = RandomNumberGenerator.GetBytes(size);
            return Convert.ToHexString(rnd);
        }

        public static string GetRandomBase64String(int size)
        {
            var rnd = RandomNumberGenerator.GetBytes(size);
            return Convert.ToBase64String(rnd); ;
        }
    }
}