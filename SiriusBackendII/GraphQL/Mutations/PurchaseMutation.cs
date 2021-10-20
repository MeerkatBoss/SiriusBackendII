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
	[ExtendObjectType(typeof(Mutation))]
	public sealed class PurchaseMutation
	{
		public async Task<Payload<Purchase>> PurchaseBouquet([Service] PurchaseService service,
															[GraphQLNonNullType] PurchaseInput purchase)
		{
			try
			{
				var (bouquetId, customerId) = purchase;
				var purchaseMade = await service.MakePurchase(bouquetId, customerId);
				return GetPayload(purchaseMade);
			}
			catch (ArgumentException e)
			{
				throw new GraphQLRequestException(e.Message);
			}
		}

		private static Payload<Purchase> GetPayload(Purchase purchase) => new(purchase.Id, purchase);
	}
}