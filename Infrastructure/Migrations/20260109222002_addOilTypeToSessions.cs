using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QudraSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addOilTypeToSessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OilType",
                table: "ServiceSessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OilType",
                table: "ServiceSessions");
        }
    }
}
