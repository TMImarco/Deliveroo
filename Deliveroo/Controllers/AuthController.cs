using Microsoft.AspNetCore.Mvc;
using Deliveroo.Tabelle;

namespace Deliveroo.Controllers;

public class AuthController : Controller
{
	private readonly IConfiguration _configuration;
	private readonly IHttpContextAccessor _contextAccessor;

	public AuthController(IConfiguration configuration, IHttpContextAccessor contextAccessor)
	{
		_configuration = configuration;
		_contextAccessor = contextAccessor;
	}

	public IActionResult LoginAdmin()
	{
		return View();
	}

	[HttpPost]
	public IActionResult LoginAdmin(Utente utente)
	{
		string username = _configuration.GetRequiredSection("AdminCredentials").GetValue<string>("UserName");
		string password = _configuration.GetRequiredSection("AdminCredentials").GetValue<string>("Password");

		if (utente.Username == username && utente.Password == password)
		{
			_contextAccessor.HttpContext.Session.SetString("user", "admin");
			return RedirectToAction("IndexAdmin", "Admin");
		}
		else
		{
			ViewData["errore"] = "Credenziali non valide";
			return View();
		}
	}

	public IActionResult Logout()
	{
		_contextAccessor.HttpContext.Session.Clear();
		return RedirectToAction("Index", "Home");
	}
}