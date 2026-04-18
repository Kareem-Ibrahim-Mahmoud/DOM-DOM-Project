using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QudraSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ForceServiceSessionCarCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_ServiceSessions_Cars_carId",
               table: "ServiceSessions");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceSessions_Cars_carId",
                table: "ServiceSessions",
                column: "carId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceSessions_Cars_carId",
                table: "ServiceSessions");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceSessions_Cars_carId",
                table: "ServiceSessions",
                column: "carId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
