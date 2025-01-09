using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

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
