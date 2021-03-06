using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ICI.Cashback.Infra.Data.EntityConfigurations
{
	public class PurchaseEntityConfiguration : IEntityTypeConfiguration<Purchase>
	{
		public void Configure(EntityTypeBuilder<Purchase> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Code).IsRequired().HasMaxLength(30);
			builder.Property(p => p.Value).IsRequired();
			builder.Property(p => p.Date).IsRequired().HasDefaultValueSql("GETDATE()");
			builder.Property(p => p.ResellerId).IsRequired();
			builder.Property(p => p.Status).IsRequired().HasDefaultValue(PurchaseStatus.InValidation);
			builder.HasOne(p => p.Reseller).WithMany(r => r.Purchases);
		}
	}
}
