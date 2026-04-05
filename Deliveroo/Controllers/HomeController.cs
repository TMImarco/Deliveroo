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
        _configuration = configuration; //NON RIMUOVERE (file di configurazione)
        _gestioneDati = new GestioneDati();
        _contextAccessor = contextAccessor;
    }

    //  ----------------------------------PER AUTENTICAZIONE-LOGIN-------------------------------------------
    //pagina per reindirizzare alla pagina di login per l'admin
    public IActionResult LoginAdmin()
    {
        return View();
    }

    //richiesta http post per l'autenticazione dell'admin
    [HttpPost]
    public IActionResult LoginAdmin(Utente utente)
    {
        //prende le cose scritte nella text box
        //DA FARE: controllare che non siano NULL e/o impedire di inserirlo e/o qualcos'altro
        string username=_configuration.GetRequiredSection("AdminCredentials").GetValue<string>("UserName");
        string password=_configuration.GetRequiredSection("AdminCredentials").GetValue<string>("Password");
        
        //controlla se le credenziali inserite sono giuste
        if (utente.Username==username && utente.Password==password)
        {
            //Credenziali corrette
            //scrivo nella sessione l'utente loggato
            _contextAccessor.HttpContext.Session.SetString("user", "admin");
            return RedirectToAction("IndexAdmin");//appena fatto il login reindirizza all'index fatto apposta per l'admin
        }
        else
        {
            //Credenziali non valide
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
    
    //-----------------------------------------PAGINA ARTICOLI------------------------------------------------
    //pagina per reindirizzare alla pagina degli articoli
    public IActionResult Articoli(string categoria)
    {
        // Visualizzo tutti gli Articoli per categoria nell'Index
        var listArticoli = _gestioneDati.GetArticoloPerCategoria(categoria);
        return View(listArticoli);
    }
    //--------------------------------------SOLO PER ADMIN-SOLO AUTORIZZATI--------------------------------------------
    
    //Index fatto apposta per l'admin (l'utente normale non potra' accedervi)
    public IActionResult IndexAdmin()
    {
        //e' stata creata una nuova classe chiamata AdminViewModel
        //per riuscire a "trasportare" all'indexAdmin piu' di 1 elemento
        //la classe sara' il model e dopo verra' scomposta per tutte le
        //operazioni necessarie
        var adminViewModel = new AdminViewModel()
        {
            Ordini = _gestioneDati.GetTuttiOrdini(),
            Associazioni = _gestioneDati.GetTutteAssociazioni(),
            Articoli = _gestioneDati.GetTuttiArticoli()
        };
        
        return View(adminViewModel);
    }
    
    //-----------------------------------------------------------------------------------------------------------------

    public IActionResult Index()
    {
        // Visualizzo le categorie nell'Index
        var listCategorie = _gestioneDati.GetCategorie();
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