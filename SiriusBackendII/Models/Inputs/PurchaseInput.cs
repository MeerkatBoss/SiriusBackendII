using System.ComponentModel.DataAnnotations;
using HotChocolate;
using HotChocolate.Types;

namespace SiriusBackendII.Models.Inputs
{
	public sealed class PurchaseInput
	{
		[Required]
		[GraphQLType(typeof(IdType))]
		[GraphQLNonNullType]
		public int BouquetId { get; init; }

		[Required]
		[GraphQLType(typeof(IdType))]
		[GraphQLNonNullType]
		public int CustomerId { get; init; }

		public void Deconstruct(out int bouquetId, out int customerId)
		{
			bouquetId = BouquetId;
			customerId = CustomerId;
		}
	}
}