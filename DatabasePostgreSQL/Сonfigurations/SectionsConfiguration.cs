﻿using DatabasePostgreSQL.Models;
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
	/// Конфигурация для сущности "Контролируемое сечение"
	/// Интерфейс<Сущность>
	/// </summary>
	public class SectionsConfiguration : IEntityTypeConfiguration<ControlledSection>
	{
		/// <summary>
		/// Метод реализации интерфейса
		/// </summary>
		/// <param name="builder"></param>
		public void Configure(EntityTypeBuilder<ControlledSection> builder)
		{
			// Указывается ключ PK
			builder.HasKey(cs => cs.Id);

			// Указывается связь О-М между контролируемым сечением (КС) и
			// составом КС
			builder
				.HasMany(cs => cs.StructureSections)
				.WithOne(scs => scs.ControlledSection);

			// Указывается связь О-М между КС и зоной надежности (ЗН)
			builder
				.HasOne(cs => cs.ReliabilityZone)
				.WithMany(rz => rz.Sections)
				.HasForeignKey(cs => cs.ReliabilityZoneId);

			// Указывается связь О-М между КС и энергосистемой (ЭС)
			builder
				.HasOne(cs => cs.PowerSystem)
				.WithMany(ps => ps.Sections)
				.HasForeignKey(cs => cs.PowerSystemId);
		}
	}
}
