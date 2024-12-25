using System;
using Microsoft.EntityFrameworkCore.Migrations;

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
                name: "Laws",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laws", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnifiedSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnifiedSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LawDistributionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parameters_Laws_LawDistributionId",
                        column: x => x.LawDistributionId,
                        principalTable: "Laws",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PowerSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UnifiedPowerSystemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerSystems_UnifiedSystems_UnifiedPowerSystemId",
                        column: x => x.UnifiedPowerSystemId,
                        principalTable: "UnifiedSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PowerSystemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zones_PowerSystems_PowerSystemId",
                        column: x => x.PowerSystemId,
                        principalTable: "PowerSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReliabilityZoneId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Connections_Zones_ReliabilityZoneId",
                        column: x => x.ReliabilityZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReliabilityZoneId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plants_Zones_ReliabilityZoneId",
                        column: x => x.ReliabilityZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PowerSystemId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReliabilityZoneId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_PowerSystems_PowerSystemId",
                        column: x => x.PowerSystemId,
                        principalTable: "PowerSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sections_Zones_ReliabilityZoneId",
                        column: x => x.ReliabilityZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReliabilityZoneId = table.Column<Guid>(type: "uuid", nullable: false),
                    TimeStampStart = table.Column<string>(type: "text", nullable: false),
                    TimeStampEnd = table.Column<string>(type: "text", nullable: false),
                    Step = table.Column<int>(type: "integer", nullable: false),
                    ValueMin = table.Column<int>(type: "integer", nullable: false),
                    ValueMax = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Times_Zones_ReliabilityZoneId",
                        column: x => x.ReliabilityZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StructureConnections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InterZoneConnectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Uid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructureConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StructureConnections_Connections_InterZoneConnectionId",
                        column: x => x.InterZoneConnectionId,
                        principalTable: "Connections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StructurePlants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PowerPlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Uid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructurePlants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StructurePlants_Plants_PowerPlantId",
                        column: x => x.PowerPlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StructureSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ControlledSectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Uid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructureSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StructureSections_Sections_ControlledSectionId",
                        column: x => x.ControlledSectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TimeCharacteristicId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviationValue = table.Column<double>(type: "double precision", nullable: false),
                    WeightFactor = table.Column<double>(type: "double precision", nullable: false),
                    ParameterLawDistributionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParameterValue = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Parameters_ParameterLawDistributionId",
                        column: x => x.ParameterLawDistributionId,
                        principalTable: "Parameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Times_TimeCharacteristicId",
                        column: x => x.TimeCharacteristicId,
                        principalTable: "Times",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Connections_ReliabilityZoneId",
                table: "Connections",
                column: "ReliabilityZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_LawDistributionId",
                table: "Parameters",
                column: "LawDistributionId");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_ReliabilityZoneId",
                table: "Plants",
                column: "ReliabilityZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerSystems_UnifiedPowerSystemId",
                table: "PowerSystems",
                column: "UnifiedPowerSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_ParameterLawDistributionId",
                table: "Results",
                column: "ParameterLawDistributionId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_TimeCharacteristicId",
                table: "Results",
                column: "TimeCharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_PowerSystemId",
                table: "Sections",
                column: "PowerSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ReliabilityZoneId",
                table: "Sections",
                column: "ReliabilityZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_StructureConnections_InterZoneConnectionId",
                table: "StructureConnections",
                column: "InterZoneConnectionId");

            migrationBuilder.CreateIndex(
                name: "IX_StructurePlants_PowerPlantId",
                table: "StructurePlants",
                column: "PowerPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_StructureSections_ControlledSectionId",
                table: "StructureSections",
                column: "ControlledSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Times_ReliabilityZoneId",
                table: "Times",
                column: "ReliabilityZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_PowerSystemId",
                table: "Zones",
                column: "PowerSystemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "StructureConnections");

            migrationBuilder.DropTable(
                name: "StructurePlants");

            migrationBuilder.DropTable(
                name: "StructureSections");

            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Laws");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "PowerSystems");

            migrationBuilder.DropTable(
                name: "UnifiedSystems");
        }
    }
}
