using Model.Helpers;
using System.Text;

namespace Migrations.Helpers.Services
{
    public class ConsoleService
    {
        private int _quantidadeMigrations;
        private readonly LogService _logService;

        public ConsoleService() 
        {
            _logService = new LogService();
            IniciarConfiguracoesIniciais();
        }

        #region "Métodos Públicos"

        public void MontarMensagemInicioMigration()
        {
            MontaMensagemInicioMigrationConsole();
            MontaMensagemInicioMigrationLog();
        }

        public void MensagemAplicandoMigration(string Versao)
        {
            MensagemAplicandoMigrationConsole(Versao);
            MensagemAplicandoMigrationLog(Versao);
        }

        public void MensagemMigrationAplicada(string Versao)
        {
            MensagemMigrationAplicadaConsole(Versao);
            MensagemMigrationAplicadaLog(Versao);
            ContarMigrationsAtualizadas();
        }

        public void MontarMensagemFimMigration()
        {
            MontarMensagemFimMigrationConsole();
            MontarMensagemFimMigrationLog();
        }

        public void TotalizadorMigrations()
        {
            TotalizadorMigrationsConsole();
            TotalizadorMigrationsLog();
        }

        #endregion

        #region "Métodos Privado"

        private void IniciarConfiguracoesIniciais()
        {
            Console.OutputEncoding = Encoding.UTF8;
            _quantidadeMigrations = 0;
        }

        private void MontaMensagemInicioMigrationLog()
        {
            _logService.IncluirLog(Mensagens.LINHA_PONTILHADA);
            _logService.IncluirLog(Mensagens.TITULO_APLICACAO);
            _logService.IncluirLog(Mensagens.LINHA_PONTILHADA);
            _logService.IncluirLog(Mensagens.NOME_BANCO_DADOS);
            _logService.IncluirLog(Mensagens.LINHA_PONTILHADA);
            _logService.IncluirLog(Mensagens.INICIO_ATUALIZACAO);
        }

        private static void MontaMensagemInicioMigrationConsole()
        {
            Console.WriteLine(Mensagens.LINHA_PONTILHADA);
            Console.WriteLine(Mensagens.TITULO_APLICACAO);
            Console.WriteLine(Mensagens.NOME_BANCO_DADOS);
            Console.WriteLine(Mensagens.LINHA_PONTILHADA);
            Console.WriteLine(Mensagens.INICIO_ATUALIZACAO);
            Console.WriteLine();
        }

        private void MontarMensagemFimMigrationLog()
        {
            _logService.IncluirLog(Mensagens.LINHA_PONTILHADA);
            _logService.IncluirLog(Mensagens.ATUALIZACAO_CONCLUIDA);
            _logService.IncluirLog(Mensagens.LINHA_PONTILHADA);
        }

        private static void MontarMensagemFimMigrationConsole()
        {
            Console.WriteLine(Mensagens.LINHA_PONTILHADA);
            Console.WriteLine(Mensagens.ATUALIZACAO_CONCLUIDA);
            Console.WriteLine(Mensagens.LINHA_PONTILHADA);
            Console.WriteLine(Mensagens.FINALIZAR);
            Console.ReadLine();
            Environment.Exit(0);
        }

        private void TotalizadorMigrationsConsole()
        {
            Console.WriteLine(Mensagens.LINHA_PONTILHADA);
            Console.WriteLine(Mensagens.FIM_ATUALIZACAO);
            Console.WriteLine();
            Console.WriteLine(Mensagens.LINHA_PONTILHADA);
            Console.WriteLine($"{Mensagens.QUANTIDADE_REGISTROS}{_quantidadeMigrations}");
        }

        private void TotalizadorMigrationsLog()
        {
            _logService.IncluirLog(Mensagens.FIM_ATUALIZACAO);
            _logService.IncluirLog(Mensagens.LINHA_PONTILHADA);
            _logService.IncluirLog($"{Mensagens.QUANTIDADE_REGISTROS}{_quantidadeMigrations}");
        }

        private void ContarMigrationsAtualizadas()
        {
            _quantidadeMigrations += 1;
        }

        private static void MensagemMigrationAplicadaConsole(string Versao)
        {
            Console.WriteLine($"{Mensagens.MIGRATION_APLICADA}{Versao}");
        }

        private void MensagemMigrationAplicadaLog(string Versao)
        {
            _logService.IncluirLog($"{Mensagens.MIGRATION_APLICADA}{Versao}");
        }

        public static void MensagemAplicandoMigrationConsole(string Versao)
        {
            Console.WriteLine($"{Mensagens.APLICANDO_MIGRATION}{Versao}");
        }

        public void MensagemAplicandoMigrationLog(string Versao)
        {
            _logService.IncluirLog($"{Mensagens.APLICANDO_MIGRATION}{Versao}");
        }

        #endregion

    }
}
