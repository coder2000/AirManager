using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;

namespace AirManager.Infrastructure.Seeder
{
    public static class CsvSeeder
    {
        static CsvSeeder()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainOnAssemblyResolve;
        }

        private static Assembly CurrentDomainOnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return new AssemblyName(args.Name).Name == "AirManager.Infrastructure" ? typeof (CsvSeeder).Assembly : null;
        }

        public static void SeedFromStream<T>(this IDbSet<T> dbSet, Stream stream,
            Expression<Func<T, object>> identifierExpression, params CsvColumnMapping<T>[] additionalMappings)
            where T : class
        {
            try
            {
                using (var reader = new StreamReader(stream))
                {
                    var csvReader = new CsvReader(reader);
                    CsvClassMap map = csvReader.Configuration.AutoMap<T>();
                    map.ReferenceMaps.Clear();

                    csvReader.Configuration.RegisterClassMap(map);
                    csvReader.Configuration.WillThrowOnMissingField = false;

                    while (csvReader.Read())
                    {
                        var entity = csvReader.GetRecord<T>();
                        foreach (var additionalMapping in additionalMappings)
                        {
                            additionalMapping.Execute(entity, csvReader.GetField(additionalMapping.CsvColumnName));
                        }

                        try
                        {
                            dbSet.AddOrUpdate(identifierExpression, entity);
                        }
                        catch (Exception exception)
                        {
                            string message = string.Format("Error adding {0} to column {1}", entity,
                                identifierExpression);
                            throw new Exception(message, exception);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                string message = string.Format("Error seeding dbSet<{0}>: {1}",
                    dbSet.GetType().GenericTypeArguments[0].FullName, exception);
                Exception innerException = exception.GetType().IsSerializable ? exception : null;

                throw new Exception(message, innerException);
            }
        }

        public static void SeedFromFile<T>(this IDbSet<T> dbSet, string fileName,
            Expression<Func<T, object>> identifierExpression, params CsvColumnMapping<T>[] additionalMapping)
            where T : class
        {
            using (Stream stream = File.OpenRead(fileName))
            {
                SeedFromStream(dbSet, stream, identifierExpression, additionalMapping);
            }
        }

        public static void SeedFromResource<T>(this IDbSet<T> dbSet, string embeddedResourceName,
            Expression<Func<T, object>> identifierExpression, params CsvColumnMapping<T>[] additionalMapping)
            where T : class
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(embeddedResourceName))
            {
                SeedFromStream(dbSet, stream, identifierExpression, additionalMapping);
            }
        }
    }
}