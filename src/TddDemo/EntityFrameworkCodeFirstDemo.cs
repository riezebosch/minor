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
        DbContextOptions<SeriesContext> options = new DbContextOptionsBuilder<SeriesContext>()
            .UseSqlServer(@"Server=.\SQLEXPRESS;Database=Series;Trusted_Connection=true").Options;

        [Fact]
        public void BeginnenMetEenContext()
        {

            using (var context = new SeriesContext(options))
            {
                context.Database.Migrate();

                var serie = new Serie
                {
                    Id = 0,
                    Title = "Narcos"
                };

                serie.Seasons.Add(new Season
                {
                    Id = 0,
                    Title = "Descenso"
                });

                context.Series.Add(serie);

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Eager loading: http://ef.readthedocs.io/en/latest/querying/related-data.html#eager-loading
        /// </summary>
        [Fact]
        public void HoeNeemIkSeizoenenMeeBijHetOpvragenVanEenSerie()
        {
            using (var context = new SeriesContext(options))
            {
                var narcos = context
                    .Series
                    .Include(s => s.Seasons) // dit is nodig om ook de seasons erbij te laden
                    .First(s => s.Title == "Narcos");

                Assert.True(narcos.Seasons.Any());
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
