using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DatabasePostgreSQL.Models
{
	/// <summary>
	/// Сущность "Внешний переток (межзонная связь, МЗС)"
	/// (хранит названия МЗС)
	/// </summary>
	public class InterZoneConnection
	{
		/// <summary>
		/// Атрибут "ID Межзонной связи (ID_МЗС)" (PK).
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		/// <summary>
		/// Атрибут "ID Зоны надежности (ID_ЗН)" (FK).
		/// </summary>
		public int ReliabilityZoneId { get; set; }

		/// <summary>
		/// Атрибут "Название МЗС".
		/// По умолчанию пустая строка.
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Ссылка на сущность "Зона надежности (ЗН)"
		/// </summary>
		public ReliabilityZone? ReliabilityZone { get; set; }

		/// <summary>
		/// Связь О-М между МЗС и СМЗС.
		/// Лист состава межзонной связи.
		/// По умолчанию пуст.
		/// </summary>
		public List<StructureInterZoneConnection> 
			StructureConnections { get; set; } = [];
	}
}
