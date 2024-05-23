using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class addmigupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanieScore",
                table: "CompaniesScores",
                newName: "CompanieSScore");

            migrationBuilder.AddColumn<decimal>(
                name: "CompanieEScore",
                table: "CompaniesScores",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanieEScore",
                table: "CompaniesScores");

            migrationBuilder.RenameColumn(
                name: "CompanieSScore",
                table: "CompaniesScores",
                newName: "CompanieScore");
        }
    }
}
