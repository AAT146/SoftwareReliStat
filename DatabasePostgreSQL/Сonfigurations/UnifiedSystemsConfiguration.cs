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
	/// Конфигурация для сущности "Объединенная энергосистема"
	/// Интерфейс<Сущность>
	/// </summary>
	public class UnifiedSystemsConfiguration : 
		IEntityTypeConfiguration<UnifiedPowerSystem>
	{
		/// <summary>
		/// Метод реализации интерфейса
		/// </summary>
		/// <param name="builder"></param>
		public void Configure(EntityTypeBuilder<UnifiedPowerSystem> builder)
		{
			// Указывается ключ PK
			builder.HasKey(ups => ups.Id);

			// Указывается связь О-М между объединенной энергосистемой (ОЭС) и
			// энергосистемой (ЭС)
			builder
				.HasMany(ups => ups.Systems)
				.WithOne(ps => ps.UnifiedPowerSystem);
		}
	}
}
