using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DatabasePostgreSQL.Models
{
	/// <summary>
	/// Сущность "Закон распределения (ЗР)" 
	/// (хранит названия ЗР)
	/// </summary>
	public class LawDistribution
	{
		/// <summary>
		/// Атрибут "ID Закона распределения (ID_ЗР)" (PK).
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		/// <summary>
		/// Атрибут "Название закона распределения".
		/// По умолчанию пустая строка.
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Связь О-М между ЗР и ПЗР.
		/// Лист параметров.
		/// По умолчанию пуст.
		/// </summary>
		public List<ParameterLawDistribution> Parameters { get; set; } = [];

		/// <summary>
		/// Связь О-М между ЗР и РР.
		/// Лист законов распределения.
		/// По умолчанию пуст.
		/// </summary>
		public List<CalculationResult> CalculationResults { get; set; } = [];
	}
}
