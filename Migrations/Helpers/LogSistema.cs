using System.Text;

namespace Model.Helpers
{
    public class LogSistema
    {

        private string _logFilePath = string.Empty;
        // Obtém o diretório onde está rodando o executável
        private string systemPath = AppDomain.CurrentDomain.BaseDirectory;
       
        public LogSistema()
        {
            string logFolder = Path.Combine(systemPath, "Logs");
           _logFilePath = Path.Combine(logFolder, $"MigrationLog_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm")}.log");

            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }
        }

        public void IncluirLogMigration( string message)
        {
            using (StreamWriter writer = new StreamWriter(_logFilePath, true, new UTF8Encoding(false)))
            {
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
        }

    }
}
