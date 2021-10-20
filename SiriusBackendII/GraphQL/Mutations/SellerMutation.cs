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
	public sealed class SellerMutation
	{
		public async Task<Payload<Seller>> Create([Service] SellerService service,
													[GraphQLNonNullType] SellerInput seller)
		{
			try
			{
				var (shopName, photoUrl) = seller;
				var addedSeller = await service.AddSeller(shopName, photoUrl);
				return GetPayload(addedSeller);
			}
			catch (ArgumentException e)
			{
				throw new GraphQLRequestException(e.Message);
			}
		}

		public async Task<Payload<Seller>> Update([Service] SellerService service,
													[GraphQLType(typeof(IdType))] int id,
													string shopName = null,
													string photoUrl = null)
		{
			try
			{
				var updatedSeller = await service.UpdateSeller(id, shopName, photoUrl);
				return GetPayload(updatedSeller);
			}
			catch (ArgumentException e)
			{
				throw new GraphQLRequestException(e.Message);
			}
		}

		public async Task<Payload<Seller>> Delete([Service] SellerService service, 
													[GraphQLType(typeof(IdType))] int id)
		{
			try
			{
				var deletedSeller = await service.RemoveSeller(id);
				return GetPayload(deletedSeller);
			}
			catch (ArgumentException e)
			{
				throw new GraphQLRequestException(e.Message);
			}
		}

		private static Payload<Seller> GetPayload(Seller seller) => new (seller.Id, seller);
	}
}