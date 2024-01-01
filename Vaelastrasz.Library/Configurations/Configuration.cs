﻿using System;
using System.Text;

namespace Vaelastrasz.Library.Configurations
{
    public class Configuration
    {
        /// <summary>
        /// The general configuration that is needed to connect to the proxy.
        /// </summary>
        /// <param name="username">The username of the credentials you will use to connect to the proxy.</param>
        /// <param name="password">The password of the <paramref name="username"/></param>
        /// <param name="host">The base url of the host proxy system (e.g. https://doi.bexis2.uni-jena.de)</param>
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