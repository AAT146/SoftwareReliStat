using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DatabasePostgreSQL.Models
{
	/// <summary>
	/// Сущность "Временная характеристика (ВХ)"
	/// </summary>
	public class TimeCharacteristic
	{
		/// <summary>
		/// Атрибут "ID Временной характеристики (ID_ВХ)" (PK).
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		/// <summary>
		/// Атрибут "ID Зоны надежности (ID_ЗН)" (FK).
		/// </summary>
		public int ReliabilityZoneId { get; set; }

		/// <summary>
		/// Атрибут "Начальная метка времени"
		/// </summary>
		public string TimeStampStart { get; set; } = 
			DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

		/// <summary>
		/// Атрибут "Конечная метка времени"
		/// </summary>
		public string TimeStampEnd { get; set; } = 
			DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

		/// <summary>
		/// Атрибуд "Шаг дискретизации"
		/// </summary>
		public int Step { get; set; } = 0;

		/// <summary>
		/// Атрибут "Минимальное значение"
		/// </summary>
		public int ValueMin { get; set; } = 0;

		/// <summary>
		/// Атрибут "Максимальное значение"
		/// </summary>
		public int ValueMax { get; set; } = 0;

		/// <summary>
		/// Ссылка на сущность "Зона надежности (ЗН)"
		/// </summary>
		public ReliabilityZone? ReliabilityZone { get; set; }

		/// <summary>
		/// Связь О-М между ВХ и РР.
		/// Лист параметров.
		/// По умолчанию пуст.
		/// </summary>
		public List<CalculationResult> Results = [];
	}
}
