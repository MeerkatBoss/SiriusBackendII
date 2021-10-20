using SiriusBackendII.Models;

namespace SiriusBackendII.Services
{
	public abstract class DbAccessService
	{
		protected readonly AppDbContext Database;
		
		protected DbAccessService(AppDbContext context)
		{
			Database = context;
		}

		protected static string GetItemNotFoundMessage<TItem>(int id) =>
			$"Could not find {typeof(TItem).Name} with an Id: {id}";
	}
}