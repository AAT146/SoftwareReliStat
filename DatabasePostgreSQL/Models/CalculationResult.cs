using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		/// <summary>
		/// Атрибут "ID Временной характеристики (ID_ВХ)" (FK).
		/// </summary>
		public int TimeCharacteristicId { get; set; }

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
		public int ParameterLawDistributionId { get; set; }

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
