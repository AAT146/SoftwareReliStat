using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabasePostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeviationValue",
                table: "CalculationResult",
                newName: "ValueDeviation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValueDeviation",
                table: "CalculationResult",
                newName: "DeviationValue");
        }
    }
}
