namespace SiriusBackendII.GraphQL.Mutations
{
	public class Mutation
	{
		public SellerMutation Seller => new ();
     
		public BouquetMutation Bouquet => new ();
     
		public CustomerMutation Customer => new ();
	}
}