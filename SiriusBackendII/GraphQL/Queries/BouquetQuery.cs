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
	public sealed class BouquetQuery
	{
		[UseSorting]
		[UseFiltering]
		public IQueryable<Bouquet> GetBouquets([Service] BouquetService service)
		{
			try
			{
				return service.GetAllBouquets();
			}
			catch (ArgumentException e)
			{
				throw new GraphQLRequestException(e.Message);
			}
		}
		
		public async Task<Bouquet> GetBouquetById([Service] BouquetService service, int id)
		{
			try
			{
				return await service.GetBouquet(id);
			}
			catch (ArgumentException e)
			{
				throw new GraphQLRequestException(e.Message);
			}
		}
	}
}