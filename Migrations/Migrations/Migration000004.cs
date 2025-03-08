using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Interface;

namespace Model.Migrations
{
    public class Migration000004 : IMigration
    {

        public string Versao => "000004";

        public  async Task AtualizarAsync(AppDbContext context)
        {
            if (context.TabelaExiste("Niveis"))
            {

                string sql = @"INSERT INTO Niveis (Descricao) VALUES 
                                      ('Administrador'), 
                                      ('Profissional'), 
                                      ('Comum')";
                await context.Database.ExecuteSqlRawAsync(sql);
            }
        }

    }
}
