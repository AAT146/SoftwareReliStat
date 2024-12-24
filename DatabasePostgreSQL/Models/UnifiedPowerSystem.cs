using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePostgreSQL.Models
{
	/// <summary>
	/// Сущность "Объединенная энергосистема (ОЭС)"
	/// (хранит названия ОЭС)
	/// </summary>
	public class UnifiedPowerSystem
	{
		/// <summary>
		/// Атрибут "ID Объединенной энергосистемы (ID_ОЭС)" (PK).
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Атрибут "Название ОЭС".
		/// По умолчанию пустая строка.
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Связь О-М между ОЭС и ЭС.
		/// Лист энергосистем.
		/// По умолчанию пуст.
		/// </summary>
		public List<PowerSystem> Systems { get; set; } = [];
	}
}
