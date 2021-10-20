using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace SiriusBackendII.Models.Inputs
{
	public sealed class CustomerInput
	{
		[Required]
		[GraphQLNonNullType]
		public string Name { get; init; }

		[Required]
		[EmailAddress]
		[GraphQLNonNullType]
		public string Email { get; init; }

		public void Deconstruct(out string name, out string email)
		{
			name = Name;
			email = Email;
		}
	}
}