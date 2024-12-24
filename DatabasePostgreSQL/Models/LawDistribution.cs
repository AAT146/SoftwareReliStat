using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePostgreSQL.Models
{
	/// <summary>
	/// Сущность "Закон распределения (ЗР)" (хранит названия ЗР)
	/// </summary>
	public class LawDistribution
	{
		/// <summary>
		/// Атрибут "ID Закона распределения (ID_ЗР)" (PK).
		/// </summary>
		public Guid Id { get; set; }

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
	}
}
