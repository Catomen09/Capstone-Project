using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class edit_CompaniesScoreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompaniesScores_CompanieInfos_CompaniesInfoCompanieId",
                table: "CompaniesScores");

            migrationBuilder.DropColumn(
                name: "CompanieId",
                table: "CompaniesScores");

            migrationBuilder.RenameColumn(
                name: "CompaniesInfoCompanieId",
                table: "CompaniesScores",
                newName: "CompaniesInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_CompaniesScores_CompaniesInfoCompanieId",
                table: "CompaniesScores",
                newName: "IX_CompaniesScores_CompaniesInfoId");

            migrationBuilder.RenameColumn(
                name: "CompanieId",
                table: "CompanieInfos",
                newName: "CompaniesInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompaniesScores_CompanieInfos_CompaniesInfoId",
                table: "CompaniesScores",
                column: "CompaniesInfoId",
                principalTable: "CompanieInfos",
                principalColumn: "CompaniesInfoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompaniesScores_CompanieInfos_CompaniesInfoId",
                table: "CompaniesScores");

            migrationBuilder.RenameColumn(
                name: "CompaniesInfoId",
                table: "CompaniesScores",
                newName: "CompaniesInfoCompanieId");

            migrationBuilder.RenameIndex(
                name: "IX_CompaniesScores_CompaniesInfoId",
                table: "CompaniesScores",
                newName: "IX_CompaniesScores_CompaniesInfoCompanieId");

            migrationBuilder.RenameColumn(
                name: "CompaniesInfoId",
                table: "CompanieInfos",
                newName: "CompanieId");

            migrationBuilder.AddColumn<int>(
                name: "CompanieId",
                table: "CompaniesScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CompaniesScores_CompanieInfos_CompaniesInfoCompanieId",
                table: "CompaniesScores",
                column: "CompaniesInfoCompanieId",
                principalTable: "CompanieInfos",
                principalColumn: "CompanieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
