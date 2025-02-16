using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Model;
using Model.Helpers;
using Model.Interface;
using System.Reflection;

namespace Model
{
    public class MigrationBase
    {
        private readonly AppDbContext _context;

        public MigrationBase(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task AplicarMigrations()
        {

            HashSet<string>? ultimaVersaoAtualizada = new HashSet<string> { "000000" };
            Mensagens.InicarContadorMigrationsAtualizadas();

            //Garante que o banco exista
            _context.Database.Migrate();

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

                        Mensagens.MensagemAplicandoMigration(migration.Versao);

                        await migration.AtualizarAsync(_context);

                        MigrationHistoricoAtualizacao migrationHistoricoAtualizacao = new MigrationHistoricoAtualizacao
                        {
                            Versao = migration.Versao,
                            DataAtualizacao = DateTime.Now
                        };

                        await _context.MigrationHistoricoAtualizacoes.AddAsync(migrationHistoricoAtualizacao);
                        await _context.SaveChangesAsync();

                        Mensagens.MensagemMigrationAplicada(migration.Versao);
                        Mensagens.ContarMigrationsAtualizadas();
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

            Mensagens.TotalizadorMigrations();
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
        #endregion
    }
}
