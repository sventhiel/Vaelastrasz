using System;
using System.Text;

namespace Vaelastrasz.Library.Configurations
{
    public class Configuration
    {
        public Configuration(string username, string password, string host)
        {
            Username = username;
            Password = password;
            Host = host;
        }

        public string Host { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        public string GetBasicAuthorizationHeader()
        {
            return $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Username}:{Password}"))}";
        }
    }
}