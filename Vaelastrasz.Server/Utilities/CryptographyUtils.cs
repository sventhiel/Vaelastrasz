using System.Security.Cryptography;
using System.Text;

namespace Vaelastrasz.Server.Utilities
{
    public static class CryptographyUtils
    {
        public static string GetSHA512HashAsBase64(string t1)
        {
            using (var sha512 = new SHA512Managed())
            {
                byte[] saltedPassword = Encoding.UTF8.GetBytes(t1);
                return Convert.ToBase64String(sha512.ComputeHash(saltedPassword));
            }
        }

        public static string GetSHA512HashAsBase64(string t1, string t2)
        {
            using (var sha512 = new SHA512Managed())
            {
                byte[] saltedPassword = (Encoding.UTF8.GetBytes(t1).Concat(Encoding.UTF8.GetBytes(t2))).ToArray();
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
