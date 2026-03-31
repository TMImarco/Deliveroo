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
    private readonly IHttpContextAccessor _contextAccessor;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IHttpContextAccessor contextAccessor)
    {
        _logger = logger;
        _configuration = configuration;
        _gestioneDati = new GestioneDati();
        _contextAccessor = contextAccessor;
    }

    //  ----------------------------------PER AUTENTICAZIONE-LOGIN-------------------------------------------
    public IActionResult LoginAdmin()
    {
        return View();
    }

    [HttpPost]
    public IActionResult LoginAdmin(Utente utente)
    {
        string username=_configuration.GetRequiredSection("AdminCredentials").GetValue<string>("UserName");
        string password=_configuration.GetRequiredSection("AdminCredentials").GetValue<string>("Password");
        
        if (utente.Username==username && utente.Password==password)
        {
            //ok è chi doveva essere
            //scrivo nella sessione l'utente loggato
            _contextAccessor.HttpContext.Session.SetString("user", "admin");
            return RedirectToAction("Index");
        }
        else
        {
            //se le credenziali non sono valide
            //ritorniamo al form di login
            ViewData["errore"] = "Credenziali non valide";
            return View();
        }
    }
    
    //-----------------------------------------PAGINA LOGOUT------------------------------------------------
    public IActionResult Logout()
    {
        _contextAccessor.HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
    
    public IActionResult Index()
    {
        return View();
    }

    //--------------------------------------SOLO PER ADMIN-SOLO AUTORIZZATI--------------------------------------------
    
    //-----------------------------------------------------------------------------------------------------------------

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