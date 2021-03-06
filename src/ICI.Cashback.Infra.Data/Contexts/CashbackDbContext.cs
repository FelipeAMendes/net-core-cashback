using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Infra.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace ICI.Cashback.Infra.Data.Contexts
{
	public class CashbackDbContext : DbContext
	{
		public CashbackDbContext(DbContextOptions<CashbackDbContext> options)
			: base(options)
		{
		}

		public DbSet<Log> Log { get; set; }
		public DbSet<Purchase> Purchase { get; set; }
		public DbSet<Reseller> Reseller { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new LogEntityConfiguration());
			modelBuilder.ApplyConfiguration(new PurchaseEntityConfiguration());
			modelBuilder.ApplyConfiguration(new ResellerEntityConfiguration());
		}
	}
}
