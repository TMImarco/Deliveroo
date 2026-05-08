using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Deliveroo.Models;
using Deliveroo.Tabelle;

namespace Deliveroo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;
    private readonly GestioneDati _gestioneDati;
    private readonly CloudinaryService _cloudinaryService;
    private readonly GestioneCarrello _gestioneCarrello;
    private int contatore; //variabile che non serve a niente ma esempio con contextAccessore
    private readonly IHttpContextAccessor _contextAccessor;

    public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor)
    {
        _logger = logger;
        _contextAccessor = contextAccessor;
        _gestioneDati = new GestioneDati();
        _gestioneCarrello = new GestioneCarrello(_contextAccessor.HttpContext.Session);
    }
    public IActionResult Index()
    {
        // CONTATORE SESSIONE
        int? contatore = HttpContext.Session.GetInt32("contatore");

        if (contatore == null)
            contatore = 1;
        else
            contatore++;

        HttpContext.Session.SetInt32("contatore", contatore.Value);

        // UTENTE (OPZIONALE)
        int? userId = HttpContext.Session.GetInt32("userId");

        Utente? utente = null;

        if (userId != null)
        {
            utente = _gestioneDati.GetUtente(userId.Value);
        }

        // CATEGORIE
        var listCategorie = _gestioneDati.GetTutteCategorie();

        // VIEWMODEL
        var model = new HomeViewModel
        {
            Utente = utente,
            Categorie = listCategorie
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}