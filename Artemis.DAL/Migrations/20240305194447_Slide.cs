using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Artemis.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Slide : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slide",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slogan = table.Column<string>(type: "Varchar(50)", maxLength: 50, nullable: true),
                    Title = table.Column<string>(type: "Varchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "Varchar(250)", maxLength: 250, nullable: false),
                    Picture = table.Column<string>(type: "Varchar(150)", maxLength: 150, nullable: true),
                    Link = table.Column<string>(type: "Varchar(150)", maxLength: 150, nullable: true),
                    DisplayIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slide", x => x.ID);
                });

            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastLoginDate",
                value: new DateTime(2024, 3, 5, 22, 44, 47, 12, DateTimeKind.Local).AddTicks(2217));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slide");

            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastLoginDate",
                value: new DateTime(2024, 3, 5, 18, 47, 40, 969, DateTimeKind.Local).AddTicks(8356));
        }
    }
}
