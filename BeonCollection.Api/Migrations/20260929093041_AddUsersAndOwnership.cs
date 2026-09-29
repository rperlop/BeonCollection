using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeonCollection.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersAndOwnership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Copies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Copies_OwnerId",
                table: "Copies",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_Users_OwnerId",
                table: "Copies",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_Users_OwnerId",
                table: "Copies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Copies_OwnerId",
                table: "Copies");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Copies");
        }
    }
}
