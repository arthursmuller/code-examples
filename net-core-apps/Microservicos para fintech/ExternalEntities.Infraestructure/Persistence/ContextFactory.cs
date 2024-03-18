using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ExternalEntities.Infraestructure.Persistence
{
    public class ExternalEntitiesContextFactory : IDesignTimeDbContextFactory<ExternalEntitiesContext>
    {
        private static string _connectionString;

        public ExternalEntitiesContext CreateDbContext()
            => new ExternalEntitiesContext(null);

        public ExternalEntitiesContext CreateDbContext(string[] args)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<ExternalEntitiesContext>();

            var version = new MariaDbServerVersion(new Version(10, 6, 11));
            builder.UseMySql(_connectionString, version)
                    .EnableSensitiveDataLogging();

            return new ExternalEntitiesContext(builder.Options);
        }

        private static void LoadConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath($"{Path.Combine(Directory.GetCurrentDirectory())}")
                .AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();
            _connectionString = configuration.GetConnectionString("ExternalEntitiesLocalDb");
        }
    }
}
