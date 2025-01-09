using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		/// <summary>
		/// Атрибут "ID Объединенной энергосистемы (ID_ОЭС)" (FK)
		/// </summary>
		public int UnifiedPowerSystemId { get; set; }

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
	}
}
