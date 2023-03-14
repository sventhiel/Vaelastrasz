using System;
using System.Collections.Generic;
using System.Text;

namespace Vaelastrasz.Library.Configurations
{
    public class Configuration
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public Dictionary<string, string> Placeholders { get; set; }

        public Configuration(string username, string password, string host, Dictionary<string, string> placeholders = null)
        {
            Username = username;
            Password = password;
            Host = host;
            Placeholders = placeholders ?? new Dictionary<string, string>();
        }


    }
}
