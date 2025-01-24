using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabasePostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LawDistribution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawDistribution", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnifiedPowerSystem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnifiedPowerSystem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParameterLawDistribution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LawDistributionId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterLawDistribution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParameterLawDistribution_LawDistribution_LawDistributionId",
                        column: x => x.LawDistributionId,
                        principalTable: "LawDistribution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PowerSystem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UnifiedPowerSystemId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSystem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerSystem_UnifiedPowerSystem_UnifiedPowerSystemId",
                        column: x => x.UnifiedPowerSystemId,
                        principalTable: "UnifiedPowerSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReliabilityZone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PowerSystemId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReliabilityZone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReliabilityZone_PowerSystem_PowerSystemId",
                        column: x => x.PowerSystemId,
                        principalTable: "PowerSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterZoneConnection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReliabilityZoneId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterZoneConnection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterZoneConnection_ReliabilityZone_ReliabilityZoneId",
                        column: x => x.ReliabilityZoneId,
                        principalTable: "ReliabilityZone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PowerPlant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReliabilityZoneId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerPlant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerPlant_ReliabilityZone_ReliabilityZoneId",
                        column: x => x.ReliabilityZoneId,
                        principalTable: "ReliabilityZone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeCharacteristic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReliabilityZoneId = table.Column<int>(type: "integer", nullable: false),
                    TimeStampStart = table.Column<string>(type: "text", nullable: false),
                    TimeStampEnd = table.Column<string>(type: "text", nullable: false),
                    Step = table.Column<int>(type: "integer", nullable: false),
                    ValueMin = table.Column<int>(type: "integer", nullable: false),
                    ValueMax = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeCharacteristic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeCharacteristic_ReliabilityZone_ReliabilityZoneId",
                        column: x => x.ReliabilityZoneId,
                        principalTable: "ReliabilityZone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StructureInterZoneConnection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InterZoneConnectionId = table.Column<int>(type: "integer", nullable: false),
                    TypeObject = table.Column<string>(type: "text", nullable: false),
                    TitleObject = table.Column<string>(type: "text", nullable: false),
                    UidObject = table.Column<string>(type: "text", nullable: false),
                    IsControlledSection = table.Column<bool>(type: "boolean", nullable: false),
                    TitleSection = table.Column<string>(type: "text", nullable: false),
                    UidSection = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructureInterZoneConnection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StructureInterZoneConnection_InterZoneConnection_InterZoneC~",
                        column: x => x.InterZoneConnectionId,
                        principalTable: "InterZoneConnection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StructurePowerPlant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PowerPlantId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Uid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructurePowerPlant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StructurePowerPlant_PowerPlant_PowerPlantId",
                        column: x => x.PowerPlantId,
                        principalTable: "PowerPlant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalculationResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TimeCharacteristicId = table.Column<int>(type: "integer", nullable: false),
                    DeviationValue = table.Column<double>(type: "double precision", nullable: false),
                    WeightFactor = table.Column<double>(type: "double precision", nullable: false),
                    ParameterLawDistributionId = table.Column<int>(type: "integer", nullable: false),
                    ParameterValue = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalculationResult_ParameterLawDistribution_ParameterLawDist~",
                        column: x => x.ParameterLawDistributionId,
                        principalTable: "ParameterLawDistribution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalculationResult_TimeCharacteristic_TimeCharacteristicId",
                        column: x => x.TimeCharacteristicId,
                        principalTable: "TimeCharacteristic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalculationResult_ParameterLawDistributionId",
                table: "CalculationResult",
                column: "ParameterLawDistributionId");

            migrationBuilder.CreateIndex(
                name: "IX_CalculationResult_TimeCharacteristicId",
                table: "CalculationResult",
                column: "TimeCharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_InterZoneConnection_ReliabilityZoneId",
                table: "InterZoneConnection",
                column: "ReliabilityZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterLawDistribution_LawDistributionId",
                table: "ParameterLawDistribution",
                column: "LawDistributionId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerPlant_ReliabilityZoneId",
                table: "PowerPlant",
                column: "ReliabilityZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerSystem_UnifiedPowerSystemId",
                table: "PowerSystem",
                column: "UnifiedPowerSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReliabilityZone_PowerSystemId",
                table: "ReliabilityZone",
                column: "PowerSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_StructureInterZoneConnection_InterZoneConnectionId",
                table: "StructureInterZoneConnection",
                column: "InterZoneConnectionId");

            migrationBuilder.CreateIndex(
                name: "IX_StructurePowerPlant_PowerPlantId",
                table: "StructurePowerPlant",
                column: "PowerPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeCharacteristic_ReliabilityZoneId",
                table: "TimeCharacteristic",
                column: "ReliabilityZoneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculationResult");

            migrationBuilder.DropTable(
                name: "StructureInterZoneConnection");

            migrationBuilder.DropTable(
                name: "StructurePowerPlant");

            migrationBuilder.DropTable(
                name: "ParameterLawDistribution");

            migrationBuilder.DropTable(
                name: "TimeCharacteristic");

            migrationBuilder.DropTable(
                name: "InterZoneConnection");

            migrationBuilder.DropTable(
                name: "PowerPlant");

            migrationBuilder.DropTable(
                name: "LawDistribution");

            migrationBuilder.DropTable(
                name: "ReliabilityZone");

            migrationBuilder.DropTable(
                name: "PowerSystem");

            migrationBuilder.DropTable(
                name: "UnifiedPowerSystem");
        }
    }
}
