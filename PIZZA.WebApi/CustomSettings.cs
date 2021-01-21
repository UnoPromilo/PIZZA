using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

namespace PIZZA.WebApi
{
    public class CustomSettings
    {
        public CustomSettings()
        {
            LoadConfiguration();
        }

        public bool Configured { get; set; }
        public string ConnectionString { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public string JwtExpiryInDays { get; set; }
        public string JwtSecurityKey { get; set; }
        public void LoadConfiguration()
        {
            Configured = bool.Parse(ConfigurationManager.AppSettings.Get(nameof(Configured)));
            ConnectionString = ConfigurationManager.AppSettings.Get(nameof(ConnectionString));
            JwtIssuer = ConfigurationManager.AppSettings.Get(nameof(JwtIssuer));
            JwtAudience = ConfigurationManager.AppSettings.Get(nameof(JwtAudience));
            JwtExpiryInDays = ConfigurationManager.AppSettings.Get(nameof(JwtExpiryInDays));
            JwtSecurityKey = ConfigurationManager.AppSettings.Get(nameof(JwtSecurityKey));
        }
        public void SaveConfiguration()
        {
            ConfigurationManager.AppSettings.Set(nameof(Configured), Configured.ToString());
            ConfigurationManager.AppSettings.Set(nameof(ConnectionString), ConnectionString);
            ConfigurationManager.AppSettings.Set(nameof(JwtIssuer), JwtIssuer);
            ConfigurationManager.AppSettings.Set(nameof(JwtAudience), JwtAudience);
            ConfigurationManager.AppSettings.Set(nameof(JwtExpiryInDays), JwtExpiryInDays);
            ConfigurationManager.AppSettings.Set(nameof(JwtSecurityKey), JwtSecurityKey);
        }
    }
}
