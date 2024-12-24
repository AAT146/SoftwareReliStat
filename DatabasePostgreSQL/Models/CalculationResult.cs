using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePostgreSQL.Models
{
	/// <summary>
	/// Сущность "Результат расчета (РР)"
	/// </summary>
	public class CalculationResult
	{
		/// <summary>
		/// Атрибут "ID Результата расчета (ID_РР)" (PK).
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Атрибут "ID Временной характеристики (ID_ВХ)" (FK).
		/// </summary>
		public Guid TimeCharacteristicId { get; set; }

		/// <summary>
		/// Атрибут "Величина отклонения".
		/// </summary>
		public double DeviationValue { get; set; } = 0;

		/// <summary>
		/// Атрибут "Весовой коэффициент".
		/// </summary>
		public double WeightFactor { get; set; } = 0;

		/// <summary>
		/// Атрибут "ID Параметра Закона Распределения (ID_ПЗР)" (FK)
		/// </summary>
		public Guid ParameterLawDistributionId { get; set; }

		/// <summary>
		/// Атрибут "Значение параметра".
		/// </summary>
		public double ParameterValue { get; set; } = 0;

		/// <summary>
		/// Ссылка на сущность "Временная характеристика (ВХ)"
		/// </summary>
		public TimeCharacteristic? TimeCharacteristic { get; set; }

		/// <summary>
		/// Ссылка на сущность "Параметры законов распределения (ПЗР)"
		/// </summary>
		public ParameterLawDistribution? ParameterLawDistribution { get; set; }
	}
}
