﻿using Vaelastrasz.Library.Configurations;

namespace Vaelastrasz.Library.Services
{
    public class DOIService
    {
        private readonly Configuration _config;

        public DOIService(Configuration config)
        {
            _config = config;
        }
    }
}