using System.Security.Cryptography;
using System.Text;

namespace Vaelastrasz.Server.Utilities
{
    public static class CryptographyUtils
    {
        public static string GetSHA512HashAsBase64(string t1)
        {
            using (var hmacsha512 = new HMACSHA512())
            {
                byte[] saltedPassword = Encoding.ASCII.GetBytes(t1);
                return Convert.ToBase64String(hmacsha512.ComputeHash(saltedPassword));
            }
        }

        public static string GetSHA512HashAsBase64(string t1, string t2)
        {
            using (var hmacsha512 = new HMACSHA512())
            {
                byte[] saltedPassword = (Encoding.ASCII.GetBytes(t1).Concat(Encoding.ASCII.GetBytes(t2))).ToArray();
                return Convert.ToBase64String(hmacsha512.ComputeHash(saltedPassword));
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