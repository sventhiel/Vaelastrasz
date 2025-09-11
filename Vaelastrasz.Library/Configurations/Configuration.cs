using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Vaelastrasz.Library.Configurations
{
    public class Configuration
    {
        public Configuration()
        {
            UpdateProperties = new List<string>();
        }

        public Configuration(string username, string password, string host, List<string> updateProperties)
        {
            Username = username;
            Password = password;
            Host = host;
            UpdateProperties = updateProperties;
        }

        public string Host { get; set; }
        public List<string> UpdateProperties { get; set; }
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