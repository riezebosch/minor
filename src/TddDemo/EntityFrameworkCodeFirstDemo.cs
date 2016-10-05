using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class EntityFrameworkCodeFirstDemo
    {
        [Fact]
        public void BeginnenMetEenContext()
        {
            var options = new DbContextOptionsBuilder<SeriesContext>()
                .UseSqlServer(@"Server=.\SQLEXPRESS;Database=Series;Trusted_Connection=true").Options;

            using (var context = new SeriesContext(options))
            {
                context.Series.Add(new Serie
                {
                    Id = 0,
                    Title = "Narcos"
                });

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Required to make the migrations work again:
        /// https://docs.efproject.net/en/latest/miscellaneous/configuring-dbcontext.html#using-idbcontextfactory-tcontext
        /// </summary>
        internal class SeriesContextFactory : IDbContextFactory<SeriesContext>
        {
            public SeriesContext Create(DbContextFactoryOptions options)
            {
                var builder = new DbContextOptionsBuilder<SeriesContext>()
                    .UseSqlServer(@"Server=.\SQLEXPRESS;Database=Series;Trusted_Connection=True");

                return new SeriesContext(builder.Options);
            }
        }
    }
}
