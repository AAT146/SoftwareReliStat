using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DatabasePostgreSQL
{
	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseDbContext>
	{
		public DatabaseDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<DatabaseDbContext>();
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = configuration.GetConnectionString("DefaultConnection");
			optionsBuilder.UseNpgsql(connectionString);

			return new DatabaseDbContext(optionsBuilder.Options);
		}
	}
}
