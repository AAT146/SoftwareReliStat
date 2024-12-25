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
	/// Конфигурация для сущности "Состав межзонной связи"
	/// Интерфейс<Сущность>
	/// </summary>
	public class StructureConnectionsConfiguration :
		IEntityTypeConfiguration<StructureInterZoneConnection>
	{
		/// <summary>
		/// Метод реализации интерфейса
		/// </summary>
		/// <param name="builder"></param>
		public void Configure(EntityTypeBuilder<StructureInterZoneConnection> builder)
		{
			// Указывается ключ PK
			builder.HasKey(sizc => sizc.Id);

			// Указывается связь О-М между составом межзонной связи (СМЗС) и
			// межзонной связью (МЗС)
			builder
				.HasOne(sizc => sizc.InterZoneConnection)
				.WithMany(izc => izc.StructureConnections)
				.HasForeignKey(sizc => sizc.InterZoneConnectionId);
		}
	}
}
