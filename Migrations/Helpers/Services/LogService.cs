using System.Text;

namespace Migrations.Helpers.Services
{
    public class LogService
    {
        private string _logFilePath = string.Empty;
        private string logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

        public LogService()
        {
            CriarPastaLogs();
            CriarArquivoLog();
        }

        #region "Métodos Públicos"
        public void IncluirLog(string message)
        {
            using (StreamWriter writer = new StreamWriter(_logFilePath, true, new UTF8Encoding(false)))
            {
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
        }
        #endregion

        #region "Métodos Privados"

        private void CriarPastaLogs()
        {
            if (!Directory.Exists(logFolder))
                Directory.CreateDirectory(logFolder);
        }

        private void CriarArquivoLog()
        {
            _logFilePath = Path.Combine(logFolder, $"MigrationLog_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm")}.log");
        }

        #endregion

    }
}
