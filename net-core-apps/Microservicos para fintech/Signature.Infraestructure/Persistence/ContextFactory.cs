using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Signature.Infraestructure.Persistence
{
    public class SignatureContextFactory : IDesignTimeDbContextFactory<SignatureContext>
    {
        private static string _connectionString;

        public SignatureContext CreateDbContext()
            => new SignatureContext(null);

        public SignatureContext CreateDbContext(string[] args)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<SignatureContext>();

            var version = new MariaDbServerVersion(new Version(10, 6, 11));
            builder.UseMySql(_connectionString, version)
                    .EnableSensitiveDataLogging();

            return new SignatureContext(builder.Options);
        }

        private static void LoadConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath($"{Path.Combine(Directory.GetCurrentDirectory())}")
                .AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();
            _connectionString = configuration.GetConnectionString("SignatureLocalDb");
        }
    }
}
