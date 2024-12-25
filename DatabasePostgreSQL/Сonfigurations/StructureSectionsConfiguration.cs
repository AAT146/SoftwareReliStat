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
	/// <summary>
	/// Конфигурация для сущности "Состав контролируемого сечения"
	/// Интерфейс<Сущность>
	/// </summary>
	public class StructureSectionsConfiguration :
		IEntityTypeConfiguration<StructureControlledSection>
	{
		/// <summary>
		/// Метод реализации интерфейса
		/// </summary>
		/// <param name="builder"></param>
		public void Configure(EntityTypeBuilder<StructureControlledSection> builder)
		{
			// Указывается ключ PK
			builder.HasKey(scs => scs.Id);

			// Указывается связь О-М между составом контролируемого сечения (СКС) и
			// контролируемым сечением (КС)
			builder
				.HasOne(scs => scs.ControlledSection)
				.WithMany(cs => cs.StructureSections)
				.HasForeignKey(scs => scs.ControlledSectionId);
		}
	}
}
