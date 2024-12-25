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
	/// Конфигурация для сущности "Результат расчета"
	/// Интерфейс<Сущность>
	/// </summary>
	public class ResultsConfiguration : 
		IEntityTypeConfiguration<CalculationResult>
	{
		/// <summary>
		/// Метод реализации интерфейса
		/// </summary>
		/// <param name="builder"></param>
		public void Configure(EntityTypeBuilder<CalculationResult> builder)
		{
			// Указывается ключ PK
			builder.HasKey(cr => cr.Id);

			// Указывается связь О-М между результатом расчета (РР) и
			// временной характеристикой (ВХ)
			builder
				.HasOne(cr => cr.TimeCharacteristic)
				.WithMany(tc => tc.Results)
				.HasForeignKey(cr => cr.TimeCharacteristicId);

			// Указывается связь О-М между РР и
			// параметром закона распределения (ПЗР)
			builder
				.HasOne(cr => cr.ParameterLawDistribution)
				.WithMany(pld => pld.Results)
				.HasForeignKey(cr => cr.ParameterLawDistributionId);
		}
	}
}
