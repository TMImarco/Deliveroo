using Deliveroo.Models;
using Microsoft.AspNetCore.Mvc;
using Deliveroo.Tabelle;

namespace Deliveroo.Controllers;

public class AuthController : Controller
{
	private readonly IConfiguration _configuration;
	private readonly IHttpContextAccessor _contextAccessor;
	private GestioneDati _gestioneDati;

	public AuthController(IConfiguration configuration, IHttpContextAccessor contextAccessor)
	{
		_configuration = configuration;
		_contextAccessor = contextAccessor;
		_gestioneDati = new GestioneDati();
	}
	
	[HttpGet]
	public IActionResult Login()
	{
		return View();
	}

	[HttpPost]
	public IActionResult Login(LoginViewModel model)
	{
		if (string.IsNullOrWhiteSpace(model.Username) ||
		    string.IsNullOrWhiteSpace(model.Password))
		{
			ViewData["errore"] = "Credenziali non valide";
			return View();
		}

		/* ADMIN */

		string adminUsername = _configuration
			.GetRequiredSection("AdminCredentials")
			.GetValue<string>("UserName");

		string adminPassword = _configuration
			.GetRequiredSection("AdminCredentials")
			.GetValue<string>("Password");

		if (model.Username == adminUsername &&
		    model.Password == adminPassword)
		{
			HttpContext.Session.SetString("role", "admin");

			return RedirectToAction("IndexAdmin", "Admin");
		}

		/* UTENTE */

		int idUtente = _gestioneDati
			.GetLoginUtente(model.Username, model.Password);

		if (idUtente != 0)
		{
			HttpContext.Session.SetInt32("userId", idUtente);
			HttpContext.Session.SetString("role", "user");

			return RedirectToAction("Index", "Home");
		}

		ViewData["errore"] = "Credenziali non valide";
		return View();
	}
	
	public IActionResult Logout()
	{
		_contextAccessor.HttpContext.Session.Clear();
		return RedirectToAction("Index", "Home");
	}
}