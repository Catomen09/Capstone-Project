using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class add_mig_CompanieInfos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanieInfos",
                columns: table => new
                {
                    CompanieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanieStockCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Scope1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Scope2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Scope3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterUsage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EnergyUsage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WomenEmployees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MenEmployees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalEmployees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RenewableEnergy = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanieInfos", x => x.CompanieId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanieInfos");
        }
    }
}
