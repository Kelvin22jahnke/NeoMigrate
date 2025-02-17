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
            MontarMensagem(Mensagens.LINHA_PONTILHADA);
            MontarMensagem(Mensagens.TITULO_APLICACAO);
            MontarMensagem(Mensagens.LINHA_PONTILHADA);
            MontarMensagem(Mensagens.NOME_BANCO_DADOS);
            MontarMensagem(Mensagens.LINHA_PONTILHADA);
            MontarMensagem(Mensagens.INICIO_ATUALIZACAO);
            Console.WriteLine();
        }

        public void MensagemAplicandoMigration(string Versao)
        {
            MontarMensagem($"{Mensagens.APLICANDO_MIGRATION}{Versao}");
        }

        public void MensagemMigrationAplicada(string Versao)
        {
            MontarMensagem($"{Mensagens.MIGRATION_APLICADA}{Versao}");
            ContarMigrationsAtualizadas();
        }

        public void MontarMensagemFimMigration()
        {
            MontarMensagem(Mensagens.LINHA_PONTILHADA);
            MontarMensagem(Mensagens.ATUALIZACAO_CONCLUIDA);
            MontarMensagem(Mensagens.LINHA_PONTILHADA);
            MontarMensagem(Mensagens.FINALIZAR);
            Console.ReadLine();
            Environment.Exit(0);
        }

        public void TotalizadorMigrations()
        {
            MontarMensagem(Mensagens.LINHA_PONTILHADA);
            MontarMensagem(Mensagens.FIM_ATUALIZACAO);
            Console.WriteLine();
            MontarMensagem(Mensagens.LINHA_PONTILHADA);
            MontarMensagem($"{Mensagens.QUANTIDADE_REGISTROS}{_quantidadeMigrations}");
        }

        #endregion

        #region "Métodos Privado"

        private void IniciarConfiguracoesIniciais()
        {
            Console.OutputEncoding = Encoding.UTF8;
            _quantidadeMigrations = 0;
        }

        private void ContarMigrationsAtualizadas()
        {
            _quantidadeMigrations ++;
        }

        private void MontarMensagem(string mensagem)
        {
            _logService.IncluirLog(mensagem);
            Console.WriteLine(mensagem);
        }

        #endregion

    }
}
