using Microsoft.EntityFrameworkCore;
using Migrations.Helpers.Configuracao;
using Model.Model;


namespace Model.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<MigrationHistoricoAtualizacao> MigrationHistoricoAtualizacoes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(ConfiguracaoBD.ObterStringConexao(), new MySqlServerVersion(new Version(8, 0, 33)));
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Se necessário, configure o nome da tabela ou outras propriedades
            modelBuilder.Entity<MigrationHistoricoAtualizacao>().ToTable("migrationhistoricoatualizacoes");
        }

        public bool TabelaExiste(string nomeTabela)
        {
            var connection = Database.GetDbConnection();

            bool tabelaExiste = false;

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open(); // Evita erro ao tentar abrir uma conexão já aberta

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"SHOW TABLES LIKE '{nomeTabela}'";
                    using (var reader = command.ExecuteReader())
                    {
                        tabelaExiste = reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao verificar tabela: {ex.Message}");
            }

            return tabelaExiste;
        }
    }
}
