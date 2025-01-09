using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		/// <summary>
		/// Атрибут "ID Зоны надежности (ID_ЗН)" (FK).
		/// </summary>
		public int ReliabilityZoneId { get; set; }

		/// <summary>
		/// Атрибут "Название ЭСТ".
		/// По умолчанию пустая строка.
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Ссылка на сущность "Зона надежности (ЗН)".
		/// </summary>
		public ReliabilityZone? ReliabilityZone { get; set; }

		/// <summary>
		/// Связь О-М между ЭСТ и СЭСТ.
		/// Лист состава ЭСТ (ГО).
		/// По умолчанию пуст.
		/// </summary>
		public List<StructurePowerPlant> Plants { get; set; } = [];
	}
}
