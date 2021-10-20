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
	public sealed class BouquetMutation
	{
		public async Task<Payload<Bouquet>> Create([Service] BouquetService service, 
														[GraphQLNonNullType] BouquetInput bouquet)
		{
			try
			{
				var (name, photoUrl, cost, sellerId) = bouquet;
				var addedBouquet = await service.AddBouquet(name, photoUrl, cost, sellerId);
				return GetPayload(addedBouquet);
			}
			catch (ArgumentException e)
			{
				throw new GraphQLRequestException(e.Message);
			}
		}

		public async Task<Payload<Bouquet>> Update([Service] BouquetService service,
													[GraphQLType(typeof(IdType))] int id,
													string name = null,
													string photoUrl = null,
													double? cost = null)
		{
			try
			{
				var updatedBouquet = await service.UpdateBouquet(id, name, photoUrl, cost);
				return GetPayload(updatedBouquet);
			}
			catch (ArgumentException e)
			{
				throw new GraphQLRequestException(e.Message);
			}
		}

		public async Task<Payload<Bouquet>> Delete([Service] BouquetService service,
													[GraphQLType(typeof(IdType))] int id)
		{
			try
			{
				var deletedBouquet = await service.RemoveBouquet(id);
				return GetPayload(deletedBouquet);
			}
			catch (ArgumentException e)
			{
				throw new GraphQLRequestException(e.Message);
			}
		}

		private static Payload<Bouquet> GetPayload(Bouquet bouquet) => new(bouquet.Id, bouquet);
	}
}