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

	public IActionResult LoginAdmin()
	{
		return View();
	}
	
	/* ---------------------- LOGIN ADMIN ---------------------- */
	[HttpPost]
	public IActionResult LoginAdmin(UtenteAdmin utenteAdmin)
	{
		string username = _configuration.GetRequiredSection("AdminCredentials").GetValue<string>("UserName");
		string password = _configuration.GetRequiredSection("AdminCredentials").GetValue<string>("Password");

		if (utenteAdmin.Username == username && utenteAdmin.Password == password)
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
	
	/* ---------------------- LOGIN UTENTE ---------------------- */
	public IActionResult LoginUtente(string username, string password)
	{
		if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
		{
			ViewData["errore"] = "Credenziali non valide";
			return View();
		}
		else
		{
			int idUtente = _gestioneDati.GetLoginUtente(username, password);
			if (idUtente != 0)
			{
				HttpContext.Session.SetInt32("userId", idUtente);
				HttpContext.Session.SetString("role", "user");
				
				return RedirectToAction("Index", "Home");
			}
			
			ViewData["errore"] = "Credenziali non valide";
			return View();
		}
	}
}