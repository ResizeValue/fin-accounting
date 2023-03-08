using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinAccountingApi.Persistance.Migrations
{
    public partial class addImageToResource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "UserResources",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "UserResources");
        }
    }
}
