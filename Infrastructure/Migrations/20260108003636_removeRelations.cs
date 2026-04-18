using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QudraSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_customerId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_WorkshopId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceSessions_AspNetUsers_customerId",
                table: "ServiceSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceSessions_Cars_carId",
                table: "ServiceSessions");

            migrationBuilder.DropIndex(
                name: "IX_ServiceSessions_carId",
                table: "ServiceSessions");

            migrationBuilder.DropIndex(
                name: "IX_ServiceSessions_customerId",
                table: "ServiceSessions");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_WorkshopId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Cars_customerId",
                table: "Cars");

            migrationBuilder.AlterColumn<string>(
                name: "customerId",
                table: "ServiceSessions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "WorkshopId",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "customerId",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "customerId",
                table: "ServiceSessions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "WorkshopId",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "customerId",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSessions_carId",
                table: "ServiceSessions",
                column: "carId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSessions_customerId",
                table: "ServiceSessions",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_WorkshopId",
                table: "Notifications",
                column: "WorkshopId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_customerId",
                table: "Cars",
                column: "customerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_customerId",
                table: "Cars",
                column: "customerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_WorkshopId",
                table: "Notifications",
                column: "WorkshopId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceSessions_AspNetUsers_customerId",
                table: "ServiceSessions",
                column: "customerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceSessions_Cars_carId",
                table: "ServiceSessions",
                column: "carId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
