using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Interface;

namespace Model.Migrations
{
    public class Migration000002 : IMigration
    {

        public string Versao => "000002";

        public async Task AtualizarAsync(AppDbContext context)
        {
            if (!context.TabelaExiste("Niveis"))
            {

                // Se não existir, cria a tabela
                string sql = @"CREATE TABLE Niveis (Id INT AUTO_INCREMENT PRIMARY KEY, Descricao VARCHAR(100) NOT NULL)";
                await context.Database.ExecuteSqlRawAsync(sql);

            }

        }

        
    }
}
