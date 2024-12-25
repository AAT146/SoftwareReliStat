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
	/// Конфигурация для сущности "Закон распределения"
	/// Интерфейс<Сущность>
	/// </summary>
	public class LawsConfiguration : 
		IEntityTypeConfiguration<LawDistribution>
	{
		/// <summary>
		/// Метод реализации интерфейса
		/// </summary>
		/// <param name="builder"></param>
		public void Configure(EntityTypeBuilder<LawDistribution> builder)
		{
			// Указывается ключ PK
			builder.HasKey(ld => ld.Id);

			// Указывается связь О-М между законом распределения (ЗР) и
			// параметром ЗР
			builder
				.HasMany(ld => ld.Parameters)
				.WithOne(pld => pld.LawDistribution);
		}
	}
}
