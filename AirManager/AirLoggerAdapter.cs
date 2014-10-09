using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Prism.Logging;

namespace AirManager
{
    public class AirLoggerAdapter : ILoggerFacade
    {
        public AirLoggerAdapter()
        {
            Logger.SetLogWriter(new LogWriter(new LoggingConfiguration()));
        }

        public void Log(string message, Category category, Priority priority)
        {
            Logger.Write(message, category.ToString(), (int) priority);
        }
    }
}