using Microsoft.EntityFrameworkCore.Migrations;

namespace SignalRChatApp.Migrations
{
    public partial class ChangeMessagesUserTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserFromId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserToId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserFromId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserToId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserFromId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserToId",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "UserFrom",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserTo",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserFrom",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserTo",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "UserFromId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserToId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserFromId",
                table: "Messages",
                column: "UserFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserToId",
                table: "Messages",
                column: "UserToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserFromId",
                table: "Messages",
                column: "UserFromId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserToId",
                table: "Messages",
                column: "UserToId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
