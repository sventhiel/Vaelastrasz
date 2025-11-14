using System;
using System.Net.Http.Headers;
using System.Text;

namespace Vaelastrasz.Library.Configurations
{
    public class Configuration
    {
        public Configuration()
        {
        }

        public Configuration(string username, string password, string host)
        {
            Username = username;
            Password = password;
            Host = host;
        }

        public string Host { get; set; }
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