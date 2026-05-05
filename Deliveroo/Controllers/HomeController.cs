using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Deliveroo.Models;

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
        /* CREO LA SESSIONE DELL'UTENTE */
        int? contatore = _contextAccessor.HttpContext.Session.GetInt32("contatore");
        if (contatore == null)
        {
            contatore = 1;
        }
        else
        {
            contatore++;
        }

        _contextAccessor.HttpContext.Session.SetInt32("contatore", (int)contatore);

        // Visualizzo le categorie nell'Index
        var listCategorie = _gestioneDati.GetTutteCategorie();
        return View(listCategorie);
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