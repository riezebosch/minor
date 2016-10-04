using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TddDemo.Model;
using Xunit;

namespace TddDemo
{
    public class EntityFrameworkDemo
    {
        public DbContextOptions DefaultOptions
        {
            get
            {
                var builder = new DbContextOptionsBuilder<SchoolContext>()
                    .UseSqlServer(@"Server=.\SQLEXPRESS;Database=School;Trusted_Connection=True");

                return builder.Options;
            }
        }

        [Fact]
        public void WatKanIkMetMigrationsAllemaalBereiken()
        {
            using (var context = new SchoolContext(DefaultOptions))
            using (context.Database.BeginTransaction())
            {
                var p = new Person
                {
                    FirstName = "TEMP",
                    LastName = "TEMP",
                    BirthDate = new DateTime(1984, 03, 05)
                };

                context.Person.Add(p);
                context.SaveChanges();
            }
        }

        [Fact]
        public void OnsiteAndOnlineCourseAreDerivedFromCourse()
        {
            using (var context = new SchoolContext(DefaultOptions))
            {
                Assert.True(context.Course.OfType<OnsiteCourse>().Any());
            }
        }

        [Fact]
        public void HoeKomIkAanDeQueryInCSharp()
        {
            using (var context = new SchoolContext(DefaultOptions))
            {
                // Arrange
                var log = new List<LogItem>();

                var serviceProvider = context.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new MyLoggerProvider(log.Add));

                var query = from p in context.Person
                            where p.FirstName == "Kim"
                            select p;

                // Act
                query.ToList();

                // Assert
                log.ShouldNotBeEmpty();
                log.ShouldContain(l => l.Message.Contains("SELECT"));
            }
        }

        [Fact]
        public void HoeMaakIkEenTransaction()
        {
            using (var context = new SchoolContext(DefaultOptions))
            using (var tx = context.Database.BeginTransaction())
            {
                context.Person.Add(new Person { FirstName = "Test", LastName = "Test" });
                context.SaveChanges();

                context
                    .Person
                    .Any(p => p.FirstName == "Test")
                    .ShouldBeTrue();
            }

            using (var context = new SchoolContext(DefaultOptions))
            {
                context
                    .Person
                    .Any(p => p.FirstName == "Test")
                    .ShouldBeFalse();
            }
        }

        /// <summary>
        /// https://docs.efproject.net/en/latest/saving/transactions.html#using-external-dbtransactions-relational-databases-only
        /// </summary>
        [Fact]
        public void HoeGebruikIkEenEigenTransaction()
        {
            using (var connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=School;Integrated Security=SSPI"))
            {
                connection.Open();

                using (var tx = connection.BeginTransaction())
                {
                    var options = new DbContextOptionsBuilder<SchoolContext>()
                        .UseSqlServer(connection)
                        .Options;

                    using (var context = new SchoolContext(options))
                    {
                        context.Database.UseTransaction(tx);
                        context.Person.Add(new Person { FirstName = "Test", LastName = "Test" });
                        context.SaveChanges();

                        context
                            .Person
                            .Any(p => p.FirstName == "Test")
                            .ShouldBeTrue();
                    }

                    using (var context = new SchoolContext(options))
                    {
                        context.Database.UseTransaction(tx);
                        context
                            .Person
                            .Any(p => p.FirstName == "Test")
                            .ShouldBeTrue();
                    }
                }
            }

            using (var context = new SchoolContext(DefaultOptions))
            {
                context
                    .Person
                    .Any(p => p.FirstName == "Test")
                    .ShouldBeFalse();
            }
        }

        [Fact]
        public void HoeWerktUpdateMetChangeTrackerDemo()
        {
            using (var context = new SchoolContext(DefaultOptions))
            using (context.Database.BeginTransaction())
            {
                // Arrange
                var kim = context
                    .Person
                    .First(p => p.FirstName == "Kim");

                // Act
                kim.FirstName = "Kam";

                // Assert
                kim.ShouldBeAssignableTo<Person>();

                var entry = context
                    .ChangeTracker
                    .Entries<Person>()
                    .Single();

                entry.Entity.ShouldBe(kim);
                entry
                    .Property("FirstName")
                    .OriginalValue
                    .ShouldBe("Kim");

                entry
                    .Property("FirstName")
                    .CurrentValue
                    .ShouldBe("Kam");
            }
        }

        [Fact]
        public void PersonShouldBeBaseClassOfStudentAndInstructor()
        {
            using (var context = new SchoolContext(DefaultOptions))
            {
                var instructor = context.Person.OfType<Instructor>().First();
            }
        }

        /// <summary>
        /// <see cref="https://docs.efproject.net/en/latest/miscellaneous/logging.html"/>
        /// </summary>
        class MyLoggerProvider : ILoggerProvider
        {
            readonly Action<LogItem> _log;

            public MyLoggerProvider(Action<LogItem> log)
            {
                _log = log;
            }

            public ILogger CreateLogger(string categoryName) => new MyLogger(_log);

            public void Dispose() { }

            class MyLogger : ILogger
            {
                readonly Action<LogItem> _log;

                public MyLogger(Action<LogItem> log)
                {
                    _log = log;
                }

                public bool IsEnabled(LogLevel logLevel) => true;

                public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
                {
                    _log(new LogItem { Loglevel = logLevel, EventId = eventId, State = state, Exception = exception, Message = formatter(state, exception) });
                }

                public IDisposable BeginScope<TState>(TState state) => null;
            }
        }
    }

    /// <summary>
    /// Required to make the migrations work again:
    /// https://docs.efproject.net/en/latest/miscellaneous/configuring-dbcontext.html#using-idbcontextfactory-tcontext
    /// </summary>
    internal class SchoolContextFactory : IDbContextFactory<SchoolContext>
    {
        public SchoolContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<SchoolContext>()
                .UseSqlServer(@"Server=.\SQLEXPRESS;Database=School;Trusted_Connection=True");

            return new SchoolContext(builder.Options);
        }
    }

    [DebuggerDisplay("Message: {Message}")]
    class LogItem
    {
        public EventId EventId { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
        public LogLevel Loglevel { get; set; }
        public object State { get; set; }
    }
}
