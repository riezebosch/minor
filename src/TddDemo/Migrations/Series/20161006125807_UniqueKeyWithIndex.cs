using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TddDemo.Migrations.Series
{
    public partial class UniqueKeyWithIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Series",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Series_Title",
                table: "Series",
                column: "Title",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Series_Title",
                table: "Series");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Series",
                nullable: true);
        }
    }
}
