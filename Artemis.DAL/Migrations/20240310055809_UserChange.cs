using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Artemis.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastLoginDate",
                value: new DateTime(2024, 3, 10, 8, 58, 9, 372, DateTimeKind.Local).AddTicks(2075));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastLoginDate",
                value: new DateTime(2024, 3, 10, 4, 28, 43, 143, DateTimeKind.Local).AddTicks(4539));
        }
    }
}
