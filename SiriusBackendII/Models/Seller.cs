using System;
using System.Collections.Generic;
using HotChocolate;
using HotChocolate.Types;

namespace SiriusBackendII.Models
{
	public sealed class Seller
	{
		[GraphQLType(typeof(IdType))]
		[GraphQLNonNullType]
		public int Id { get; set; }

		public string ShopName { get; set; }

		public string PhotoUrl { get; set; }

		public DateTime Date { get; set; }

		public ICollection<Bouquet> Bouquets { get; set; }

		public int Sold { get; set; }
	}
}