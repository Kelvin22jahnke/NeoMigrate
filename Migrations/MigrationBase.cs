using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Model;
using Model.Interface;
using System.Reflection;
using Migrations.Helpers.Services;

namespace Model
{
    public class MigrationBase
    {
        private readonly AppDbContext _context;
        private readonly ConsoleService _consoleService;

        public MigrationBase(AppDbContext appDbContext, ConsoleService consoleService)
        {
            _context = appDbContext;
            _consoleService  = consoleService;
        }

        public async Task AplicarMigrations()
        {

            HashSet<string>? ultimaVersaoAtualizada = new HashSet<string> { "000000" };
            
            //Garante que o banco exista
            await _context.Database.MigrateAsync();

            if (ExisteTabelaHistoricoAtualizacoes())
            {
                ultimaVersaoAtualizada = [.. _context.MigrationHistoricoAtualizacoes.Select(m => m.Versao)];
            }

            List<IMigration> listaMigrations = ObterListaDeAtualizacoes();

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

            _consoleService.TotalizadorMigrations();
        }

        #region "Métodos Privados"
        private bool ExisteTabelaHistoricoAtualizacoes()
        {
            return _context.TabelaExiste("MigrationHistoricoAtualizacoes");
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
            _consoleService.MensagemAplicandoMigration(migration.Versao);
            await migration.AtualizarAsync(_context);

            // Adiciona o histórico de migração no banco
            var migrationHistoricoAtualizacao = new MigrationHistoricoAtualizacao
            {
                Versao = migration.Versao,
                DataAtualizacao = DateTime.Now
            };

            await _context.MigrationHistoricoAtualizacoes.AddAsync(migrationHistoricoAtualizacao);
            await _context.SaveChangesAsync();

            _consoleService.MensagemMigrationAplicada(migration.Versao);
        }

        private void LogError(string message, Exception ex)
        {
            Console.WriteLine(message);
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
        }

        #endregion
    }
}
