using Microsoft.Extensions.Configuration;

namespace Common
{
    public static class AppConfig
    {
        public static IConfigurationRoot Configuration { get; }

        static AppConfig()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static string AdminEmail => Configuration["AdminAccount:Email"];
        public static string AdminPassword => Configuration["AdminAccount:Password"];
        public static string UserEmail => Configuration["UserAccount:Email"];
        public static string UserPassword => Configuration["UserAccount:Password"];
    }
}
