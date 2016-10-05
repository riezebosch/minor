using Microsoft.EntityFrameworkCore;
using System;
using JetBrains.Annotations;

namespace TddDemo
{
    public class SeriesContext : DbContext
    {
        public SeriesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Serie> Series { get; internal set; }
    }
}