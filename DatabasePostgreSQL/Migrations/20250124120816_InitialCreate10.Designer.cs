﻿// <auto-generated />
using DatabasePostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabasePostgreSQL.Migrations
{
    [DbContext(typeof(DatabaseDbContext))]
    [Migration("20250124120816_InitialCreate10")]
    partial class InitialCreate10
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DatabasePostgreSQL.Models.CalculationResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("DeviationValue")
                        .HasColumnType("double precision");

                    b.Property<int>("ParameterLawDistributionId")
                        .HasColumnType("integer");

                    b.Property<double>("ParameterValue")
                        .HasColumnType("double precision");

                    b.Property<int>("TimeCharacteristicId")
                        .HasColumnType("integer");

                    b.Property<double>("WeightFactor")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ParameterLawDistributionId");

                    b.HasIndex("TimeCharacteristicId");

                    b.ToTable("CalculationResult");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.InterZoneConnection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ReliabilityZoneId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ReliabilityZoneId");

                    b.ToTable("InterZoneConnection");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.LawDistribution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("LawDistribution");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.ParameterLawDistribution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("LawDistributionId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LawDistributionId");

                    b.ToTable("ParameterLawDistribution");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.PowerPlant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ReliabilityZoneId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ReliabilityZoneId");

                    b.ToTable("PowerPlant");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.PowerSystem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UnifiedPowerSystemId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UnifiedPowerSystemId");

                    b.ToTable("PowerSystem");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.ReliabilityZone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<int>("PowerSystemId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PowerSystemId");

                    b.ToTable("ReliabilityZone");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.StructureInterZoneConnection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("InterZoneConnectionId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsControlledSection")
                        .HasColumnType("boolean");

                    b.Property<string>("TitleObject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TitleSection")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TypeObject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UidObject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UidSection")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("InterZoneConnectionId");

                    b.ToTable("StructureInterZoneConnection");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.StructurePowerPlant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PowerPlantId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PowerPlantId");

                    b.ToTable("StructurePowerPlant");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.TimeCharacteristic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ReliabilityZoneId")
                        .HasColumnType("integer");

                    b.Property<int>("Step")
                        .HasColumnType("integer");

                    b.Property<string>("TimeStampEnd")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TimeStampStart")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ValueMax")
                        .HasColumnType("integer");

                    b.Property<int>("ValueMin")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ReliabilityZoneId");

                    b.ToTable("TimeCharacteristic");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.UnifiedPowerSystem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UnifiedPowerSystem");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.CalculationResult", b =>
                {
                    b.HasOne("DatabasePostgreSQL.Models.ParameterLawDistribution", "ParameterLawDistribution")
                        .WithMany("Results")
                        .HasForeignKey("ParameterLawDistributionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabasePostgreSQL.Models.TimeCharacteristic", "TimeCharacteristic")
                        .WithMany("Results")
                        .HasForeignKey("TimeCharacteristicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParameterLawDistribution");

                    b.Navigation("TimeCharacteristic");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.InterZoneConnection", b =>
                {
                    b.HasOne("DatabasePostgreSQL.Models.ReliabilityZone", "ReliabilityZone")
                        .WithMany("Connections")
                        .HasForeignKey("ReliabilityZoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReliabilityZone");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.ParameterLawDistribution", b =>
                {
                    b.HasOne("DatabasePostgreSQL.Models.LawDistribution", "LawDistribution")
                        .WithMany("Parameters")
                        .HasForeignKey("LawDistributionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LawDistribution");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.PowerPlant", b =>
                {
                    b.HasOne("DatabasePostgreSQL.Models.ReliabilityZone", "ReliabilityZone")
                        .WithMany("PowerPlants")
                        .HasForeignKey("ReliabilityZoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReliabilityZone");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.PowerSystem", b =>
                {
                    b.HasOne("DatabasePostgreSQL.Models.UnifiedPowerSystem", "UnifiedPowerSystem")
                        .WithMany("Systems")
                        .HasForeignKey("UnifiedPowerSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UnifiedPowerSystem");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.ReliabilityZone", b =>
                {
                    b.HasOne("DatabasePostgreSQL.Models.PowerSystem", "PowerSystem")
                        .WithMany("Zones")
                        .HasForeignKey("PowerSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PowerSystem");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.StructureInterZoneConnection", b =>
                {
                    b.HasOne("DatabasePostgreSQL.Models.InterZoneConnection", "InterZoneConnection")
                        .WithMany("StructureConnections")
                        .HasForeignKey("InterZoneConnectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InterZoneConnection");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.StructurePowerPlant", b =>
                {
                    b.HasOne("DatabasePostgreSQL.Models.PowerPlant", "PowerPlant")
                        .WithMany("Plants")
                        .HasForeignKey("PowerPlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PowerPlant");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.TimeCharacteristic", b =>
                {
                    b.HasOne("DatabasePostgreSQL.Models.ReliabilityZone", "ReliabilityZone")
                        .WithMany("TimeCharacteristics")
                        .HasForeignKey("ReliabilityZoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReliabilityZone");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.InterZoneConnection", b =>
                {
                    b.Navigation("StructureConnections");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.LawDistribution", b =>
                {
                    b.Navigation("Parameters");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.ParameterLawDistribution", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.PowerPlant", b =>
                {
                    b.Navigation("Plants");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.PowerSystem", b =>
                {
                    b.Navigation("Zones");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.ReliabilityZone", b =>
                {
                    b.Navigation("Connections");

                    b.Navigation("PowerPlants");

                    b.Navigation("TimeCharacteristics");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.TimeCharacteristic", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("DatabasePostgreSQL.Models.UnifiedPowerSystem", b =>
                {
                    b.Navigation("Systems");
                });
#pragma warning restore 612, 618
        }
    }
}
