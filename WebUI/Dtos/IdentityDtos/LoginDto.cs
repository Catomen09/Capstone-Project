using System.ComponentModel.DataAnnotations;

namespace WebUI.Dtos.IdentityDtos
{
	public class LoginDto
	{
		[Required(ErrorMessage = "Username is required")]
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
