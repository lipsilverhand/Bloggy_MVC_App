using System.ComponentModel.DataAnnotations;

namespace Bloggy_MVC.Models.ViewModels
{
	public class LoginViewModel
	{
		[Required]
		public string Username { get; set; }


		[Required]
		[MinLength(8, ErrorMessage = "Password has to be at least 8 characters")]
		public string Password { get; set; }

		public string? ReturnUrl { get; set; }

	}
}
