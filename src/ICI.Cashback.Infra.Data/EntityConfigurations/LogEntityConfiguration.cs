using ICI.Cashback.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ICI.Cashback.Infra.Data.EntityConfigurations
{
	public class LogEntityConfiguration : IEntityTypeConfiguration<Log>
	{
		public void Configure(EntityTypeBuilder<Log> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(p => p.UserIp).IsRequired().HasMaxLength(12);
			builder.Property(p => p.Date).IsRequired().HasDefaultValueSql("GETDATE()");
			builder.Property(p => p.Object).HasMaxLength(1000);
			builder.Property(p => p.OperationId).IsRequired();
			builder.Property(p => p.User).HasMaxLength(50);
			builder.Property(p => p.Table).HasMaxLength(50);
			builder.Property(p => p.Platform).IsRequired();
		}
	}
}
