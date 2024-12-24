using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePostgreSQL.Models
{
	/// <summary>
	/// Сущность "Контролируемое сечение (КС)"
	/// (хранит названия КС)
	/// </summary>
	public class ControlledSection
	{
		/// <summary>
		/// Атрибут "ID Контролируемого сечения (ID_КС)" (PK).
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Атрибут "ID Энергосистемы (ID_ЭС)" (FK).
		/// </summary>
		public Guid PowerSystemId { get; set; }

		/// <summary>
		/// Атрибут "ID Зоны надежности (ID_ЗН)" (FK).
		/// </summary>
		public Guid ReliabilityZoneId { get; set; }

		/// <summary>
		/// Атрибут "Название КС".
		/// По умолчанию пустая строка.
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Ссылка на сущность "Энергосистема (ЭС)".
		/// </summary>
		public PowerSystem? PowerSystem { get; set; }

		/// <summary>
		/// Ссылка на сущность "Зона надежности (ЗН)".
		/// </summary>
		public ReliabilityZone? ReliabilityZone { get; set; }

		/// <summary>
		/// Связь О-М между КС и СКС.
		/// Лист состава контролируемого сечения.
		/// По умолчанию пуст.
		/// </summary>
		public List<StructureControlledSection> 
			StructureSections { get; set; } = [];
	}
}
