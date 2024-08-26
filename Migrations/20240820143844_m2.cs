using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IS_server.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "senderEmail",
                table: "Poruke",
                newName: "senderUserName");

            migrationBuilder.RenameColumn(
                name: "receieverEmail",
                table: "Poruke",
                newName: "receieverUserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "senderUserName",
                table: "Poruke",
                newName: "senderEmail");

            migrationBuilder.RenameColumn(
                name: "receieverUserName",
                table: "Poruke",
                newName: "receieverEmail");
        }
    }
}
