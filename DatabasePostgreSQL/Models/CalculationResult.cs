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
		/// Атрибут "Кластер №"
		/// </summary>
		public int Cluste {  get; set; } = 0;

		/// <summary>
		/// Атрибут "Интервал"
		/// </summary>
		public string Interval { get; set; } = string.Empty;

		/// <summary>
		/// Атрибут "Весовой коэффициент".
		/// </summary>
		public double WeightFactor { get; set; } = 0;

		/// <summary>
		/// Атрибут "ID Закона Распределения (ID_ПЗР)" (FK)
		/// </summary>
		public int LawDistributionId { get; set; }

		/// <summary>
		/// Атрибут "Значение параметров".
		/// </summary>
		public string ParameterValue { get; set; } = string.Empty;

		/// <summary>
		/// Атрибут "Величина отклонения".
		/// </summary>
		public double ValueDeviation { get; set; } = 0;

		/// <summary>
		/// Ссылка на сущность "Временная характеристика (ВХ)"
		/// </summary>
		public TimeCharacteristic? TimeCharacteristic { get; set; }

		/// <summary>
		/// Ссылка на сущность "Закон распределения (ЗР)"
		/// </summary>
		public LawDistribution? LawDistribution { get; set; }
	}
}
