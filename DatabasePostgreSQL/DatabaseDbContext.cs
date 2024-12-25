using DatabasePostgreSQL.Models;
using DatabasePostgreSQL.Сonfigurations;
using Microsoft.EntityFrameworkCore;

namespace DatabasePostgreSQL
{
	/// <summary>
	/// Класс, для инициализации
	/// </summary>
	public class DatabaseDbContext : DbContext
	{
		/// <summary>
		/// Конструктор по умолчанию
		/// </summary>
		/// <param name="options"></param>
		public DatabaseDbContext(DbContextOptions<DatabaseDbContext> options)
			: base(options)
		{
		}

		/// <summary>
		/// Коллекция сущности "Результат расчета"
		/// </summary>
		public DbSet<CalculationResult> 
			CalculationResult { get; set; }

		/// <summary>
		/// Коллекция сущности "Внешний переток (межзонная связь)"
		/// </summary>
		public DbSet<InterZoneConnection> 
			InterZoneConnection { get; set; }

		/// <summary>
		/// Коллекция сущности "Закон распределения"
		/// </summary>
		public DbSet<LawDistribution> 
			LawDistribution { get; set; }

		/// <summary>
		/// Коллекция сущности "Параметры закона распределения"
		/// </summary>
		public DbSet<ParameterLawDistribution> 
			ParameterLawDistribution { get; set; }

		/// <summary>
		/// Коллекция сущности "Электростанция"
		/// </summary>
		public DbSet<PowerPlant> PowerPlant { get; set; }

		/// <summary>
		/// Коллекция сущности "Энергосистема"
		/// </summary>
		public DbSet<PowerSystem> PowerSystem { get; set; }

		/// <summary>
		/// Коллекция сущности "Зона надежности"
		/// </summary>
		public DbSet<ReliabilityZone> ReliabilityZone { get; set; }

		/// <summary>
		/// Коллекция сущности "Характеристика МЗС"
		/// </summary>
		public DbSet<StructureInterZoneConnection> StructureInterZoneConnection { get; set; }

		/// <summary>
		/// Коллекция сущности "Состав ЭСТ"
		/// </summary>
		public DbSet<StructurePowerPlant> StructurePowerPlant { get; set; }

		/// <summary>
		/// Коллекция сущности "Временная характеристика"
		/// </summary>
		public DbSet<TimeCharacteristic> TimeCharacteristic { get; set; }

		/// <summary>
		/// Состав коллекции "Объединенная энергосистема"
		/// </summary>
		public DbSet<UnifiedPowerSystem> UnifiedPowerSystem { get; set; }

		/// <summary>
		/// Метод: применение конфигураций 
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new ConnectionsConfiguration());
			modelBuilder.ApplyConfiguration(new LawsConfiguration());
			modelBuilder.ApplyConfiguration(new ParametersConfiguration());
			modelBuilder.ApplyConfiguration(new PlantsConfiguration());
			modelBuilder.ApplyConfiguration(new PowerSystemsConfiguration());
			modelBuilder.ApplyConfiguration(new ResultsConfiguration());
			modelBuilder.ApplyConfiguration(new StructureConnectionsConfiguration());
			modelBuilder.ApplyConfiguration(new StructurePlantsConfiguration());
			modelBuilder.ApplyConfiguration(new TimesConfiguration());
			modelBuilder.ApplyConfiguration(new UnifiedSystemsConfiguration());
			modelBuilder.ApplyConfiguration(new ZonesConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
