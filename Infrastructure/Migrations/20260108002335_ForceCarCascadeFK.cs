using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QudraSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ForceCarCascadeFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_customerId",
                table: "Cars");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_customerId",
                table: "Cars",
                column: "customerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_customerId",
                table: "Cars");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_customerId",
                table: "Cars",
                column: "customerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
