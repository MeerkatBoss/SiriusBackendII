using System.ComponentModel.DataAnnotations;
using HotChocolate;
using HotChocolate.Types;

namespace SiriusBackendII.Models.Inputs
{
	public sealed class BouquetInput
	{
		[Required]
		[GraphQLNonNullType]
		public string Name { get; init; }

		[Required]
		[GraphQLNonNullType]
		public string PhotoUrl { get; init; }

		[Required]
		[GraphQLNonNullType]
		public double Cost { get; init; }

		[Required]
		[GraphQLType(typeof(IdType))]
		[GraphQLNonNullType]
		public int SellerId { get; init; }

		public void Deconstruct(out string name, out string photoUrl, out double cost, out int sellerId)
		{
			name = Name;
			photoUrl = PhotoUrl;
			cost = Cost;
			sellerId = SellerId;
		}
	}
}