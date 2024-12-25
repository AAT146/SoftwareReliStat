using DatabasePostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePostgreSQL.Сonfigurations
{
	public class PlantsConfiguration : IEntityTypeConfiguration<PowerPlant>
	{
		public void Configure(EntityTypeBuilder<PowerPlant> builder)
		{
			// Указывается ключ PK
			builder.HasKey(pp => pp.Id);

			// Указывается связь О-М между ЗН и ЭСТ
			builder
				.HasOne(pp => pp.ReliabilityZone)
				.WithMany(rz => rz.PowerPlants)
				.HasForeignKey(pp => pp.ReliabilityZoneId);

			// Указывается связь О-М между ЭСТ и СЭСТ
			builder
				.HasMany(pp => pp.Plants)
				.WithOne(spp => spp.PowerPlant);
		}
	}
}
