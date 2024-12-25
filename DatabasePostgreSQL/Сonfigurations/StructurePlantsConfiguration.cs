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
	/// Конфигурация для сущности "Состав электростанций"
	/// Интерфейс<Сущность>
	/// </summary>
	public class StructurePlantsConfiguration : 
		IEntityTypeConfiguration<StructurePowerPlant>
	{
		/// <summary>
		/// Метод реализации интерфейса
		/// </summary>
		/// <param name="builder"></param>
		public void Configure(EntityTypeBuilder<StructurePowerPlant> builder)
		{
			// Указывается ключ PK
			builder.HasKey(spp => spp.Id);

			// Указывается связь О-М между составом электростанций (СЭСТ) и
			// электростанциями (ЭСТ)
			builder
				.HasOne(spp => spp.PowerPlant)
				.WithMany(pp => pp.Plants)
				.HasForeignKey(spp => spp.PowerPlantId);
		}
	}
}
