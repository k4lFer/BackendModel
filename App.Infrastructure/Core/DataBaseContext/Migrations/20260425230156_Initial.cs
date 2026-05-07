using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Core.DataBaseContext.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "user_credential");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "user_credential",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "varchar", maxLength: 150, nullable: false),
                    username = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "persons",
                schema: "user_credential",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "date", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.id);
                    table.ForeignKey(
                        name: "FK_persons_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "user_credential",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_persons_user_id",
                schema: "user_credential",
                table: "persons",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "persons",
                schema: "user_credential");

            migrationBuilder.DropTable(
                name: "users",
                schema: "user_credential");
        }
    }
}
