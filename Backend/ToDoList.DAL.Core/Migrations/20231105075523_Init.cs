using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.DAL.Core.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "tasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 5, 7, 55, 23, 526, DateTimeKind.Utc).AddTicks(8107),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 11, 1, 18, 56, 12, 28, DateTimeKind.Utc).AddTicks(4324));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "tasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 5, 7, 55, 23, 526, DateTimeKind.Utc).AddTicks(7913),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 11, 1, 18, 56, 12, 28, DateTimeKind.Utc).AddTicks(4169));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "tasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 1, 18, 56, 12, 28, DateTimeKind.Utc).AddTicks(4324),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 11, 5, 7, 55, 23, 526, DateTimeKind.Utc).AddTicks(8107));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "tasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 1, 18, 56, 12, 28, DateTimeKind.Utc).AddTicks(4169),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 11, 5, 7, 55, 23, 526, DateTimeKind.Utc).AddTicks(7913));
        }
    }
}
