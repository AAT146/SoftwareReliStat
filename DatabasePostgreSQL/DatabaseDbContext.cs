using DatabasePostgreSQL.Models;
using DatabasePostgreSQL.Сonfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;


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
			modelBuilder.ApplyConfiguration(new SectionsConfiguration());
			modelBuilder.ApplyConfiguration(new StructureConnectionsConfiguration());
			modelBuilder.ApplyConfiguration(new StructurePlantsConfiguration());
			modelBuilder.ApplyConfiguration(new StructureSectionsConfiguration());
			modelBuilder.ApplyConfiguration(new TimesConfiguration());
			modelBuilder.ApplyConfiguration(new UnifiedSystemsConfiguration());
			modelBuilder.ApplyConfiguration(new ZonesConfiguration());

			base.OnModelCreating(modelBuilder);
		}

		/// <summary>
		/// Метод: подключение к базе данных PostgreSQL
		/// </summary>
		/// <param name="optionsBuilder"></param>
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				var configuration = new ConfigurationBuilder()
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appsettings.json")
					.Build();

				optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
			}
		}
	}
}
