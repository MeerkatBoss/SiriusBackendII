using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiriusBackendII.Models;

namespace SiriusBackendII.Services
{
	public sealed class CustomerService : DbAccessService
	{
		public CustomerService(AppDbContext context) : base(context)
		{
		}
		
		public async Task<Customer> GetCustomer(int id)
		{
			var customer = await Database.Customers
				.Where(c => c.Id == id)
				.FirstOrDefaultAsync();
			if (customer is null)
				throw new ArgumentException(GetItemNotFoundMessage<Customer>(id));
			return customer;
		}

		public async Task<Customer> AddCustomer(string name, string email)
		{
			var customer = new Customer
			{
				Name = name,
				Email = email,
				Purchases = new List<Purchase>()
			};
			await Database.Customers
				.AddAsync(customer);
			await Database.SaveChangesAsync();
			return customer;
		}

		public async Task<Customer> UpdateCustomer(int id,
													string name = null,
													string email = null)
		{
			var customer = await Database.Customers
				.Where(c => c.Id == id)
				.FirstOrDefaultAsync();
			if (customer is null)
				throw new ArgumentException(GetItemNotFoundMessage<Customer>(id));
			customer.Name = name ?? customer.Name;
			customer.Email = email ?? customer.Email;
			await Database.SaveChangesAsync();
			return customer;
		}

		public async Task<Customer> RemoveCustomer(int id)
		{
			var customer = await Database.Customers
				.Where(c => c.Id == id)
				.FirstOrDefaultAsync();
			if (customer is null)
				throw new ArgumentException(GetItemNotFoundMessage<Customer>(id));
			Database.Customers
				.Remove(customer);
			await Database.SaveChangesAsync();
			return customer;
		}
	}
}