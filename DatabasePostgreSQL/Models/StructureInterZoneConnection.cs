using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePostgreSQL.Models
{
	/// <summary>
	/// Сущность "Характеристика межзонной связи (ХМЗС)"
	/// (хранит информацию об объектах МЗС)
	/// </summary>
	public class StructureInterZoneConnection
	{
		/// <summary>
		/// Атрибут "ID ХМЗС" (PK).
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Атрибут "ID Межзонной связи (ID_МЗС)" (FK).
		/// </summary>
		public Guid InterZoneConnectionId { get; set; }

		/// <summary>
		/// Атрибут "Тип объекта в МЗС".
		/// По умолчанию пустая строка.
		/// </summary>
		public string TypeObject { get; set; } = string.Empty;

		/// <summary>
		/// Атрибут "Название объекта в МЗС".
		/// По умолчанию пустая строка.
		/// </summary>
		public string TitleObject { get; set; } = string.Empty;

		/// <summary>
		/// Атрибут "UID измерения (Pij по ЛЭП)".
		/// По умолчанию пустая строка.
		/// </summary>
		public string UidObject { get; set; } = string.Empty;

		/// <summary>
		/// Атрибут "Принадлежность к КС".
		/// По умолчанию false.
		/// </summary>
		public bool IsControlledSection { get; set; } = false;

		/// <summary>
		/// Атрибут "Название КС".
		/// По умолчанию пустая строка.
		/// </summary>
		public string TitleSection { get; set; } = string.Empty;

		/// <summary>
		/// Атрибут "UID измерения (МДП КС)".
		/// По умолчанию пустая строка.
		/// </summary>
		public string UidSection { get; set; } = string.Empty;

		/// <summary>
		/// Ссылка на сущность "Внешний переток (межзонная связь, МЗС)"
		/// </summary>
		public InterZoneConnection? InterZoneConnection { get; set; }
	}
}
