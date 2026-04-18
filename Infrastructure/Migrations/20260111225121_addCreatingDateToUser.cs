using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QudraSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCreatingDateToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "AspNetUsers");
        }
    }
}
