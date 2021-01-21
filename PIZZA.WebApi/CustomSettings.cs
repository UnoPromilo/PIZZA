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
        public void LoadConfiguration()
        {
            Configured = bool.Parse(ConfigurationManager.AppSettings.Get(nameof(Configured)));
            ConnectionString = ConfigurationManager.AppSettings.Get(nameof(ConnectionString));
        }
        public void SaveConfiguration()
        {
            ConfigurationManager.AppSettings.Set(nameof(ConnectionString), ConnectionString);
        }
    }
}
