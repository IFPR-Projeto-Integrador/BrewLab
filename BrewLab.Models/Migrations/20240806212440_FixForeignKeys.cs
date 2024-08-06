using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewLab.Models.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdExperimentalPlanning",
                table: "Experiments");

            migrationBuilder.DropColumn(
                name: "IdExperimentalModel",
                table: "ExperimentalPlannings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdExperimentalPlanning",
                table: "Experiments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdExperimentalModel",
                table: "ExperimentalPlannings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
