using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePostgreSQL.Models
{
	/// <summary>
	/// Сущность "Внешний переток (межзонная связь, МЗС)"
	/// (хранит названия МЗС)
	/// </summary>
	public class InterZoneConnection
	{
		/// <summary>
		/// Атрибут "ID Межзонной связи (ID_МЗС)" (PK).
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Атрибут "ID Зоны надежности (ID_ЗН)" (FK).
		/// </summary>
		public Guid ReliabilityZoneId { get; set; }

		/// <summary>
		/// Атрибут "Название МЗС".
		/// По умолчанию пустая строка.
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Ссылка на сущность "Зона надежности (ЗН)"
		/// </summary>
		public ReliabilityZone? ReliabilityZone { get; set; }

		/// <summary>
		/// Связь О-М между МЗС и СМЗС.
		/// Лист состава межзонной связи.
		/// По умолчанию пуст.
		/// </summary>
		public List<StructureInterZoneConnection> 
			StructureConnections { get; set; } = [];
	}
}
