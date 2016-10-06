using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                    Title = $"Narcos{Guid.NewGuid()}"
                };
                context.Series.Add(serie);

                var season = new Season
                {
                    Id = 0,
                    Title = "Season 1"
                };
                serie.Seasons.Add(season);

                var episode = new Episode
                {
                    Id = 0,
                    Title = "Descenso"
                };
                season.Episodes.Add(episode);
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
                var serie = from s in context
                                .Series
                                .Include(s => s.Seasons) // dit is nodig om ook de seasons erbij te laden
                                .ThenInclude(s => s.Episodes)
                            where s.Seasons.Any() && s.Seasons.SelectMany(season => season.Episodes).Any()
                            select s;

                Assert.True(serie.First().Seasons.Any());
                Assert.True(serie.First().Seasons.First().Episodes.Any());
            }
        }

        [Fact]
        public void DeTitleVanEenSerieMoetUniekZijn()
        {
            using (var connection = new SqlConnection(@"Server=.\SQLEXPRESS;Database=Series;Trusted_Connection=true"))
            {
                connection.Open();
                using (var tx = connection.BeginTransaction())
                {
                    var options = new DbContextOptionsBuilder<SeriesContext>()
                        .UseSqlServer(connection)
                        .Options;

                    // Arrange
                    using (var context = new SeriesContext(options))
                    {
                        context.Database.UseTransaction(tx);
                        context.Series.Add(new Serie { Title = "UNIEK" });
                        context.SaveChanges();
                    }

                    using (var context = new SeriesContext(options))
                    {
                        context.Database.UseTransaction(tx);
                        context.Series.Add(new Serie { Title = "UNIEK" });

                        var ex = Assert.Throws<DbUpdateException>(() => context.SaveChanges());
                        Assert.Contains("UNIQUE", ex.InnerException.Message);
                        Assert.Contains("UNIEK", ex.InnerException.Message);
                    }
                }
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
