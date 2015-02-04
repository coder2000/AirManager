using System;
using System.Runtime.Serialization;

namespace AirManager.Infrastructure.Seeder
{
    [Serializable]
    public class SeederException : Exception
    {
        public SeederException()
        {
        }

        public SeederException(string message) : base(message)
        {
        }

        public SeederException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SeederException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}