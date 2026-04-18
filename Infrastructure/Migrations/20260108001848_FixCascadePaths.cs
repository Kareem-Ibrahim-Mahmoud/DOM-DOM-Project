using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QudraSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_CustomerId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceSessions_AspNetUsers_customerId",
                table: "ServiceSessions");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_CustomerId",
                table: "Notifications");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceSessions_AspNetUsers_customerId",
                table: "ServiceSessions",
                column: "customerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceSessions_AspNetUsers_customerId",
                table: "ServiceSessions");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CustomerId",
                table: "Notifications",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_CustomerId",
                table: "Notifications",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceSessions_AspNetUsers_customerId",
                table: "ServiceSessions",
                column: "customerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
