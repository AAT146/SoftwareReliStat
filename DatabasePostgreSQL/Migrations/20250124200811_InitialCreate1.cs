using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabasePostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalculationResult_ParameterLawDistribution_ParameterLawDist~",
                table: "CalculationResult");

            migrationBuilder.RenameColumn(
                name: "ParameterLawDistributionId",
                table: "CalculationResult",
                newName: "LawDistributionId");

            migrationBuilder.RenameColumn(
                name: "DeviationValue",
                table: "CalculationResult",
                newName: "ValueDeviation");

            migrationBuilder.RenameIndex(
                name: "IX_CalculationResult_ParameterLawDistributionId",
                table: "CalculationResult",
                newName: "IX_CalculationResult_LawDistributionId");

            migrationBuilder.AlterColumn<string>(
                name: "ParameterValue",
                table: "CalculationResult",
                type: "text",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<int>(
                name: "Cluste",
                table: "CalculationResult",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Interval",
                table: "CalculationResult",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_CalculationResult_LawDistribution_LawDistributionId",
                table: "CalculationResult",
                column: "LawDistributionId",
                principalTable: "LawDistribution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalculationResult_LawDistribution_LawDistributionId",
                table: "CalculationResult");

            migrationBuilder.DropColumn(
                name: "Cluste",
                table: "CalculationResult");

            migrationBuilder.DropColumn(
                name: "Interval",
                table: "CalculationResult");

            migrationBuilder.RenameColumn(
                name: "ValueDeviation",
                table: "CalculationResult",
                newName: "DeviationValue");

            migrationBuilder.RenameColumn(
                name: "LawDistributionId",
                table: "CalculationResult",
                newName: "ParameterLawDistributionId");

            migrationBuilder.RenameIndex(
                name: "IX_CalculationResult_LawDistributionId",
                table: "CalculationResult",
                newName: "IX_CalculationResult_ParameterLawDistributionId");

            migrationBuilder.AlterColumn<double>(
                name: "ParameterValue",
                table: "CalculationResult",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_CalculationResult_ParameterLawDistribution_ParameterLawDist~",
                table: "CalculationResult",
                column: "ParameterLawDistributionId",
                principalTable: "ParameterLawDistribution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
