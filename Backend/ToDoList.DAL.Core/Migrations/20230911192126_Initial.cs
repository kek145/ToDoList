using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToDoList.DAL.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 9, 11, 19, 21, 26, 386, DateTimeKind.Utc).AddTicks(2918)),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 9, 11, 19, 21, 26, 386, DateTimeKind.Utc).AddTicks(3017))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tasks", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tasks");
        }
    }
}
