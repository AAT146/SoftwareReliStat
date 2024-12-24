using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePostgreSQL.Models
{
	/// <summary>
	/// Сущность "Энергосистема (ЭС)" 
	/// (хранит названия ЭС)
	/// </summary>
	public class PowerSystem
	{
		/// <summary>
		/// Атрибут "ID Энергосистемы (ID_ЭС)" (PK).
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Атрибут "ID Объединенной энергосистемы (ID_ОЭС)" (FK)
		/// </summary>
		public Guid UnifiedPowerSystemId { get; set; }

		/// <summary>
		/// Атрибут "Название ЭС".
		/// По умолчанию пустая строка.
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Ссылка на сущность "Объединенная энергосистема (ОЭС)"
		/// </summary>
		public UnifiedPowerSystem? UnifiedPowerSystem { get; set; }

		/// <summary>
		/// Связь О-М между ЭС и ЗН.
		/// Лист зон надежности.
		/// По умолчанию пуст.
		/// </summary>
		public List<ReliabilityZone> Zones { get; set; } = [];

		/// <summary>
		/// Связь О-М между ЭС и КС.
		/// Лист контролируемых сечений.
		/// По умолчанию пуст.
		/// </summary>
		public List<ControlledSection> Sections { get; set; } = [];
	}
}
