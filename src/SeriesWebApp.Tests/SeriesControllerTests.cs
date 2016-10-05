using SeriesWebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using TddDemo;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace SeriesWebApp.Tests
{
    public class SeriesControllerTests
    {
        DbContextOptions<SeriesContext> options = new DbContextOptionsBuilder<SeriesContext>()
            .UseSqlite("FileName=temp.db").Options;

        [Fact]
        public void IndexShouldIncludeSeasonsAndEpisodesInModel()
        {
            InitializeDatabase();
            using (var context = new SeriesContext(options))
            {
                var controller = new SeriesController(context);
                var view = controller.Index();

                var result = Assert.IsType<ViewResult>(view);
                var model = Assert.IsAssignableFrom<IEnumerable<Serie>>(result.Model);

                Assert.True(model.SelectMany(s => s.Seasons).SelectMany(s => s.Episodes).Any());
            }
        }

        private void InitializeDatabase()
        {
            using (var context = new SeriesContext(options))
            {
                if (context.Database.EnsureCreated())
                {
                    var serie = new Serie
                    {
                        Id = 0,
                        Title = "Narcos"
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
        }
    }
}
