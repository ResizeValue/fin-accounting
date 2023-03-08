using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinAccountingApi.Persistance.Migrations
{
    public partial class OC_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OwnershipCost",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "OwnershipCost");
        }
    }
}
