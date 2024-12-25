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
	public class LawsConfiguration : IEntityTypeConfiguration<LawDistribution>
	{
		public void Configure(EntityTypeBuilder<LawDistribution> builder)
		{
			// Указывается ключ PK
			builder.HasKey(ld => ld.Id);

			// Указывается связь О-М между ЗР и ПЗР
			builder
				.HasMany(ld => ld.Parameters)
				.WithOne(pld => pld.LawDistribution);
		}
	}
}
