using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IS_server.Migrations
{
    /// <inheritdoc />
    public partial class m3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "receieverUserName",
                table: "Poruke",
                newName: "receiverUserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "receiverUserName",
                table: "Poruke",
                newName: "receieverUserName");
        }
    }
}
