using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.DAL.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitCloud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "tasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 25, 20, 22, 32, 53, DateTimeKind.Utc).AddTicks(5512),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 9, 23, 18, 30, 44, 149, DateTimeKind.Utc).AddTicks(3935));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "tasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 25, 20, 22, 32, 53, DateTimeKind.Utc).AddTicks(5284),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 9, 23, 18, 30, 44, 149, DateTimeKind.Utc).AddTicks(3755));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "tasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 23, 18, 30, 44, 149, DateTimeKind.Utc).AddTicks(3935),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 9, 25, 20, 22, 32, 53, DateTimeKind.Utc).AddTicks(5512));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "tasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 23, 18, 30, 44, 149, DateTimeKind.Utc).AddTicks(3755),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 9, 25, 20, 22, 32, 53, DateTimeKind.Utc).AddTicks(5284));
        }
    }
}
