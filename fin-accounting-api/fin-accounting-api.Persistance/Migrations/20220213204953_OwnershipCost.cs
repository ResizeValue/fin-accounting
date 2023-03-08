using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinAccountingApi.Persistance.Migrations
{
    public partial class OwnershipCost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceCosts");

            migrationBuilder.CreateTable(
                name: "OwnershipCost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Periodicity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnershipCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnershipCost_UserResources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "UserResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OwnershipCost_ResourceId",
                table: "OwnershipCost",
                column: "ResourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnershipCost");

            migrationBuilder.CreateTable(
                name: "MaintenanceCosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceId = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Periodicity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceCosts_UserResources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "UserResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceCosts_ResourceId",
                table: "MaintenanceCosts",
                column: "ResourceId");
        }
    }
}
