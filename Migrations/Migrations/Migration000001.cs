using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Interface;

namespace Model.Migrations
{
    public class Migration000001 : IMigration
    {
        public string Versao => "000001";

        public async Task AtualizarAsync(AppDbContext context)
        {
            if (!context.TabelaExiste("MigrationHistoricoAtualizacoes"))
            {
                string sql = @"CREATE TABLE MigrationHistoricoAtualizacoes (
                                         Id INT AUTO_INCREMENT PRIMARY KEY,
                                     Versao VARCHAR(10) UNIQUE NOT NULL,
                            DataAtualizacao DATETIME NOT NULL )";

                await context.Database.ExecuteSqlRawAsync(sql);
            }
        }
    }
}
