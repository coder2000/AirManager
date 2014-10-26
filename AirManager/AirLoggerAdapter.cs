// Copyright 2014 Dieter Lunn All Rights Reserved

using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Prism.Logging;

namespace AirManager
{
    public class AirLoggerAdapter : ILoggerFacade
    {
        public AirLoggerAdapter()
        {
            var factory = new LogWriterFactory();
            LogWriter writer = factory.Create();
            Logger.SetLogWriter(writer);
        }

        public void Log(string message, Category category, Priority priority)
        {
            Logger.Write(message, category.ToString(), (int) priority);
        }
    }
}