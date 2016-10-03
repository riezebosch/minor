using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TddDemo.Model;
using Xunit;

namespace TddDemo
{
    public class Class1
    {
        [Fact]
        public void OnsiteAndOnlineCourseAreDerivedFromCourse()
        {
            using (var context = new SchoolContext())
            {
                Assert.True(context.Course.OfType<OnsiteCourse>().Any());
            }
        }

        [Fact]  
        public void HoeKomIkAanDeQueryInCSharp()
        {
            using (var context = new SchoolContext())
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

            public ILogger CreateLogger(string categoryName) =>new MyLogger(_log);

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
