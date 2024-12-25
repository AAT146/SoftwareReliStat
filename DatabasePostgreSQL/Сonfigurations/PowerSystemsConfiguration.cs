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
	/// Конфигурация для сущности "Энергосистема"
	/// Интерфейс<Сущность>
	/// </summary>
	public class PowerSystemsConfiguration : 
		IEntityTypeConfiguration<PowerSystem>
	{
		/// <summary>
		/// Метод реализации интерфейса
		/// </summary>
		/// <param name="builder"></param>
		public void Configure(EntityTypeBuilder<PowerSystem> builder)
		{
			// Указывается ключ PK
			builder.HasKey(ps => ps.Id);

			// Указывается связь О-М между энергосистемой (ЭС) и
			// объединнной энергосистемой (ОЭС)
			builder
				.HasOne(ps => ps.UnifiedPowerSystem)
				.WithMany(ups => ups.Systems)
				.HasForeignKey(ps => ps.UnifiedPowerSystemId);

			// Указывается связь О-М между ЭС и зоной надежности (ЗН)
			builder
				.HasMany(ps => ps.Zones)
				.WithOne(ld => ld.PowerSystem);
		}
	}
}
