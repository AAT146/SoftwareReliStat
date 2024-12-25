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
	/// Конфигурация для сущности "Временная характеристика"
	/// Интерфейс<Сущность>
	/// </summary>
	public class TimesConfiguration : IEntityTypeConfiguration<TimeCharacteristic>
	{
		/// <summary>
		/// Метод реализации интерфейса
		/// </summary>
		/// <param name="builder"></param>
		public void Configure(EntityTypeBuilder<TimeCharacteristic> builder)
		{
			// Указывается ключ PK
			builder.HasKey(tc => tc.Id);

			// Указывается связь О-М между временной характеристикой (ВХ) и
			// результатом расчета (РР)
			builder
				.HasMany(tc => tc.Results)
				.WithOne(cr => cr.TimeCharacteristic);

			// Указывается связь О-М между зоной надежности (ЗН) и ВХ
			builder
				.HasOne(tc => tc.ReliabilityZone)
				.WithMany(rz => rz.TimeCharacteristics)
				.HasForeignKey(tc => tc.ReliabilityZoneId);
		}
	}
}
