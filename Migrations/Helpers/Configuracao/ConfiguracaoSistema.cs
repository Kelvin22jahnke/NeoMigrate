using Microsoft.Extensions.Configuration;

namespace Migrations.Helpers.Configuracao
{
    public static class ConfiguracaoSistema
    {
        private static readonly IConfiguration _configuration;

        static ConfiguracaoSistema()
        {
            _configuration = InicializarConfiguracao();
        }

        public static IConfiguration ObterConfiguracao()
        {
            return _configuration;
        }

        public static string ObterVersaoSistema()
        {
            return _configuration["SystemConfig:Version"]!;
        }

        #region "Métodos Privados"
        private static IConfiguration InicializarConfiguracao()
        {
            return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()) // Define o diretório base
                                             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Carrega o appsettings.json
                                             .Build();

        }

        #endregion
    }
}
