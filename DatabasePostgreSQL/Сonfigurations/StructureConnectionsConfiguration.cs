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
	public class StructureConnectionsConfiguration :
		IEntityTypeConfiguration<StructureInterZoneConnection>
	{
		public void Configure(EntityTypeBuilder<StructureInterZoneConnection> builder)
		{
			// Указывается ключ PK
			builder.HasKey(sizc => sizc.Id);

			// Указывается связь О-М между СМЗС и МЗС
			builder
				.HasOne(sizc => sizc.InterZoneConnection)
				.WithMany(izc => izc.StructureConnections)
				.HasForeignKey(sizc => sizc.InterZoneConnectionId);
		}
	}
}
