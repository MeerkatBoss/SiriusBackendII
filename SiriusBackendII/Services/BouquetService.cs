using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiriusBackendII.Models;

namespace SiriusBackendII.Services
{
	public sealed class BouquetService : DbAccessService
	{
		public BouquetService(AppDbContext context) : base(context)
		{
		}
		
		public IQueryable<Bouquet> GetAllBouquets()
		{
			return Database.Bouquets
				.Include(b => b.Seller)
				.AsQueryable();
		}
		
		public async Task<Bouquet> GetBouquet(int id)
		{
			var bouquet =  await Database.Bouquets
				.Include(b => b.Seller)
				.Where(b => b.Id == id)
				.FirstOrDefaultAsync();
			if (bouquet is null)
				throw new ArgumentException(GetItemNotFoundMessage<Bouquet>(id));
			return bouquet;
		}

		public async Task<Bouquet> AddBouquet(string name, string photoUrl, double cost, int sellerId)
		{
			var seller = await Database.Sellers
				.Where(s => s.Id == sellerId)
				.FirstOrDefaultAsync();
			if (seller is null)
				throw new ArgumentException(GetItemNotFoundMessage<Seller>(sellerId));
			var bouquet = new Bouquet
			{
				Name = name,
				PhotoUrl = photoUrl,
				Cost = cost,
				Seller = seller
			};
			await Database.Bouquets.AddAsync(bouquet);
			await Database.SaveChangesAsync();
			return bouquet;

		}

		public async Task<Bouquet> UpdateBouquet(int id,
														string name = null,
														string photoUrl = null,
														double? cost = null)
		{
			var bouquet = await Database.Bouquets
				.Where(b => b.Id == id)
				.FirstOrDefaultAsync();
			if (bouquet is null)
				throw new ArgumentException(GetItemNotFoundMessage<Bouquet>(id));
			bouquet.Name = name ?? bouquet.Name;
			bouquet.PhotoUrl = photoUrl ?? bouquet.PhotoUrl;
			bouquet.Cost = cost ?? bouquet.Cost;
			await Database.SaveChangesAsync();
			return bouquet;
		}

		public async Task<Bouquet> RemoveBouquet(int id)
		{
			var bouquet = await Database.Bouquets
				.Where(b => b.Id == id)
				.FirstOrDefaultAsync();
			if (bouquet is null)
				throw new ArgumentException(GetItemNotFoundMessage<Bouquet>(id));
			Database.Bouquets.Remove(bouquet);
			await Database.SaveChangesAsync();
			return bouquet;
		}
	}
}