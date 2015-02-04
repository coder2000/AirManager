using System;

namespace AirManager.Infrastructure.Seeder
{
    public class CsvColumnMapping<T>
    {
        private readonly Action<T, object> _action;
        private readonly string _csvColumnName;

        public CsvColumnMapping(string csvColumnName, Action<T, object> action)
        {
            _csvColumnName = csvColumnName;
            _action = action;
        }

        public string CsvColumnName
        {
            get { return _csvColumnName; }
        }

        public void Execute(T entity, object csvColumnValue)
        {
            _action(entity, csvColumnValue);
        }
    }
}