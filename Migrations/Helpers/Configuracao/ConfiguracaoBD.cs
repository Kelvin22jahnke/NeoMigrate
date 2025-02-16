using Microsoft.Extensions.Configuration;

namespace Migrations.Helpers.Configuracao
{
    public static class ConfiguracaoBD
    {
        private static readonly IConfiguration _configuration;

        static ConfiguracaoBD()
        {
            _configuration = ConfiguracaoSistema.ObterConfiguracao();
        }

        public static string ObterStringConexao()
        {
            return _configuration["ConnectionStrings:MySQL"]!;
        }

        public static string ObterNomeBaseDados()
        {
            return _configuration["DatabaseSettings:DatabaseName"] ?? "default_db";
        }
    }
}
