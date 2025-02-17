using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Model;
using Model.Interface;
using System.Reflection;
using Migrations.Helpers.Services;
using Model.Helpers;
using System.Text;

namespace Model
{
    public class MigrationBase
    {
        private readonly AppDbContext _context;
        private readonly ConsoleService _consoleService;
        private readonly List<string> _mensagensConsole = new List<string>(); // Lista para mensagens do console
  
        public MigrationBase(AppDbContext appDbContext, ConsoleService consoleService)
        {
            _context = appDbContext;
            _consoleService = consoleService;
            _mensagensConsole.Clear();
        }

        public async Task AplicarMigrations()
        {

            HashSet<string>? ultimaVersaoAtualizada = new HashSet<string> { "000000" };
           
            _mensagensConsole.AddRange(_consoleService.MontarMensagemInicioMigration());

            //Garante que o banco exista
            await _context.Database.MigrateAsync();

            // Verifica se a tabela de histórico existe antes de acessar o banco
            bool tabelaExiste = ExisteTabelaHistoricoAtualizacoes();

            if (tabelaExiste)
            {
                ultimaVersaoAtualizada = [.. _context.MigrationHistoricoAtualizacoes.Select(m => m.Versao)];
            }

            List<IMigration> listaMigrations = ObterListaDeAtualizacoes();

            // Se não há migrações pendentes, exibir mensagem e sair
            if (!listaMigrations.Any(m => !ultimaVersaoAtualizada.Contains(m!.Versao)))
            {
                _mensagensConsole.Add(Mensagens.LINHA_PONTILHADA);
                _mensagensConsole.Add(Mensagens.NENHUMA_MIGRACAO_PENDENTE);
                _mensagensConsole.AddRange(_consoleService.MontarMensagemFimMigration());

                ImprimirMensagens();
                return;
            }

            foreach (var migration in listaMigrations)
            {
                if (!ultimaVersaoAtualizada.Contains(migration!.Versao))
                {
                    
                    try
                    {
                        await AplicarMigrationAsync(migration);
                    }
                    catch (DbUpdateException dbEx)
                    {
                        Console.WriteLine("Erro ao atualizar o banco:");
                        Console.WriteLine(dbEx.InnerException?.Message); // Verifique a mensagem interna
                        Console.WriteLine(dbEx.Message); // A mensagem principal do erro
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao salvar: {ex.Message}");
                    }
                }
            }

            _mensagensConsole.AddRange(_consoleService.TotalizadorMigrations());
            _mensagensConsole.AddRange(_consoleService.MontarMensagemFimMigration());

            ImprimirMensagens();
        }

        #region "Métodos Privados"
        private bool ExisteTabelaHistoricoAtualizacoes()
        {
            try
            {
                return _context.TabelaExiste("MigrationHistoricoAtualizacoes");
            }
            catch
            {
                return false; // Se a consulta falhar, assumimos que a tabela não existe
            }
        }

        private List<IMigration> ObterListaDeAtualizacoes()
        {

            var listaMigrations = Assembly.GetExecutingAssembly()
                                       .GetTypes()
                                       .Where(w => typeof(IMigration).IsAssignableFrom(w) && !w.IsInterface)
                                       .Select(w => Activator.CreateInstance(w) as IMigration)
                                       .OrderBy(o => o!.Versao).ToList();

            return listaMigrations!;
        }

        private async Task AplicarMigrationAsync(IMigration migration)
        {
            _mensagensConsole.AddRange(_consoleService.MensagemAplicandoMigration(migration.Versao));

            await migration.AtualizarAsync(_context);

            // Adiciona o histórico de migração no banco
            var migrationHistoricoAtualizacao = new MigrationHistoricoAtualizacao
            {
                Versao = migration.Versao,
                DataAtualizacao = DateTime.Now
            };

            await _context.MigrationHistoricoAtualizacoes.AddAsync(migrationHistoricoAtualizacao);
            await _context.SaveChangesAsync();

            _mensagensConsole.AddRange(_consoleService.MensagemMigrationAplicada(migration.Versao));
        }

        private void LogError(string message, Exception ex)
        {
            Console.WriteLine(message);
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
        }

        private void ImprimirMensagens()
        {
            // Exibir mensagens no console de forma otimizada
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("\n=== LOG DE MIGRAÇÕES ===");
            Console.WriteLine(string.Join("\n", _mensagensConsole));
            Console.WriteLine(Mensagens.LINHA_PONTILHADA);
            Console.WriteLine("\n🔚 Processo concluído.");
            Console.WriteLine(Mensagens.FINALIZAR);
            Console.ReadLine();
            Environment.Exit(0);
        }

        #endregion
    }
}
