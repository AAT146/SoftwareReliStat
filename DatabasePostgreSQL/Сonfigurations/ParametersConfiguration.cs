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
	public class ParametersConfiguration : IEntityTypeConfiguration<ParameterLawDistribution>
	{
		public void Configure(EntityTypeBuilder<ParameterLawDistribution> builder)
		{
			// Указывается ключ PK
			builder.HasKey(pld => pld.Id);

			// Указывается связь О-М между ПРЗ и РР
			builder
				.HasMany(pld => pld.Results)
				.WithOne(rr => rr.ParameterLawDistribution);

			// Указывается связь О-М между ЗР и ПЗР
			builder
				.HasOne(pld => pld.LawDistribution)
				.WithMany(ld => ld.Parameters)
				.HasForeignKey(cr => cr.LawDistributionId);
		}
	}
}
