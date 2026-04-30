using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exercise3.Migrations
{
    /// <inheritdoc />
    public partial class AddedCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LaunchDate",
                table: "Missions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2026, 4, 9, 18, 49, 43, 467, DateTimeKind.Local).AddTicks(9181),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2026, 4, 9, 17, 53, 35, 238, DateTimeKind.Local).AddTicks(9966));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LaunchDate",
                table: "Missions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2026, 4, 9, 17, 53, 35, 238, DateTimeKind.Local).AddTicks(9966),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2026, 4, 9, 18, 49, 43, 467, DateTimeKind.Local).AddTicks(9181));
        }
    }
}
