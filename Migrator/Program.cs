using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Model.Data;
using Model;
using Migrations.Helpers.Services;
using Migrations.Helpers.Configuracao;

ConsoleService consoleService = new ConsoleService();

consoleService.MontarMensagemInicioMigration();

var serviceProvider = new ServiceCollection()
          .AddDbContext<AppDbContext>(optionts => optionts.UseMySql(ConfiguracaoBD.ObterStringConexao(), new MySqlServerVersion(new Version(8, 0, 33))))
          .BuildServiceProvider();

using var context = serviceProvider.GetRequiredService<AppDbContext>();

MigrationBase migrationBase = new MigrationBase(context);
await migrationBase.AplicarMigrations();

consoleService.MontarMensagemFimMigration();