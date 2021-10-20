using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiriusBackendII.Models;

namespace SiriusBackendII.Services
{
	public sealed class PurchaseService : DbAccessService
	{
		public PurchaseService(AppDbContext context) : base(context)
		{
		}
		
		public async Task<IQueryable<Purchase>> GetCustomersPurchases(int id)
		{
			var customer = await Database.Customers
				.Include(c => c.Purchases)
				.ThenInclude(p => p.Bouquet)
				.ThenInclude(b => b.Seller)
				.Where(c => c.Id == id)
				.FirstOrDefaultAsync();
			if (customer is null)
				throw new ArgumentException(GetItemNotFoundMessage<Customer>(id));
			return customer.Purchases.AsQueryable();
		}

		public async Task<Purchase> MakePurchase(int bouquetId, int customerId)
		{
			var customer = await Database.Customers
				.Where(c => c.Id == customerId)
				.FirstOrDefaultAsync();
			var bouquet = await Database.Bouquets
				.Include(b => b.Seller)
				.Where(b => b.Id == bouquetId)
				.FirstOrDefaultAsync();
			if (customer is null)
				throw new ArgumentException(GetItemNotFoundMessage<Customer>(customerId));
			if (bouquet is null)
				throw new ArgumentException(GetItemNotFoundMessage<BouquetService>(bouquetId));
			var purchase = new Purchase
			{
				Bouquet = bouquet,
				Customer = customer,
				Cost = bouquet.Cost,
				Income = GetIncome(bouquet.Cost)
			};
			await Database.Purchases.AddAsync(purchase);
			bouquet.Seller.Sold++;
			await Database.SaveChangesAsync();
			return purchase;
		}
		
		private static double GetIncome(double cost) => Math.Round(cost * 0.3, 2);
	}
}