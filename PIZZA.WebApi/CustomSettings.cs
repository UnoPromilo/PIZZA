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
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings[nameof(Configured)].Value = Configured.ToString();
            config.AppSettings.Settings[nameof(ConnectionString)].Value = ConnectionString.ToString();
            config.AppSettings.Settings[nameof(JwtIssuer)].Value = JwtIssuer.ToString();
            config.AppSettings.Settings[nameof(JwtAudience)].Value = JwtAudience.ToString();
            config.AppSettings.Settings[nameof(JwtExpiryInDays)].Value = JwtExpiryInDays.ToString();
            config.AppSettings.Settings[nameof(JwtSecurityKey)].Value = JwtSecurityKey.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);


        }
    }
}
