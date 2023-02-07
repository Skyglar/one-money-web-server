
using common.ResourceNames;
using Microsoft.Extensions.Configuration;

namespace common {
    public static class AppSettingsConfigurationManager {
        public static void SetAppSettingsProperties(IConfiguration configuration) {
            LocalDatabaseConnectionString = configuration.GetConnectionString(ConnectionStringNames.Local);

            DatabaseName = configuration.GetConnectionString(ConnectionStringNames.DatabaseName);
        }

        public static string LocalDatabaseConnectionString { get; private set; }

        public static string DatabaseName { get; private set; }
    }
}
