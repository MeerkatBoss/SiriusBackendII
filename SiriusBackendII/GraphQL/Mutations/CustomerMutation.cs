using System;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Types;
using SiriusBackendII.Models;
using SiriusBackendII.Models.Inputs;
using SiriusBackendII.Models.Payloads;
using SiriusBackendII.Services;

namespace SiriusBackendII.GraphQL.Mutations
{
	public sealed class CustomerMutation
	{
		public async Task<Payload<Customer>> Create([Service] CustomerService service,
													[GraphQLNonNullType] CustomerInput customer)
		{
			try
			{
				var (name, email) = customer;
				var customerAdded = await service.AddCustomer(name, email);
				return GetPayload(customerAdded);
			}
			catch (ArgumentException e)
			{
				throw new GraphQLRequestException(e.Message);
			}
		}

		public async Task<Payload<Customer>> Update([Service] CustomerService service,
													[GraphQLType(typeof(IdType))] int id,
													string name = null,
													string email = null)
		{
			try
			{
				var customerUpdated = await service.UpdateCustomer(id, name, email);
				return GetPayload(customerUpdated);
			}
			catch (ArgumentException e)
			{
				throw new GraphQLRequestException(e.Message);
			}
		}

		public async Task<Payload<Customer>> Delete([Service] CustomerService service,
													[GraphQLType(typeof(IdType))] int id)
		{
			try
			{
				var customerDeleted = await service.RemoveCustomer(id);
				return GetPayload(customerDeleted);
			}
			catch (ArgumentException e)
			{
				throw new GraphQLRequestException(e.Message);
			}
		}

		private static Payload<Customer> GetPayload(Customer customer) => new(customer.Id, customer);
	}
}