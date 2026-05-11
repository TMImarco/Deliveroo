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
    public IActionResult Login(string? returnUrl)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model, string? returnUrl)
    {
        if (string.IsNullOrWhiteSpace(model.Username) ||
            string.IsNullOrWhiteSpace(model.Password))
        {
            ViewData["errore"] = "Credenziali non valide";
            ViewBag.ReturnUrl = returnUrl;
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
        int idUtente = _gestioneDati.GetLoginUtente(model.Username, model.Password);

        if (idUtente != 0)
        {
            HttpContext.Session.SetInt32("userId", idUtente);
            HttpContext.Session.SetString("role", "user");

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        ViewData["errore"] = "Credenziali non valide";
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpGet]
    public IActionResult Registrazione(string? returnUrl)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public IActionResult Registrazione(RegistrazioneViewModel model, string? returnUrl)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        bool usernameEsiste = _gestioneDati.UsernameEsiste(model.Username);
        if (usernameEsiste)
        {
            ViewData["errore"] = "Username già in uso";
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        int idUtente = _gestioneDati.RegistraUtente(model);

        HttpContext.Session.SetInt32("userId", idUtente);
        HttpContext.Session.SetString("role", "user");

        if (!string.IsNullOrEmpty(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        _contextAccessor.HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
    
    // GET /Auth/Profilo
[HttpGet]
public IActionResult Profilo()
{
    var idStr = HttpContext.Session.GetString("userId");
    if (string.IsNullOrEmpty(idStr))
        return RedirectToAction("Login");

    int? id = HttpContext.Session.GetInt32("userId");
    var utente = _gestioneDati.GetUtente(id);
    if (utente == null)
        return RedirectToAction("Login");

    return View(utente);
}

// POST /Auth/AggiornaProfilo
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult AggiornaProfilo(string Nome, string Indirizzo, string Telefono, string Username)
{
    var idStr = HttpContext.Session.GetString("userId");
    if (string.IsNullOrEmpty(idStr))
        return RedirectToAction("Login");

    int? id = HttpContext.Session.GetInt32("userId");

    // controlla username duplicato (escludendo l'utente corrente)
    if (_gestioneDati.ModificaUsernameEsiste(Username, id))
    {
        TempData["Errore"] = "Username già in uso, scegline un altro.";
        return RedirectToAction("Profilo");
    }

    _gestioneDati.ModificaUtente(id, Nome.Trim(), Indirizzo.Trim(), Telefono.Trim(), Username.Trim());
    HttpContext.Session.SetString("UserName", Username.Trim()); // aggiorna sessione
    TempData["Successo"] = "Profilo aggiornato con successo.";
    return RedirectToAction("Profilo");
}

// POST /Auth/CambiaPassword
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CambiaPassword(string PasswordAttuale, string NuovaPassword, string ConfermaPassword)
    {
        var idStr = HttpContext.Session.GetString("userId");
        if (string.IsNullOrEmpty(idStr))
            return RedirectToAction("Login");

        int? id = HttpContext.Session.GetInt32("userId");

        if (NuovaPassword != ConfermaPassword)
        {
            TempData["Errore"] = "Le password non coincidono.";
            return RedirectToAction("Profilo");
        }

        // verifica password attuale direttamente nel DB
        bool corretta = _gestioneDati.PasswordAttualeCorretta(id, PasswordAttuale);
        if (!corretta)
        {
            TempData["Errore"] = "Password attuale non corretta.";
            return RedirectToAction("Profilo");
        }

        _gestioneDati.ModificaPassword(id, NuovaPassword);
        TempData["Successo"] = "Password aggiornata con successo.";
        return RedirectToAction("Profilo");
    }
}