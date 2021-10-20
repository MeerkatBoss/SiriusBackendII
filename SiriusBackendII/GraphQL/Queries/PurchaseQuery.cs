using System;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Data;
using HotChocolate.Types;
using SiriusBackendII.Models;
using SiriusBackendII.Services;

namespace SiriusBackendII.GraphQL.Queries
{
	[ExtendObjectType(typeof(Query))]
	public sealed class PurchaseQuery
	{
		[UseSorting]
		[UseFiltering]
		public async Task<IQueryable<Purchase>> GetPurchases([Service] PurchaseService service, int customerId)
		{
			try
			{
				return await service.GetCustomersPurchases(customerId);
			}
			catch (ArgumentException e)
			{
				throw new GraphQLRequestException(e.Message);
			}
		}
	}
}