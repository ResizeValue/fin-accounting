using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinAccountingApi.Persistance.Migrations
{
    public partial class AddDescriptionToResource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UserResources",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "UserResources");
        }
    }
}
