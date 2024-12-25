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
	public class StructureSectionsConfiguration :
		IEntityTypeConfiguration<StructureControlledSection>
	{
		public void Configure(EntityTypeBuilder<StructureControlledSection> builder)
		{
			// Указывается ключ PK
			builder.HasKey(scs => scs.Id);

			// Указывается связь О-М между СКС и КС
			builder
				.HasOne(scs => scs.ControlledSection)
				.WithMany(cs => cs.StructureSections)
				.HasForeignKey(scs => scs.ControlledSectionId);
		}
	}
}
