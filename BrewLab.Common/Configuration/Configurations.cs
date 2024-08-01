using dotenv.net;
using Microsoft.Extensions.Configuration;

namespace BrewLab.Common.Configuration;
public static class Configs
{
    private static IConfiguration BuildConfiguration()
    {
        DotEnv.Fluent()
            .Load();

        return new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();
    }

    public static Database Database
    {
        get
        {
            var _config = BuildConfiguration();
            var configs = _config.Get<Database>();

            if (configs is null) throw new ArgumentNullException(nameof(configs));

            configs.Verify();

            return configs;
        }
    }

    public static JWTConfig JWT
    {
        get
        {
            var _config = BuildConfiguration();
            var configs = _config.Get<JWTConfig>();

            if (configs is null) throw new ArgumentNullException(nameof(configs));

            configs.Verify();

            return configs;
        }
    }


}