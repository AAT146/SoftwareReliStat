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
	/// Конфигурация для сущности "Электростанция"
	/// Интерфейс<Сущность>
	/// </summary>
	public class PlantsConfiguration : 
		IEntityTypeConfiguration<PowerPlant>
	{
		/// <summary>
		/// Метод реализации интерфейса
		/// </summary>
		/// <param name="builder"></param>
		public void Configure(EntityTypeBuilder<PowerPlant> builder)
		{
			// Указывается ключ PK
			builder.HasKey(pp => pp.Id);

			// Указывается связь О-М между зоной надежности (ЗН) и
			// электростанцией (ЭСТ)
			builder
				.HasOne(pp => pp.ReliabilityZone)
				.WithMany(rz => rz.PowerPlants)
				.HasForeignKey(pp => pp.ReliabilityZoneId);

			// Указывается связь О-М между ЭСТ и составом ЭСТ
			builder
				.HasMany(pp => pp.Plants)
				.WithOne(spp => spp.PowerPlant);
		}
	}
}
