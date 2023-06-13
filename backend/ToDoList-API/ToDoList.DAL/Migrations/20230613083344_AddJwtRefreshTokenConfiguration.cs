using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToDoList.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddJwtRefreshTokenConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                table: "tasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 13, 8, 33, 44, 45, DateTimeKind.Utc).AddTicks(9814),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 6, 12, 16, 45, 26, 731, DateTimeKind.Utc).AddTicks(7974));

            migrationBuilder.CreateTable(
                name: "tokens",
                columns: table => new
                {
                    tokenid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    token = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tokens", x => x.tokenid);
                    table.ForeignKey(
                        name: "FK_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tokens_token",
                table: "tokens",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tokens_user_id",
                table: "tokens",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tokens");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                table: "tasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 12, 16, 45, 26, 731, DateTimeKind.Utc).AddTicks(7974),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 6, 13, 8, 33, 44, 45, DateTimeKind.Utc).AddTicks(9814));
        }
    }
}
