using System;
using System.Net.Http.Headers;
using System.Text;

namespace Vaelastrasz.Library.Configurations
{
    public class Configuration
    {
        public Configuration(string username, string password, string host, bool ignoreNull)
        {
            Username = username;
            Password = password;
            Host = host;
            IgnoreNull = ignoreNull;
        }

        public string Host { get; set; }
        public bool IgnoreNull { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        public AuthenticationHeaderValue GetBasicAuthenticationHeaderValue()
        {
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Username}:{Password}")));
        }

        public string GetBasicAuthorizationHeader()
        {
            return $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Username}:{Password}"))}";
        }
    }
}