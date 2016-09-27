using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TddDemo.Migrations
{
    public partial class TPT2TPH : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Course",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Course",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "Course",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Course",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Course",
                type: "smalldatetime",
                nullable: true);

            migrationBuilder.Sql("MERGE Course AS target USING OnsiteCourse as source ON (target.CourseID = source.CourseID) WHEN MATCHED THEN UPDATE SET target.Location=source.Location,target.Days=source.Days,target.Time=source.Time,target.Discriminator='OnsiteCourse'");
            migrationBuilder.Sql("MERGE Course AS target USING OnlineCourse as source ON (target.CourseID = source.CourseID) WHEN MATCHED THEN UPDATE SET target.URL=source.URL,target.Discriminator='OnlineCourse'");

            migrationBuilder.DropTable(
               name: "OnlineCourse");

            migrationBuilder.DropTable(
                name: "OnsiteCourse");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "OnlineCourse",
                columns: table => new
                {
                    CourseID = table.Column<int>(nullable: false),
                    URL = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineCourse", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_OnlineCourse_Course",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OnsiteCourse",
                columns: table => new
                {
                    CourseID = table.Column<int>(nullable: false),
                    Days = table.Column<string>(maxLength: 50, nullable: false),
                    Location = table.Column<string>(maxLength: 50, nullable: false),
                    Time = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnsiteCourse", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_OnsiteCourse_Course",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.Sql("INSERT INTO OnsiteCourse (CourseID, Location, Days, Time) SELECT CourseID, Location, Days, Time FROM Course WHERE Discriminator = 'OnsiteCourse'");
            migrationBuilder.Sql("INSERT INTO OnlineCourse (CourseID, URL) SELECT CourseID, URL FROM Course WHERE Discriminator = 'OnlineCourse'");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineCourse_CourseID",
                table: "OnlineCourse",
                column: "CourseID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OnsiteCourse_CourseID",
                table: "OnsiteCourse",
                column: "CourseID",
                unique: true);

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "URL",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Days",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Course");
        }
    }
}
