using Migrations.Helpers.Configuracao;

namespace Model.Helpers
{
    public static class Mensagens
    {
        public static string NOME_BANCO_DADOS = $"📌 Banco de Dados MySQL: {ConfiguracaoBD.ObterNomeBaseDados()}";
        public static string INICIO_ATUALIZACAO = $"⏳ Início da Atualização: {DateTime.Now:dd/MM/yyyy: HH:mm}";
        public static string FIM_ATUALIZACAO = $"⏳ Fim da Atualização: {DateTime.Now:dd/MM/yyyy: HH:mm}";
        public static string LINHA_PONTILHADA = new string('.', 80);
        public static string TITULO_APLICACAO = $"🔄 Iniciando Migrations no NeoMigrate | Versão: {ConfiguracaoSistema.ObterVersaoSistema()}";
        public const string APLICANDO_MIGRATION = $"🚀 Aplicando Migration versão: ";
        public const string MIGRATION_APLICADA = $"✅ Migration aplicada com sucesso versão: ";
        public const string ATUALIZACAO_CONCLUIDA = "✅ Todas as migrations foram aplicadas!";
        public const string QUANTIDADE_REGISTROS = $"🔄 Quantidade de Atualizações executadas no Banco de Dados: ";
        public const string NENHUMA_MIGRACAO_PENDENTE = "✅ Nenhuma migração pendente.";
        public const string FINALIZAR = "\n🔘 Pressione ENTER para sair e fechar...";
        public const string ATUALIZACOES_FINALIZADAS = "📌 Atualizações finalizadas.";

    }
}
