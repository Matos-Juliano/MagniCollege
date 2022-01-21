using System;
using System.Data.Entity.Migrations.Infrastructure;

namespace MagniCollegeMigrate
{
    internal sealed class EfLogger : MigrationsLogger
    {
        private readonly ConnectionStrings _connectionStrings;

        public EfLogger(ConnectionStrings tenantInfo) => _connectionStrings = tenantInfo;

        public override void Info(string message) => Console.WriteLine($"{_connectionStrings.Name}: {message}");

        public override void Verbose(string message) { /* no op */ }

        public override void Warning(string message) => Console.WriteLine($"{_connectionStrings.Name}: {message}");
    }

}
