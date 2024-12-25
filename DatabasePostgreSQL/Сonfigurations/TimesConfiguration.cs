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
	public class TimesConfiguration : IEntityTypeConfiguration<TimeCharacteristic>
	{
		public void Configure(EntityTypeBuilder<TimeCharacteristic> builder)
		{
			// Указывается ключ PK
			builder.HasKey(tc => tc.Id);

			// Указывается связь О-М между ВХ и РР
			builder
				.HasMany(tc => tc.Results)
				.WithOne(cr => cr.TimeCharacteristic);

			// Указывается связь О-М между ЗН и ВХ
			builder
				.HasOne(tc => tc.ReliabilityZone)
				.WithMany(rz => rz.TimeCharacteristics)
				.HasForeignKey(tc => tc.ReliabilityZoneId);
		}
	}
}
