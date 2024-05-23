using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class add_mig_CompaniesScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompaniesScores",
                columns: table => new
                {
                    CompaniesScoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanieId = table.Column<int>(type: "int", nullable: false),
                    CompanieStockCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompaniesInfoCompanieId = table.Column<int>(type: "int", nullable: false),
                    CompanieScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompaniesScores", x => x.CompaniesScoreId);
                    table.ForeignKey(
                        name: "FK_CompaniesScores_CompanieInfos_CompaniesInfoCompanieId",
                        column: x => x.CompaniesInfoCompanieId,
                        principalTable: "CompanieInfos",
                        principalColumn: "CompanieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompaniesScores_CompaniesInfoCompanieId",
                table: "CompaniesScores",
                column: "CompaniesInfoCompanieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompaniesScores");
        }
    }
}
