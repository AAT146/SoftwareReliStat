using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePostgreSQL.Models
{
	/// <summary>
	/// Сущность "Состав электростанций (СЭСТ)" 
	/// (хранит названия ЭСТ)
	/// </summary>
	public class StructurePowerPlant
	{
		/// <summary>
		/// Атрибут "ID генерирующего оборудования (ID_ГО)" (PK).
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Атрибут "ID Электростанции (ID_ЭСТ)" (FK).
		/// </summary>
		public Guid PowerPlantId { get; set; }

		/// <summary>
		/// Атрибут "Название ГО".
		/// По умолчанию пустая строка.
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Атрибут "UID измерения (ГО)".
		/// По умолчанию пустая строка.
		/// </summary>
		public string Uid { get; set; } = string.Empty;

		/// <summary>
		/// Ссылка на сущность "Электростанция (ЭСТ)".
		/// </summary>
		public PowerPlant? PowerPlant { get; set; }
	}
}
