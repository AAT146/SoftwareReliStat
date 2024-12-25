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
	public class ZonesConfiguration : IEntityTypeConfiguration<ReliabilityZone>
	{
		public void Configure(EntityTypeBuilder<ReliabilityZone> builder)
		{
			// Указывается ключ PK
			builder.HasKey(rz => rz.Id);

			// Указывается связь О-М между ЗН и ЭС
			builder
				.HasOne(rz => rz.PowerSystem)
				.WithMany(ps => ps.Zones)
				.HasForeignKey(rz => rz.PowerSystemId);

			// Указывается связь О-М между ЗН и ЭСТ
			builder
				.HasMany(rz => rz.PowerPlants)
				.WithOne(pp => pp.ReliabilityZone);

			// Указывается связь О-М между ЗН и ВХ
			builder
				.HasMany(rz => rz.TimeCharacteristics)
				.WithOne(pp => pp.ReliabilityZone);

			// Указывается связь О-М между ЗН и МЗС
			builder
				.HasMany(rz => rz.Connections)
				.WithOne(izc => izc.ReliabilityZone);

			// Указывается связь О-М между ЗН и КС
			builder
				.HasMany(rz => rz.Sections)
				.WithOne(izc => izc.ReliabilityZone);
		}
	}
}
