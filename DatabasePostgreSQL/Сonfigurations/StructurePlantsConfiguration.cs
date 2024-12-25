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
	public class StructurePlantsConfiguration : 
		IEntityTypeConfiguration<StructurePowerPlant>
	{
		public void Configure(EntityTypeBuilder<StructurePowerPlant> builder)
		{
			// Указывается ключ PK
			builder.HasKey(spp => spp.Id);

			// Указывается связь О-М между СКС и КС
			builder
				.HasOne(spp => spp.PowerPlant)
				.WithMany(pp => pp.Plants)
				.HasForeignKey(spp => spp.PowerPlantId);
		}
	}
}
