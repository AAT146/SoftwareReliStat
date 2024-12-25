using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePostgreSQL.Models
{
	/// <summary>
	/// Сущность "Параметры законов распределения (ПЗР)"
	/// (хранит названия параметров ЗР)
	/// </summary>
	public class ParameterLawDistribution
	{
		/// <summary>
		/// Атрибут "ID Параметра Закона Распределения (ID_ПЗР)" (PK)
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Атрибут "ID Закона распределения (ID_ЗР)" (FK)
		/// </summary>
		public Guid LawDistributionId { get; set; }

		/// <summary>
		/// Атрибут "Название параметра закона распределения"
		/// По умолчанию пустая строка
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Ссылка на сущность "Закон распределения (ЗР)"
		/// </summary>
		public LawDistribution? LawDistribution { get; set; }

		/// <summary>
		/// Связь О-М между ПЗР и РР.
		/// Лист параметров.
		/// По умолчанию пуст.
		/// </summary>
		public List<CalculationResult> Results = [];
	}
}
