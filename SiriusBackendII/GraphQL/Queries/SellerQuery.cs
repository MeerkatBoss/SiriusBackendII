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
	public sealed class SellerQuery
	{
		[UseSorting]
		[UseFiltering]
		public IQueryable<Seller> GetSellers([Service] SellerService service)
		{
			try
			{
				return service.GetAllSellers();

			}
			catch (ArgumentException e)
			{
				throw new GraphQLRequestException(e.Message);
			}
		}
		
		public async Task<Seller> GetSellerById([Service] SellerService service, int id)
		{
			try
			{
				return await service.GetSeller(id);
			}
			catch (ArgumentException e)
			{
				throw new GraphQLRequestException(e.Message);
			}
		}
	}
}