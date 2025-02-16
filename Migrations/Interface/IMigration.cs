using Model.Data;

namespace Model.Interface
{
    public interface IMigration
    {
        string Versao { get; }
        Task AtualizarAsync(AppDbContext context);
    }
}
