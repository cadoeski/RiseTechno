using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contact.Infrastructure.Migrations
{
    public partial class db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "contact",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    konum = table.Column<string>(type: "text", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contact", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    name = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    company = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_id",
                schema: "public",
                table: "user",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contact",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user",
                schema: "public");
        }
    }
}
