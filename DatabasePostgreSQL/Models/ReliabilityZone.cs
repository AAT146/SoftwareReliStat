using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DatabasePostgreSQL.Models
{
	/// <summary>
	/// Сущность "Зона надежности (ЗН)"
	/// (хранит названия ЗН)
	/// </summary>
	public class ReliabilityZone
	{
		/// <summary>
		/// Атрибут "ID Зоны надежности (ID_ЗН)" (PK).
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		/// <summary>
		/// Атрибут "ID Энергосистемы (ID_ЭС)" (FK).
		/// </summary>
		public int PowerSystemId { get; set; }


		/// <summary>
		/// Атрибут "Номер ЗН".
		/// </summary>
		public int Number {  get; set; }

		/// <summary>
		/// Атрибут "Название ЗН".
		/// По умолчанию пустая строка.
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Ссылка на сущность "Энергосистема (ЭС)"
		/// </summary>
		public PowerSystem? PowerSystem { get; set; }

		/// <summary>
		/// Связь О-М между ЗН и МЗС.
		/// Лист межзонный связей.
		/// По умолчанию пуст.
		/// </summary>
		public List<InterZoneConnection> Connections { get; set; } = [];

		/// <summary>
		/// Связь О-М между ЗН и ЭСТ.
		/// Лист электростанций.
		/// По умолчанию пуст.
		/// </summary>
		public List<PowerPlant> PowerPlants { get; set; } = [];

		/// <summary>
		/// Связь О-М между ЗН и ВХ.
		/// Лист временных характеристик.
		/// По умолчанию пуст.
		/// </summary>
		public List<TimeCharacteristic> TimeCharacteristics { get; set; } = [];
	}
}
