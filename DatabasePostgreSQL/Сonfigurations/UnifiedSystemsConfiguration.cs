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
	public class UnifiedSystemsConfiguration : IEntityTypeConfiguration<UnifiedPowerSystem>
	{
		public void Configure(EntityTypeBuilder<UnifiedPowerSystem> builder)
		{
			// Указывается ключ PK
			builder.HasKey(ups => ups.Id);

			// Указывается связь О-М между ОЭС и ЭС
			builder
				.HasMany(ups => ups.Systems)
				.WithOne(ps => ps.UnifiedPowerSystem);
		}
	}
}
