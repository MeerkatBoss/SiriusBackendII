using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiriusBackendII.Models;

namespace SiriusBackendII.Services
{
	public sealed class SellerService : DbAccessService
	{
		public SellerService(AppDbContext context) : base(context) { }
		
		public IQueryable<Seller> GetAllSellers()
		{
			return Database.Sellers
				.Include(s => s.Bouquets)
				.AsQueryable();
		}

		public async Task<Seller> GetSeller(int id)
		{
			var seller =  await Database.Sellers
				.Include(s => s.Bouquets)
				.Where(s => s.Id == id)
				.FirstOrDefaultAsync();
			if (seller is null)
				throw new ArgumentException(GetItemNotFoundMessage<Seller>(id));
			return seller;
		}
		
		public async Task<Seller> AddSeller(string shopName, string photoUrl)
		{
			var seller = new Seller
			{
				Date = DateTime.Today,
				ShopName = shopName,
				PhotoUrl = photoUrl,
				Bouquets = new List<Bouquet>(),
				Sold = 0
			};
			await Database.Sellers.AddAsync(seller);
			await Database.SaveChangesAsync();
			return seller;
		}

		public async Task<Seller> UpdateSeller(int id,
												string shopName = null,
												string photoUrl = null)
		{
			var seller = await Database.Sellers.FirstOrDefaultAsync(s => s.Id == id);
			if (seller is null)
				throw new ArgumentException(GetItemNotFoundMessage<Seller>(id));
			seller.ShopName = shopName ?? seller.ShopName;
			seller.PhotoUrl = photoUrl ?? seller.PhotoUrl;
			await Database.SaveChangesAsync();
			return seller;
		}

		public async Task<Seller> RemoveSeller(int id)
		{
			var seller = await Database.Sellers
				.Where(s => s.Id == id)
				.Include(s => s.Bouquets)
				.FirstOrDefaultAsync();
			if (seller is null)
				throw new ArgumentException(GetItemNotFoundMessage<Seller>(id));
			foreach (var bouquet in seller.Bouquets)
			{
				Database.Bouquets.Remove(bouquet);
			}
			Database.Sellers.Remove(seller);
			await Database.SaveChangesAsync();
			return seller;
		}
	}
}