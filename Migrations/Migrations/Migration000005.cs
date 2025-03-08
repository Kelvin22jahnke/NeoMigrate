using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Interface;
using MySqlConnector;

namespace Migrations.Migrations
{
    public class Migration000005 : IMigration
    {
        public string Versao => "000005";

        public async Task AtualizarAsync(AppDbContext context)
        {
            if (context.TabelaExiste("Usuarios"))
            {
                string senha = CriptografarSenha("adm2025");

                // Insere o Usuário ADM de forma segura com parâmetros
                string sql = @"INSERT INTO Usuarios (Nome, Email, Senha, IdNivel)
                                    VALUES (@Nome, @Email, @Senha, 
                                            (SELECT Id FROM Niveis WHERE Descricao = 'Administrador'))";

                // Usando parâmetros para evitar SQL Injection
                await context.Database.ExecuteSqlRawAsync(sql,
                    new MySqlParameter("@Nome", "Administrador"),
                    new MySqlParameter("@Email", "kelvin18jahnke@gmail.com"),
                    new MySqlParameter("@Senha", senha));
            }
        }

        private string CriptografarSenha(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }
    }
}
