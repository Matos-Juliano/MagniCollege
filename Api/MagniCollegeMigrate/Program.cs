using MagniCollege.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;

namespace MagniCollegeMigrate
{
    public class Program
    {
        static int Main(string[] args)
        {
            List<ConnectionStrings> tenants = DbInitializator.GetConfiguredStrings();

            int exitCode = ExecuteMigrations(tenants);

            DbInitializator.DbInit();

            Console.WriteLine("ExitCode is:" + exitCode);

            return exitCode;
        }

        private static int ExecuteMigrations(List<ConnectionStrings> tenants)
        {
            int exitCode = 0;
            try
            {
                tenants.ForEach(x =>
                {
                    MigrateTenantDatabase(x);
                });
            }catch(Exception e)
            {
                exitCode = 1;
            }

            return exitCode;
        }

        private static void MigrateTenantDatabase(ConnectionStrings x)
        {
            var dbMigrator = new MigratorLoggingDecorator(
                new DbMigrator(new Configuration
                {
                    TargetDatabase = new System.Data.Entity.Infrastructure.DbConnectionInfo(x.ConnectionString, "System.Data.SqlClient")
                }),
                new EfLogger(x));

            try
            {
                dbMigrator.Update();
            }
            catch
            {
                throw;
            }
        }        
    }
}
