using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TddDemo.Migrations.Series
{
    public partial class UniqueOpTitleVanSeries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Series",
                nullable: false);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Series_Title",
                table: "Series",
                column: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Series_Title",
                table: "Series");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Series",
                nullable: true);
        }
    }
}
