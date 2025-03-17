using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bloggy_MVC.Models.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		public string Username { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MinLength(8, ErrorMessage = "Password has to be at least 8 characters")]
		[RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^A-Za-z\d]).{8,}$",
		ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
		public string Password { get; set; }

	}
}
