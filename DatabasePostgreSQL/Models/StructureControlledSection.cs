using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePostgreSQL.Models
{
	/// <summary>
	/// Сущность "Состав контролируемого сечения (СКС)"
	/// (хранит названия и UID ЛЭП, входящих в КС)
	/// </summary>
	public class StructureControlledSection
	{
		/// <summary>
		/// Атрибут "ID КС" (PK).
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Атрибут "ID Контролируемого сечения (ID_КС)" (PK).
		/// </summary>
		public Guid ControlledSectionId { get; set; }

		/// <summary>
		/// Атрибут "Название ЛЭП в КС".
		/// По умолчанию пустая строка.
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Атрибут "UID измерения (ЛЭП в КС)".
		/// По умолчанию пустая строка.
		/// </summary>
		public string Uid { get; set; } = string.Empty;

		/// <summary>
		/// Ссылка на сущность "Контролируемое сечение (КС)".
		/// </summary>
		public ControlledSection? ControlledSection { get; set; }
	}
}
