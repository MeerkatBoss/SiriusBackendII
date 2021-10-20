using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotChocolate;
using HotChocolate.Types;

namespace SiriusBackendII.Models
{
	public sealed class Customer
	{
		[GraphQLType(typeof(IdType))]
		[GraphQLNonNullType]
		public int Id { get; set; }

		public string Name { get; set; }
		
		public string Email { get; set; }

		public ICollection<Purchase> Purchases { get; set; }
	}
}