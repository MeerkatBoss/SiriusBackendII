using HotChocolate;
using HotChocolate.Types;

namespace SiriusBackendII.Models
{
	public sealed class Purchase
	{
		[GraphQLType(typeof(IdType))]
		[GraphQLNonNullType]
		public int Id { get; set; }

		public Bouquet Bouquet { get; set; }

		public Customer Customer { get; set; }

		public double Cost { get; set; }

		public double Income { get; set; }
	}
}