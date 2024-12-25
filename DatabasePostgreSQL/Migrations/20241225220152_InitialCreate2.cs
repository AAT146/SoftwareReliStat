using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabasePostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Zones_ReliabilityZoneId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_Laws_LawDistributionId",
                table: "Parameters");

            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Zones_ReliabilityZoneId",
                table: "Plants");

            migrationBuilder.DropForeignKey(
                name: "FK_PowerSystems_UnifiedSystems_UnifiedPowerSystemId",
                table: "PowerSystems");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Parameters_ParameterLawDistributionId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Times_TimeCharacteristicId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_StructureConnections_Connections_InterZoneConnectionId",
                table: "StructureConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_StructurePlants_Plants_PowerPlantId",
                table: "StructurePlants");

            migrationBuilder.DropForeignKey(
                name: "FK_Times_Zones_ReliabilityZoneId",
                table: "Times");

            migrationBuilder.DropForeignKey(
                name: "FK_Zones_PowerSystems_PowerSystemId",
                table: "Zones");

            migrationBuilder.DropTable(
                name: "StructureSections");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zones",
                table: "Zones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnifiedSystems",
                table: "UnifiedSystems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Times",
                table: "Times");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StructurePlants",
                table: "StructurePlants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StructureConnections",
                table: "StructureConnections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Results",
                table: "Results");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PowerSystems",
                table: "PowerSystems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Plants",
                table: "Plants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parameters",
                table: "Parameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Laws",
                table: "Laws");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Connections",
                table: "Connections");

            migrationBuilder.RenameTable(
                name: "Zones",
                newName: "ReliabilityZone");

            migrationBuilder.RenameTable(
                name: "UnifiedSystems",
                newName: "UnifiedPowerSystem");

            migrationBuilder.RenameTable(
                name: "Times",
                newName: "TimeCharacteristic");

            migrationBuilder.RenameTable(
                name: "StructurePlants",
                newName: "StructurePowerPlant");

            migrationBuilder.RenameTable(
                name: "StructureConnections",
                newName: "StructureInterZoneConnection");

            migrationBuilder.RenameTable(
                name: "Results",
                newName: "CalculationResult");

            migrationBuilder.RenameTable(
                name: "PowerSystems",
                newName: "PowerSystem");

            migrationBuilder.RenameTable(
                name: "Plants",
                newName: "PowerPlant");

            migrationBuilder.RenameTable(
                name: "Parameters",
                newName: "ParameterLawDistribution");

            migrationBuilder.RenameTable(
                name: "Laws",
                newName: "LawDistribution");

            migrationBuilder.RenameTable(
                name: "Connections",
                newName: "InterZoneConnection");

            migrationBuilder.RenameIndex(
                name: "IX_Zones_PowerSystemId",
                table: "ReliabilityZone",
                newName: "IX_ReliabilityZone_PowerSystemId");

            migrationBuilder.RenameIndex(
                name: "IX_Times_ReliabilityZoneId",
                table: "TimeCharacteristic",
                newName: "IX_TimeCharacteristic_ReliabilityZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_StructurePlants_PowerPlantId",
                table: "StructurePowerPlant",
                newName: "IX_StructurePowerPlant_PowerPlantId");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "StructureInterZoneConnection",
                newName: "UidSection");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "StructureInterZoneConnection",
                newName: "UidObject");

            migrationBuilder.RenameIndex(
                name: "IX_StructureConnections_InterZoneConnectionId",
                table: "StructureInterZoneConnection",
                newName: "IX_StructureInterZoneConnection_InterZoneConnectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Results_TimeCharacteristicId",
                table: "CalculationResult",
                newName: "IX_CalculationResult_TimeCharacteristicId");

            migrationBuilder.RenameIndex(
                name: "IX_Results_ParameterLawDistributionId",
                table: "CalculationResult",
                newName: "IX_CalculationResult_ParameterLawDistributionId");

            migrationBuilder.RenameIndex(
                name: "IX_PowerSystems_UnifiedPowerSystemId",
                table: "PowerSystem",
                newName: "IX_PowerSystem_UnifiedPowerSystemId");

            migrationBuilder.RenameIndex(
                name: "IX_Plants_ReliabilityZoneId",
                table: "PowerPlant",
                newName: "IX_PowerPlant_ReliabilityZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_Parameters_LawDistributionId",
                table: "ParameterLawDistribution",
                newName: "IX_ParameterLawDistribution_LawDistributionId");

            migrationBuilder.RenameIndex(
                name: "IX_Connections_ReliabilityZoneId",
                table: "InterZoneConnection",
                newName: "IX_InterZoneConnection_ReliabilityZoneId");

            migrationBuilder.AddColumn<bool>(
                name: "IsControlledSection",
                table: "StructureInterZoneConnection",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TitleObject",
                table: "StructureInterZoneConnection",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleSection",
                table: "StructureInterZoneConnection",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TypeObject",
                table: "StructureInterZoneConnection",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReliabilityZone",
                table: "ReliabilityZone",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnifiedPowerSystem",
                table: "UnifiedPowerSystem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeCharacteristic",
                table: "TimeCharacteristic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StructurePowerPlant",
                table: "StructurePowerPlant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StructureInterZoneConnection",
                table: "StructureInterZoneConnection",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalculationResult",
                table: "CalculationResult",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PowerSystem",
                table: "PowerSystem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PowerPlant",
                table: "PowerPlant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParameterLawDistribution",
                table: "ParameterLawDistribution",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LawDistribution",
                table: "LawDistribution",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterZoneConnection",
                table: "InterZoneConnection",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CalculationResult_ParameterLawDistribution_ParameterLawDist~",
                table: "CalculationResult",
                column: "ParameterLawDistributionId",
                principalTable: "ParameterLawDistribution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculationResult_TimeCharacteristic_TimeCharacteristicId",
                table: "CalculationResult",
                column: "TimeCharacteristicId",
                principalTable: "TimeCharacteristic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterZoneConnection_ReliabilityZone_ReliabilityZoneId",
                table: "InterZoneConnection",
                column: "ReliabilityZoneId",
                principalTable: "ReliabilityZone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterLawDistribution_LawDistribution_LawDistributionId",
                table: "ParameterLawDistribution",
                column: "LawDistributionId",
                principalTable: "LawDistribution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PowerPlant_ReliabilityZone_ReliabilityZoneId",
                table: "PowerPlant",
                column: "ReliabilityZoneId",
                principalTable: "ReliabilityZone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PowerSystem_UnifiedPowerSystem_UnifiedPowerSystemId",
                table: "PowerSystem",
                column: "UnifiedPowerSystemId",
                principalTable: "UnifiedPowerSystem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReliabilityZone_PowerSystem_PowerSystemId",
                table: "ReliabilityZone",
                column: "PowerSystemId",
                principalTable: "PowerSystem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StructureInterZoneConnection_InterZoneConnection_InterZoneC~",
                table: "StructureInterZoneConnection",
                column: "InterZoneConnectionId",
                principalTable: "InterZoneConnection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StructurePowerPlant_PowerPlant_PowerPlantId",
                table: "StructurePowerPlant",
                column: "PowerPlantId",
                principalTable: "PowerPlant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeCharacteristic_ReliabilityZone_ReliabilityZoneId",
                table: "TimeCharacteristic",
                column: "ReliabilityZoneId",
                principalTable: "ReliabilityZone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalculationResult_ParameterLawDistribution_ParameterLawDist~",
                table: "CalculationResult");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculationResult_TimeCharacteristic_TimeCharacteristicId",
                table: "CalculationResult");

            migrationBuilder.DropForeignKey(
                name: "FK_InterZoneConnection_ReliabilityZone_ReliabilityZoneId",
                table: "InterZoneConnection");

            migrationBuilder.DropForeignKey(
                name: "FK_ParameterLawDistribution_LawDistribution_LawDistributionId",
                table: "ParameterLawDistribution");

            migrationBuilder.DropForeignKey(
                name: "FK_PowerPlant_ReliabilityZone_ReliabilityZoneId",
                table: "PowerPlant");

            migrationBuilder.DropForeignKey(
                name: "FK_PowerSystem_UnifiedPowerSystem_UnifiedPowerSystemId",
                table: "PowerSystem");

            migrationBuilder.DropForeignKey(
                name: "FK_ReliabilityZone_PowerSystem_PowerSystemId",
                table: "ReliabilityZone");

            migrationBuilder.DropForeignKey(
                name: "FK_StructureInterZoneConnection_InterZoneConnection_InterZoneC~",
                table: "StructureInterZoneConnection");

            migrationBuilder.DropForeignKey(
                name: "FK_StructurePowerPlant_PowerPlant_PowerPlantId",
                table: "StructurePowerPlant");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeCharacteristic_ReliabilityZone_ReliabilityZoneId",
                table: "TimeCharacteristic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnifiedPowerSystem",
                table: "UnifiedPowerSystem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeCharacteristic",
                table: "TimeCharacteristic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StructurePowerPlant",
                table: "StructurePowerPlant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StructureInterZoneConnection",
                table: "StructureInterZoneConnection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReliabilityZone",
                table: "ReliabilityZone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PowerSystem",
                table: "PowerSystem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PowerPlant",
                table: "PowerPlant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParameterLawDistribution",
                table: "ParameterLawDistribution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LawDistribution",
                table: "LawDistribution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterZoneConnection",
                table: "InterZoneConnection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalculationResult",
                table: "CalculationResult");

            migrationBuilder.DropColumn(
                name: "IsControlledSection",
                table: "StructureInterZoneConnection");

            migrationBuilder.DropColumn(
                name: "TitleObject",
                table: "StructureInterZoneConnection");

            migrationBuilder.DropColumn(
                name: "TitleSection",
                table: "StructureInterZoneConnection");

            migrationBuilder.DropColumn(
                name: "TypeObject",
                table: "StructureInterZoneConnection");

            migrationBuilder.RenameTable(
                name: "UnifiedPowerSystem",
                newName: "UnifiedSystems");

            migrationBuilder.RenameTable(
                name: "TimeCharacteristic",
                newName: "Times");

            migrationBuilder.RenameTable(
                name: "StructurePowerPlant",
                newName: "StructurePlants");

            migrationBuilder.RenameTable(
                name: "StructureInterZoneConnection",
                newName: "StructureConnections");

            migrationBuilder.RenameTable(
                name: "ReliabilityZone",
                newName: "Zones");

            migrationBuilder.RenameTable(
                name: "PowerSystem",
                newName: "PowerSystems");

            migrationBuilder.RenameTable(
                name: "PowerPlant",
                newName: "Plants");

            migrationBuilder.RenameTable(
                name: "ParameterLawDistribution",
                newName: "Parameters");

            migrationBuilder.RenameTable(
                name: "LawDistribution",
                newName: "Laws");

            migrationBuilder.RenameTable(
                name: "InterZoneConnection",
                newName: "Connections");

            migrationBuilder.RenameTable(
                name: "CalculationResult",
                newName: "Results");

            migrationBuilder.RenameIndex(
                name: "IX_TimeCharacteristic_ReliabilityZoneId",
                table: "Times",
                newName: "IX_Times_ReliabilityZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_StructurePowerPlant_PowerPlantId",
                table: "StructurePlants",
                newName: "IX_StructurePlants_PowerPlantId");

            migrationBuilder.RenameColumn(
                name: "UidSection",
                table: "StructureConnections",
                newName: "Uid");

            migrationBuilder.RenameColumn(
                name: "UidObject",
                table: "StructureConnections",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_StructureInterZoneConnection_InterZoneConnectionId",
                table: "StructureConnections",
                newName: "IX_StructureConnections_InterZoneConnectionId");

            migrationBuilder.RenameIndex(
                name: "IX_ReliabilityZone_PowerSystemId",
                table: "Zones",
                newName: "IX_Zones_PowerSystemId");

            migrationBuilder.RenameIndex(
                name: "IX_PowerSystem_UnifiedPowerSystemId",
                table: "PowerSystems",
                newName: "IX_PowerSystems_UnifiedPowerSystemId");

            migrationBuilder.RenameIndex(
                name: "IX_PowerPlant_ReliabilityZoneId",
                table: "Plants",
                newName: "IX_Plants_ReliabilityZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterLawDistribution_LawDistributionId",
                table: "Parameters",
                newName: "IX_Parameters_LawDistributionId");

            migrationBuilder.RenameIndex(
                name: "IX_InterZoneConnection_ReliabilityZoneId",
                table: "Connections",
                newName: "IX_Connections_ReliabilityZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_CalculationResult_TimeCharacteristicId",
                table: "Results",
                newName: "IX_Results_TimeCharacteristicId");

            migrationBuilder.RenameIndex(
                name: "IX_CalculationResult_ParameterLawDistributionId",
                table: "Results",
                newName: "IX_Results_ParameterLawDistributionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnifiedSystems",
                table: "UnifiedSystems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Times",
                table: "Times",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StructurePlants",
                table: "StructurePlants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StructureConnections",
                table: "StructureConnections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zones",
                table: "Zones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PowerSystems",
                table: "PowerSystems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plants",
                table: "Plants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parameters",
                table: "Parameters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Laws",
                table: "Laws",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Connections",
                table: "Connections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Results",
                table: "Results",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_Sections_PowerSystemId",
                table: "Sections",
                column: "PowerSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ReliabilityZoneId",
                table: "Sections",
                column: "ReliabilityZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_StructureSections_ControlledSectionId",
                table: "StructureSections",
                column: "ControlledSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Zones_ReliabilityZoneId",
                table: "Connections",
                column: "ReliabilityZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_Laws_LawDistributionId",
                table: "Parameters",
                column: "LawDistributionId",
                principalTable: "Laws",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Zones_ReliabilityZoneId",
                table: "Plants",
                column: "ReliabilityZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PowerSystems_UnifiedSystems_UnifiedPowerSystemId",
                table: "PowerSystems",
                column: "UnifiedPowerSystemId",
                principalTable: "UnifiedSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Parameters_ParameterLawDistributionId",
                table: "Results",
                column: "ParameterLawDistributionId",
                principalTable: "Parameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Times_TimeCharacteristicId",
                table: "Results",
                column: "TimeCharacteristicId",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StructureConnections_Connections_InterZoneConnectionId",
                table: "StructureConnections",
                column: "InterZoneConnectionId",
                principalTable: "Connections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StructurePlants_Plants_PowerPlantId",
                table: "StructurePlants",
                column: "PowerPlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Zones_ReliabilityZoneId",
                table: "Times",
                column: "ReliabilityZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Zones_PowerSystems_PowerSystemId",
                table: "Zones",
                column: "PowerSystemId",
                principalTable: "PowerSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
