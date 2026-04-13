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

	private readonly GestioneCarrello _gestioneCarrello;
	private int contatore; //variabile che non serve a niente ma esempio con contextAccessore

	private readonly IHttpContextAccessor _contextAccessor;

	public HomeController(ILogger<HomeController> logger, IConfiguration configuration,
		IHttpContextAccessor contextAccessor)
	{
		_logger = logger;
		_configuration = configuration; //NON RIMUOVERE (file di configurazione)
		_contextAccessor = contextAccessor;
		_gestioneDati = new GestioneDati();
		contatore = 0;
		_gestioneCarrello = new GestioneCarrello(_contextAccessor.HttpContext.Session);
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
		string username = _configuration.GetRequiredSection("AdminCredentials").GetValue<string>("UserName");
		string password = _configuration.GetRequiredSection("AdminCredentials").GetValue<string>("Password");

		//controlla se le credenziali inserite sono giuste
		if (utente.Username == username && utente.Password == password)
		{
			//Credenziali corrette
			//scrivo nella sessione l'utente loggato
			_contextAccessor.HttpContext.Session.SetString("user", "admin");
			return RedirectToAction("IndexAdmin"); //appena fatto il login reindirizza all'index fatto apposta per l'admin
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
	public IActionResult Articoli(int idCategoria)
	{
		// Visualizzo tutti gli Articoli per categoria
		var listArticoli = _gestioneDati.GetArticoliPerCategoria(idCategoria);
		return View(listArticoli);
	}

	//pagine per reindirizzare alla pagina dell'inserimento articolo del carrello
	public IActionResult InserimentoArticolo(int id)
	{
		// Visualizzo l'articolo scelto
		var articoloScelto = _gestioneDati.GetArticoloScelto(id);
		return View(articoloScelto);
	}

	//-----------------------------------------CARRELLO------------------------------------------------

	// reindirizzamento verso la pagina riepilogo del carrello
	public IActionResult Carrello()
	{
		List<Articolo> lista = _gestioneCarrello.RecuperaCarrello();
		return View(lista);
	}
	
	public IActionResult MettiNelCarrello(int id, int qty = 1)
	{
		Articolo articolo = _gestioneDati.GetArticoloScelto(id);
		List<Articolo> lista = _gestioneCarrello.RecuperaCarrello();
		for (int i = 0; i < qty; i++)
			lista.Add(articolo);
		_gestioneCarrello.SalvaCarrello(lista);
		return RedirectToAction("InserimentoArticolo");
	}

	// per aggiornare il numero badge del carrello
	public IActionResult NumeroElementi()
	{
		return Content(_gestioneCarrello.NumeroElementiCarrello().ToString());
	}
	
	/* AGGIUNGERE O TOGLIERE DALLA QUANTITA DAL CARRELLO */
	public IActionResult Incrementa(int id)
	{
		List<Articolo> lista = _gestioneCarrello.RecuperaCarrello();
		Articolo articolo = _gestioneDati.GetArticoloScelto(id);
		lista.Add(articolo);
		_gestioneCarrello.SalvaCarrello(lista);
		return RedirectToAction("Carrello");
	}

	public IActionResult Decrementa(int id)
	{
		List<Articolo> lista = _gestioneCarrello.RecuperaCarrello();
		Articolo daRimuovere = lista.FirstOrDefault(a => a.IdArticolo == id);
		if (daRimuovere != null)
			lista.Remove(daRimuovere);
		_gestioneCarrello.SalvaCarrello(lista);
		return RedirectToAction("Carrello");
	}

	public IActionResult Rimuovi(int id)
	{
		List<Articolo> lista = _gestioneCarrello.RecuperaCarrello();
		lista.RemoveAll(a => a.IdArticolo == id);
		_gestioneCarrello.SalvaCarrello(lista);
		return RedirectToAction("Carrello");
	}

	public IActionResult SvuotaCarrello()
	{
		_gestioneCarrello.SvuotaCarrello();
		List<Articolo> lista = _gestioneCarrello.RecuperaCarrello();
		_gestioneCarrello.SalvaCarrello(lista);
		return View("Carrello", lista);
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
			Articoli = _gestioneDati.GetTuttiArticoli(),
			OrdiniTotaliDiOgniCategoria = _gestioneDati.GetOrdiniTotaliDiOgniCategoria(),
			ClassificaArticoli = _gestioneDati.GetTop10Articoli()
		};

		return View(adminViewModel);
	}

	//-----------------------------------------------------------------------------------------------------------------

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