using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public Guid Id { get; set; }

		/// <summary>
		/// Атрибут "ID Энергосистемы (ID_ЭС)" (FK).
		/// </summary>
		public Guid PowerSystemId { get; set; }

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
