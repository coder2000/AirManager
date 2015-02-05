// Copyright 2014 Dieter Lunn All Rights Reserved

using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Prism.Logging;

namespace AirManager
{
    /// <summary>
    /// 
    /// </summary>
    public class AirLoggerAdapter : ILoggerFacade
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AirLoggerAdapter"/> class.
        /// </summary>
        public AirLoggerAdapter()
        {
            var factory = new LogWriterFactory();
            LogWriter writer = factory.Create();
            Logger.SetLogWriter(writer);
        }

        /// <summary>
        /// Write a new log entry with the specified category and priority.
        /// </summary>
        /// <param name="message">Message body to log.</param>
        /// <param name="category">Category of the entry.</param>
        /// <param name="priority">The priority of the entry.</param>
        public void Log(string message, Category category, Priority priority)
        {
            Logger.Write(message, category.ToString(), (int) priority);
        }
    }
}