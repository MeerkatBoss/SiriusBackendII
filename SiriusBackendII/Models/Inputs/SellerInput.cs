using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace SiriusBackendII.Models.Inputs
{
	public sealed class SellerInput
	{
		[Required]
		[GraphQLNonNullType]
		public string ShopName { get; init; }

		[Required]
		[Url]
		[GraphQLNonNullType]
		public string PhotoUrl { get; init; }

		public void Deconstruct(out string shopName, out string photoUrl)
		{
			shopName = ShopName;
			photoUrl = PhotoUrl;
		}
	}
}