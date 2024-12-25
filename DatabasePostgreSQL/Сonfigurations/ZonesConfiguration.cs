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
	/// Конфигурация для сущности "Зона надежности"
	/// Интерфейс<Сущность>
	/// </summary>
	public class ZonesConfiguration : 
		IEntityTypeConfiguration<ReliabilityZone>
	{
		/// <summary>
		/// Метод реализации интерфейса
		/// </summary>
		/// <param name="builder"></param>
		public void Configure(EntityTypeBuilder<ReliabilityZone> builder)
		{
			// Указывается ключ PK
			builder.HasKey(rz => rz.Id);

			// Указывается связь О-М между зоной надежности (ЗН) и
			// энергосистемой (ЭС)
			builder
				.HasOne(rz => rz.PowerSystem)
				.WithMany(ps => ps.Zones)
				.HasForeignKey(rz => rz.PowerSystemId);

			// Указывается связь О-М между ЗН и
			// электростанцией (ЭСТ)
			builder
				.HasMany(rz => rz.PowerPlants)
				.WithOne(pp => pp.ReliabilityZone);

			// Указывается связь О-М между ЗН и
			// временной характеристикой (ВХ)
			builder
				.HasMany(rz => rz.TimeCharacteristics)
				.WithOne(pp => pp.ReliabilityZone);

			// Указывается связь О-М между ЗН и
			// межзонной связью (МЗС)
			builder
				.HasMany(rz => rz.Connections)
				.WithOne(izc => izc.ReliabilityZone);

			// Указывается связь О-М между ЗН и
			// контролируемым сечением (КС)
			builder
				.HasMany(rz => rz.Sections)
				.WithOne(izc => izc.ReliabilityZone);
		}
	}
}
