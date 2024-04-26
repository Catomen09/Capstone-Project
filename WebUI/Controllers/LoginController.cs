using DataAccessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebUI.Dtos.IdentityDtos;

namespace WebUI.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;


		public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Index(LoginDto loginDto)
		{
			var result = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false);
			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Admin");
			}
			else
			{
				// JavaScript alert mesajı için ViewBag kullanarak hata mesajını gönderiyoruz
				ViewBag.ErrorMessage = "Kullanici adi veya sifre yanlis.";
				return View();
			}

		}
		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Login");
		}
		[HttpGet]
		public async Task<IActionResult> ForgotPassword(string username)
		{
			if (username == null)
				return RedirectToAction("Index", "Login");
			var user = await _userManager.FindByNameAsync(username);
			if (user == null)
				return RedirectToAction("Index", "Login");

			return View(user);
		}
		[HttpPost]
		public async Task<IActionResult> ForgotPassword(AppUser appUser)
		{
			var user = await _userManager.FindByNameAsync(appUser.UserName);
			if (user == null)
			{
				return NotFound();
			}
			user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, appUser.PasswordHash);
			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
				return RedirectToAction("Index", "Login");
			else
				return View();

		}
	}
}

