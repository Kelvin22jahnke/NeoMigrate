using System.Text;

namespace Model.Helpers
{
    public static class Mensagens
    {
        private static int quantidadeMigrations = 0;
        private static string logFilePath = $"MigrationLog-{DateTime.Now:dd-MM-yyyy: HH:mm}.txt";
        private static LogSistema _logSistema = new LogSistema();

        private static string LINHA_PONTILHADA = new string('.', 80);
        private const string TITULO_APLICACAO = "🔄 Iniciando Migrations no NeoMigrate | Versão: 1.0.1";
        private static string NOM_BANCO_DADOS = $"📌 Banco de Dados MySQL: {ConfiguracaoBD.ObterNomeBaseDados()}";
        private static string INICIO_ATUALIZACAO = $"⏳ Início da Atualização: {DateTime.Now:dd/MM/yyyy: HH:mm}";
        private const string ATUALIZACAO_CONCLUIDA = "✅ Todas as migrations foram aplicadas!";
        private static string FIM_ATUALIZACAO = $"⏳ Fim da Atualização: {DateTime.Now:dd/MM/yyyy: HH:mm}";
        private static string QUANTIDADE_REGISTROS = $"🔄 Quantidade de Atualizações executadas no Banco de Dados: {quantidadeMigrations}";

        public static void ConfigurarConsole()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }

        public static void MontarMensagemInicioMigration()
        {
            
            Console.WriteLine(LINHA_PONTILHADA);
            _logSistema.IncluirLogMigration(LINHA_PONTILHADA);
            _logSistema.IncluirLogMigration(TITULO_APLICACAO);
            Console.WriteLine(TITULO_APLICACAO);
            _logSistema.IncluirLogMigration(LINHA_PONTILHADA);
            _logSistema.IncluirLogMigration(NOM_BANCO_DADOS);
            Console.WriteLine(NOM_BANCO_DADOS);
            Console.WriteLine(LINHA_PONTILHADA);
            _logSistema.IncluirLogMigration(LINHA_PONTILHADA);
            _logSistema.IncluirLogMigration(INICIO_ATUALIZACAO);
            Console.WriteLine(INICIO_ATUALIZACAO);
            Console.WriteLine();
        }

        public static void MontarMensagemFimMigration()
        {
            Console.WriteLine(LINHA_PONTILHADA);
            _logSistema.IncluirLogMigration(LINHA_PONTILHADA);
            _logSistema.IncluirLogMigration(ATUALIZACAO_CONCLUIDA);
            Console.WriteLine(ATUALIZACAO_CONCLUIDA);
            _logSistema.IncluirLogMigration(LINHA_PONTILHADA);
            Console.WriteLine(LINHA_PONTILHADA);
            Console.WriteLine("\n🔘 Pressione ENTER para sair e fechar...");
            Console.ReadLine();
            Environment.Exit(0);
        }

        public static void MensagemAplicandoMigration(string Versao)
        {
            string mensagem = $"🚀 Aplicando Migration versão: {Versao}....";
            _logSistema.IncluirLogMigration(mensagem);
            Console.WriteLine(mensagem);
        }

        public static void MensagemMigrationAplicada(string Versao)
        {

            string mensagem = $"✅ Migration versão: {Versao} aplicada com sucesso!";
            _logSistema.IncluirLogMigration(mensagem);
            Console.WriteLine(mensagem);
        }

        public static void TotalizadorMigrations()
        {

            Console.WriteLine(LINHA_PONTILHADA);
            _logSistema.IncluirLogMigration(FIM_ATUALIZACAO);
            Console.WriteLine(FIM_ATUALIZACAO);
            Console.WriteLine();
            Console.WriteLine(LINHA_PONTILHADA);
            Console.WriteLine(QUANTIDADE_REGISTROS);
        }

        public static void ContarMigrationsAtualizadas()
        {
            quantidadeMigrations += 1;
        }

        public static void InicarContadorMigrationsAtualizadas()
        {
            quantidadeMigrations = 0;
        }

    }
}
