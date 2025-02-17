using Model.Helpers;

namespace Migrations.Helpers.Services
{
    public class ConsoleService
    {
        private int _quantidadeMigrations;
        private readonly LogService _logService;
        private readonly List<string> _listaMensagensConsole = new List<string>(); // Lista para mensagens de console

        public ConsoleService() 
        {
            _logService = new LogService();
            IniciarConfiguracoesIniciais();
        }

        #region "Métodos Públicos"

        public List<string> MontarMensagemInicioMigration()
        {
            _listaMensagensConsole.Clear();

            MontarMensagem(Mensagens.LINHA_PONTILHADA);
            MontarMensagem(Mensagens.TITULO_APLICACAO);
            MontarMensagem(Mensagens.LINHA_PONTILHADA);
            MontarMensagem(Mensagens.NOME_BANCO_DADOS);
            MontarMensagem(Mensagens.LINHA_PONTILHADA);
            MontarMensagem(Mensagens.INICIO_ATUALIZACAO);

            return _listaMensagensConsole;
        }

        public List<string> MensagemAplicandoMigration(string Versao)
        {
            _listaMensagensConsole.Clear();

            MontarMensagem($"{Mensagens.APLICANDO_MIGRATION}{Versao}");

            return _listaMensagensConsole;
        }

        public List<string> MensagemMigrationAplicada(string Versao)
        {
            _listaMensagensConsole.Clear();

            MontarMensagem($"{Mensagens.MIGRATION_APLICADA}{Versao}");
            ContarMigrationsAtualizadas();

            return _listaMensagensConsole;
        }

        public List<string> MontarMensagemFimMigration()
        {
            _listaMensagensConsole.Clear();

            MontarMensagem(Mensagens.LINHA_PONTILHADA);
            MontarMensagem(Mensagens.ATUALIZACAO_CONCLUIDA);
            MontarMensagem(Mensagens.LINHA_PONTILHADA);
            MontarMensagem(Mensagens.ATUALIZACOES_FINALIZADAS);

            return _listaMensagensConsole;
        }

        public List<string> TotalizadorMigrations()
        {
            _listaMensagensConsole.Clear();

            MontarMensagem(Mensagens.LINHA_PONTILHADA);
            MontarMensagem(Mensagens.FIM_ATUALIZACAO);
            MontarMensagem(Mensagens.LINHA_PONTILHADA);
            MontarMensagem($"{Mensagens.QUANTIDADE_REGISTROS}{_quantidadeMigrations}");

            return _listaMensagensConsole;
        }

        #endregion

        #region "Métodos Privado"

        private void IniciarConfiguracoesIniciais()
        {
            _quantidadeMigrations = 0;
        }

        private void ContarMigrationsAtualizadas()
        {
            _quantidadeMigrations ++;
        }

        private void MontarMensagem(string mensagem)
        {
            _logService.IncluirLog(mensagem);
            _listaMensagensConsole.Add(mensagem);
        }

        #endregion

    }
}
