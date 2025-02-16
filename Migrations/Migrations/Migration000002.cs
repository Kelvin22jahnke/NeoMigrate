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
            if (!context.TabelaExiste("Usuarios"))
            {
                string sql = @"CREATE TABLE Usuarios (
                                         Id INT AUTO_INCREMENT PRIMARY KEY,
                                       Nome NVARCHAR(100) NOT NULL,
                                     Email VARCHAR(100) UNIQUE NOT NULL )";

                await context.Database.ExecuteSqlRawAsync(sql);
            }

        }

    }
}
