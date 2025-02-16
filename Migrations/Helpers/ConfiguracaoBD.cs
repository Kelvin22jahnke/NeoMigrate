using Microsoft.EntityFrameworkCore;

namespace Model.Helpers
{
    public static class ConfiguracaoBD
    {
        private const string DATA_BASE = "Banco_Novo23";
        private const string STRING_CONEXAO_BANCO = $"server=localhost;database={DATA_BASE};user=root;password=;";

        public static string ObterStringConexao()
        {
            return STRING_CONEXAO_BANCO;
        }

        public static string ObterNomeBaseDados()
        {
            return DATA_BASE;
        }
    }
}
