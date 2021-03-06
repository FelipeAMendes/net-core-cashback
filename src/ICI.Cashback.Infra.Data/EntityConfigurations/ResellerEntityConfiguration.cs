using ICI.Cashback.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ICI.Cashback.Infra.Data.EntityConfigurations
{
	public class ResellerEntityConfiguration : IEntityTypeConfiguration<Reseller>
	{
		public void Configure(EntityTypeBuilder<Reseller> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
			builder.Property(p => p.Cpf).HasMaxLength(11).IsRequired();
			builder.Property(p => p.Email).HasMaxLength(100).IsRequired();
			builder.Property(p => p.Password).HasMaxLength(64).IsRequired();
			builder.Property(p => p.Role).HasMaxLength(50).IsRequired();
			builder.Property(p => p.Enabled).IsRequired().HasDefaultValue(true);
		}
	}
}
