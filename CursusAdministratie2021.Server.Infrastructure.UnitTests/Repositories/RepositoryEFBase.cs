using CursusAdministratie2021.Server.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Data.Common;

namespace CursusAdministratie2021.Server.Infrastructure.UnitTests.Repositories {
    public class RepositoryEFBase : IDisposable {
        private readonly DbConnection _connection;
        public RepositoryEFBase() {
            ContextOptions = new DbContextOptionsBuilder<CoursesAdministrationDbContext>()
                    .UseSqlite(CreateInMemoryDatabase())
                    .Options;
            _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
            //Seed();
        }
        private static DbConnection CreateInMemoryDatabase() {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        public void Dispose() => _connection.Dispose();
        protected DbContextOptions<CoursesAdministrationDbContext> ContextOptions { get; }
    }
}
