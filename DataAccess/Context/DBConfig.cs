namespace DataAccess.Context.Common
{
    public static class DBConfig
    {
        public static string ConnectionString { get; private set; }

        static DBConfig()
        {
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            //var configuration = builder.Build();
            //ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
