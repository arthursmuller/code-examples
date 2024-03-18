﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Notifications.Infraestructure.Persistence
{
    public class NotificationsContextFactory : IDesignTimeDbContextFactory<NotificationsContext>
    {
        private static string _connectionString;

        public NotificationsContext CreateDbContext()
            => new NotificationsContext(null);

        public NotificationsContext CreateDbContext(string[] args)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<NotificationsContext>();

            var version = new MariaDbServerVersion(new Version(10, 6, 11));
            builder.UseMySql(_connectionString, version)
                    .EnableSensitiveDataLogging();

            return new NotificationsContext(builder.Options);
        }

        private static void LoadConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath($"{Path.Combine(Directory.GetCurrentDirectory())}")
                .AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();
            _connectionString = configuration.GetConnectionString("NotificationsLocalDb");
        }
    }
}
