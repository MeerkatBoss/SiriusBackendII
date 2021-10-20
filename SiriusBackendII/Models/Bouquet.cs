using System;
using HotChocolate;
using HotChocolate.Types;

namespace SiriusBackendII.Models
{
	public sealed class Bouquet
	{
		[GraphQLType(typeof(IdType))]
		[GraphQLNonNullType]
		public int Id { get; set; }
		
		public string Name { get; set; }
		
		public double Cost { get; set; }

		public string PhotoUrl { get; set; }

		public Seller Seller { get; set; }
	}
}