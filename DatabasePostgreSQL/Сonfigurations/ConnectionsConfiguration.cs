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
	public class ConnectionsConfiguration : IEntityTypeConfiguration<InterZoneConnection>
	{
		public void Configure(EntityTypeBuilder<InterZoneConnection> builder)
		{
			// Указывается ключ PK
			builder.HasKey(izc => izc.Id);

			// Указывается связь О-М между МЗС и СМЗС
			builder
				.HasMany(izc => izc.StructureConnections)
				.WithOne(sizc => sizc.InterZoneConnection);

			// Указывается связь О-М между ЗН и МЗС
			builder
				.HasOne(izc => izc.ReliabilityZone)
				.WithMany(rz => rz.Connections)
				.HasForeignKey(izc => izc.ReliabilityZoneId);
		}
	}
}
