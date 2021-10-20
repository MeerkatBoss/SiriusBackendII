using HotChocolate;
using HotChocolate.Types;
using SiriusBackendII.GraphQL.Queries;

namespace SiriusBackendII.Models.Payloads
{
	public class Payload<TModel> where TModel: class
	{
		public Payload(int id,
						TModel result)
		{
			Id = id;
			Result = result;
		}

		[GraphQLType(typeof(IdType))]
		[GraphQLNonNullType]
		public int Id { get; init; }
		
		public TModel Result { get; init; }

		public Query Query => new ();
	}
}