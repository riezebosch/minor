using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TddDemo;

namespace TddDemo.Migrations.Series
{
    [DbContext(typeof(SeriesContext))]
    [Migration("20161006125807_UniqueKeyWithIndex")]
    partial class UniqueKeyWithIndex
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TddDemo.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("SeasonId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("Episode");
                });

            modelBuilder.Entity("TddDemo.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("SerieId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("SerieId");

                    b.ToTable("Season");
                });

            modelBuilder.Entity("TddDemo.Serie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Series");
                });

            modelBuilder.Entity("TddDemo.Episode", b =>
                {
                    b.HasOne("TddDemo.Season")
                        .WithMany("Episodes")
                        .HasForeignKey("SeasonId");
                });

            modelBuilder.Entity("TddDemo.Season", b =>
                {
                    b.HasOne("TddDemo.Serie")
                        .WithMany("Seasons")
                        .HasForeignKey("SerieId");
                });
        }
    }
}
