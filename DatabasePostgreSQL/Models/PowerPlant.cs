using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePostgreSQL.Models
{
	/// <summary>
	/// Сущность "Электростанция (ЭСТ)" 
	/// (хранит названия ЭСТ)
	/// </summary>
	public class PowerPlant
	{
		/// <summary>
		/// Атрибут "ID Электростанции (ID_ЭСТ)" (PK).
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Атрибут "ID Зоны надежности (ID_ЗН)" (FK).
		/// </summary>
		public Guid ReliabilityZoneId { get; set; }

		/// <summary>
		/// Атрибут "Название ЭСТ".
		/// По умолчанию пустая строка.
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Ссылка на сущность "Зона надежности (ЗН)".
		/// </summary>
		public ReliabilityZone? ReliabilityZone { get; set; }


	}
}
