using System;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Types;
using SiriusBackendII.Models;
using SiriusBackendII.Services;

namespace SiriusBackendII.GraphQL.Queries
{
	[ExtendObjectType(typeof(Query))]
	public sealed class CustomerQuery
	{
		public async Task<Customer> GetCustomerById([Service] CustomerService service, int id)
		{
			try
			{
				return await service.GetCustomer(id);
			}
			catch (ArgumentException e)
			{
				throw new GraphQLRequestException(e.Message);
			}
		}
	}
}