using EntityLayer.Entities;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using WebUI.Dtos.IdentityDtos;
using WebUI.Dtos.MailDtos;

namespace WebUI.Controllers
{
	[AllowAnonymous]
	public class RegisterController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

		public RegisterController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Index(RegisterDto registerDto)
		{
			var appUser = new AppUser()
			{
				Name = registerDto.Name,
				Surname = registerDto.Surname,
				Email = registerDto.Mail,
				UserName = registerDto.Username,

			};
			var result = await _userManager.CreateAsync(appUser, registerDto.Password);
			if (result.Succeeded)
			{
				MimeMessage mimeMessage = new MimeMessage();
				MailboxAddress mailboxAddressFrom = new MailboxAddress("Capstone Project Admin", "catobabbatuzcu@gmail.com");
				mimeMessage.From.Add(mailboxAddressFrom);
				MailboxAddress mailboxAddressTo = new MailboxAddress("User", registerDto.Mail);
				mimeMessage.To.Add(mailboxAddressTo);
				var bodyBuilder = new BodyBuilder();
				bodyBuilder.TextBody = "Welcome to Capstone Admin Panel. Enjoy your work";
				mimeMessage.Body = bodyBuilder.ToMessageBody();
				mimeMessage.Subject = "Login Process Successful";
				SmtpClient client = new SmtpClient();
				client.Connect("smtp.gmail.com", 587, false);
				client.Authenticate("catobabbatuzcu@gmail.com", "lvff wwat asey tgpb");
				client.Send(mimeMessage);
				client.Disconnect(true);
				return RedirectToAction("Index", "Login");
			}
			return View();
		}
	}
}
