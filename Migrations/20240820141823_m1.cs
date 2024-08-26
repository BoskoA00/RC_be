using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IS_server.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Poruke",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    senderEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    receieverEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    senderId = table.Column<int>(type: "int", nullable: false),
                    receiverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poruke", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Poruke_Users_receiverId",
                        column: x => x.receiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Poruke_Users_senderId",
                        column: x => x.senderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Poruke_receiverId",
                table: "Poruke",
                column: "receiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Poruke_senderId",
                table: "Poruke",
                column: "senderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Poruke");
        }
    }
}
