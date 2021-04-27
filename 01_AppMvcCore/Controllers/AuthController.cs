using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using _01_AppMvcCore.Models;
using _01_AppMvcCore.Services;
using Microsoft.AspNetCore.Http;
using _01_AppMvcCore.Mapper;

namespace _01_AppMvcCore.Controllers
{
	public class AuthController : Controller
	{
		IAuthService _authService;
		
		#region Constructor
		
		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		#endregion

		#region Register Methods
		
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Register(User user)
		{
			try
			{
				_authService.Register(user);
				return RedirectToAction("Login");
			}
			catch (Exception)
			{
				return View();
			}
		}

		#endregion

		#region Login Methods
		
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Login(string email, string password)
		{
			User user = _authService.Login(email, password);

			if (user != null)
			{
				HttpContext.Session.Set<User>("CurrentUser", user);
				HttpContext.Session.Set<bool>("IsLog", true);
			}

			return RedirectToAction("Index", "Home", user);
		}

		#endregion
	}
}