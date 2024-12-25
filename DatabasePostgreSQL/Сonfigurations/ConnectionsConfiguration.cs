using DatabasePostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePostgreSQL.Сonfigurations
{
	/// <summary>
	/// Конфигурация для сущности "Межзонная связь"
	/// Интерфейс<Сущность>
	/// </summary>
	public class ConnectionsConfiguration : 
		IEntityTypeConfiguration<InterZoneConnection>
	{
		/// <summary>
		/// Метод реализации интерфейса
		/// </summary>
		/// <param name="builder"></param>
		public void Configure(EntityTypeBuilder<InterZoneConnection> builder)
		{
			// Указывается ключ PK
			builder.HasKey(izc => izc.Id);

			// Указывается связь О-М между межзонной связью (МЗС) и
			// составом МЗС
			builder
				.HasMany(izc => izc.StructureConnections)
				.WithOne(sizc => sizc.InterZoneConnection);

			// Указывается связь О-М между зоной надежности (ЗН) и МЗС
			builder
				.HasOne(izc => izc.ReliabilityZone)
				.WithMany(rz => rz.Connections)
				.HasForeignKey(izc => izc.ReliabilityZoneId);
		}
	}
}
