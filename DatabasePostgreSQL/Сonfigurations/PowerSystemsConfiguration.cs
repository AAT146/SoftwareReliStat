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
	public class PowerSystemsConfiguration : IEntityTypeConfiguration<PowerSystem>
	{
		public void Configure(EntityTypeBuilder<PowerSystem> builder)
		{
			// Указывается ключ PK
			builder.HasKey(ps => ps.Id);

			// Указывается связь О-М между ЭС и ОЭС
			builder
				.HasOne(ps => ps.UnifiedPowerSystem)
				.WithMany(ups => ups.Systems)
				.HasForeignKey(ps => ps.UnifiedPowerSystemId);

			// Указывается связь О-М между ЭС и ЗН
			builder
				.HasMany(ps => ps.Zones)
				.WithOne(ld => ld.PowerSystem);
		}
	}
}
