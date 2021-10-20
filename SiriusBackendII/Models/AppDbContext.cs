using Microsoft.EntityFrameworkCore;

namespace SiriusBackendII.Models
{
	public sealed class AppDbContext : DbContext
	{
		public DbSet<Bouquet> Bouquets { get; set; }
		public DbSet<Seller> Sellers { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Purchase> Purchases { get; set; }

		public AppDbContext(DbContextOptions options)
			: base(options)
		{
			// Bouquets = Set<Bouquet>();
			// Sellers = Set<Seller>();
			// Customers = Set<Customer>();
			// Purchases = Set<Purchase>();
			Database.EnsureCreated();
		}
	}
}