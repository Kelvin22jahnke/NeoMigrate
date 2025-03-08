using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Interface;

namespace Model.Migrations
{
    public class Migration000003 : IMigration
    {

        public string Versao => "000003";

        public  async Task AtualizarAsync(AppDbContext context)
        {
            if (!context.TabelaExiste("Usuarios"))
            {

                // Se não existir, cria a tabela
                string sql = @"CREATE TABLE Usuarios 
                                        ( Id INT AUTO_INCREMENT PRIMARY KEY,
                                        Nome VARCHAR(100) NOT NULL,
                                       Email VARCHAR(100) NOT NULL UNIQUE,
                                       Senha VARCHAR(100) NOT NULL,
                                     IdNivel INT,
                                 FOREIGN KEY (IdNivel) REFERENCES Niveis(Id))";

                await context.Database.ExecuteSqlRawAsync(sql);
            }

        }
    }
}
