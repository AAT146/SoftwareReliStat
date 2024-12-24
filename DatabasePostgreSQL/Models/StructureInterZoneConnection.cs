using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePostgreSQL.Models
{
	/// <summary>
	/// Сущность "Состав межзонной связи (СМЗС)"
	/// (хранит названия и UID ЛЭП, входящих в МЗС)
	/// </summary>
	public class StructureInterZoneConnection
	{
		/// <summary>
		/// Атрибут "ID ЛЭП" (PK).
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Атрибут "ID Межзонной связи (ID_МЗС)" (FK).
		/// </summary>
		public Guid InterZoneConnectionId { get; set; }

		/// <summary>
		/// Атрибут "Название ЛЭП в МЗС".
		/// По умолчанию пустая строка.
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Атрибут "UID измерения (ЛЭП к МЗС)".
		/// По умолчанию пустая строка.
		/// </summary>
		public string Uid { get; set; } = string.Empty;

		/// <summary>
		/// Ссылка на сущность "Внешний переток (межзонная связь, МЗС)"
		/// </summary>
		public InterZoneConnection? InterZoneConnection { get; set; }
	}
}
