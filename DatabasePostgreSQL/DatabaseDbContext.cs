using DatabasePostgreSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabasePostgreSQL
{
	/// <summary>
	/// Класс, для инициализации
	/// </summary>
	public class DatabaseDbContext : DbContext
	{
		/// <summary>
		/// Коллекция сущности "Результат расчета"
		/// </summary>
		public DbSet<CalculationResult> Results { get; set; }

		/// <summary>
		/// Коллекция сущности "Контролируемое сечение"
		/// </summary>
		public DbSet<ControlledSection> Sections { get; set; }

		/// <summary>
		/// Коллекция сущности "Внешний переток (межзонная связь)"
		/// </summary>
		public DbSet<InterZoneConnection> Connections { get; set; }

		/// <summary>
		/// Коллекция сущности "Закон распределения"
		/// </summary>
		public DbSet<LawDistribution> Laws { get; set; }

		/// <summary>
		/// Коллекция сущности "Параметры закона распределения"
		/// </summary>
		public DbSet<ParameterLawDistribution> Parameters { get; set; }

		/// <summary>
		/// Коллекция сущности "Электростанция"
		/// </summary>
		public DbSet<PowerPlant> Plants { get; set; }

		/// <summary>
		/// Коллекция сущности "Энергосистема"
		/// </summary>
		public DbSet<PowerSystem> PowerSystems { get; set; }

		/// <summary>
		/// Коллекция сущности "Зона надежности"
		/// </summary>
		public DbSet<ReliabilityZone> Zones { get; set; }

		/// <summary>
		/// Коллекция сущности "Состав КС"
		/// </summary>
		public DbSet<StructureControlledSection> StructureSections { get; set; }

		/// <summary>
		/// Коллекция сущности "Состав МЗС"
		/// </summary>
		public DbSet<StructureInterZoneConnection> StructureConnections { get; set; }

		/// <summary>
		/// Коллекция сущности "Состав ЭСТ"
		/// </summary>
		public DbSet<StructurePowerPlant> StructurePlants { get; set; }

		/// <summary>
		/// Коллекция сущности "Временная характеристика"
		/// </summary>
		public DbSet<TimeCharacteristic> Times { get; set; }

		/// <summary>
		/// Состав коллекции "Объединенная энергосистема"
		/// </summary>
		public DbSet<UnifiedPowerSystem> UnifiedSystems { get; set; }
	}
}
