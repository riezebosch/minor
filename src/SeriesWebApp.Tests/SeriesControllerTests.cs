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
        [Fact]
        public void IndexShouldNotIncludeSeasonsAndEpisodesInModel()
        {
            using (var context = CreateContext())
            {
                var controller = new SeriesController(context);
                var view = controller.Index();

                var result = Assert.IsType<ViewResult>(view);
                var model = Assert.IsAssignableFrom<IEnumerable<Serie>>(result.Model);

                Assert.False(model.SelectMany(s => s.Seasons).Any());
            }
        }


        [Fact]
        public void DetailsShouldShowSpecificSerie()
        {
            using (var context = CreateContext())
            {
                var serie = context.Series.First();
                var controller = new SeriesController(context);
                var view = controller.Details(serie.Id);

                var result = Assert.IsType<ViewResult>(view);
                var model = Assert.IsType<Serie>(result.Model);

                Assert.Equal(serie, model);
            }
        }

        [Fact]
        public void DetailsShouldReturnErrorWhenSerieNotExists()
        {
            AssertNotFound(controller => controller.Details(-1), -1);
        }

        [Fact]
        public void DetailsShouldIncludeSeasonsAndEpisodesInModel()
        {
            using (var context = CreateContext())
            {
                var serie = context.Series.First();
                var controller = new SeriesController(context);
                var view = controller.Details(serie.Id);

                var result = Assert.IsType<ViewResult>(view);
                var model = Assert.IsType<Serie>(result.Model);

                Assert.True(model.Seasons.SelectMany(s => s.Episodes).Any());
            }
        }

        [Fact]
        public void EditShouldReturnErrorWhenSerieNotExists()
        {
            AssertNotFound(controller => controller.Edit(-1), -1);
        }

        [Fact]
        public void UpdateShouldSaveChangesToContext()
        {
            var title = Guid.NewGuid().ToString();

            using (var context = CreateContext())
            {
                var serie = context.Series.First();
                var controller = new SeriesController(context);

                controller.Update(serie.Id, title);
            }

            using (var context = CreateContext())
            {
                Assert.True(context.Series.Any(s => s.Title == title));
            }
        }

        [Fact]
        public void UpdateShouldRedirectToDetails()
        {
            using (var context = CreateContext())
            {
                var serie = context.Series.First();
                var controller = new SeriesController(context);
                var view = controller.Update(serie.Id, "");

                var result = Assert.IsType<RedirectToActionResult>(view);
                
                Assert.Equal(result.ActionName, "Details");
                Assert.Equal(serie.Id, result.RouteValues["id"]);
            }
        }

        [Fact]
        public void UpdateShouldReturnErrorWhenSerieNotExists()
        {
            AssertNotFound(controller => controller.Update(-1, ""), -1);
        }

        private void AssertNotFound(Func<SeriesController, IActionResult> action, object value)
        {
            using (var context = CreateContext())
            {
                var controller = new SeriesController(context);
                var result = action(controller);

                var notfound = Assert.IsType<NotFoundObjectResult>(result);
                Assert.Equal(notfound.Value, value);
            }
        }

        [Fact]
        public void EditShouldShowSpecificSerie()
        {
            using (var context = CreateContext())
            {
                var serie = context.Series.First();
                var controller = new SeriesController(context);
                var view = controller.Edit(serie.Id);

                var result = Assert.IsType<ViewResult>(view);
                var model = Assert.IsType<Serie>(result.Model);

                Assert.Equal(serie, model);
            }
        }

        private SeriesContext CreateContext()
        {
            DbContextOptions<SeriesContext> options = new DbContextOptionsBuilder<SeriesContext>()
                .UseSqlite("FileName=temp.db").Options;

            InitializeDatabase(options);
            return new SeriesContext(options);
        }

        private void InitializeDatabase(DbContextOptions<SeriesContext> options)
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
