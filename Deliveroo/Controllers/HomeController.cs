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
		var articoloScelto = _gestioneDati.GetArticoloScelto(id);

		// Recupera tutti gli articoli della stessa categoria, ordinati per ID, così posso scorrere tra di loro
		var articoliCategoria = _gestioneDati.GetArticoliPerCategoria(articoloScelto.Categoria.IdCategoria);
		var ids = articoliCategoria.Select(a => a.IdArticolo).ToList();

		int index = ids.IndexOf(id);
		ViewBag.IdPrev = index > 0 ? ids[index - 1] : (int?)null;
		ViewBag.IdNext = index < ids.Count - 1 ? ids[index + 1] : (int?)null;
		ViewBag.IdCategoria = articoloScelto.Categoria.IdCategoria;

		return View(articoloScelto);
	}
	//-----------------------------------------CARRELLO------------------------------------------------

	// reindirizzamento verso la pagina riepilogo del carrello
	public IActionResult Carrello()
	{
		// Salva il referer (tengo conto della pagina precedente in cui ero) solo se non viene dal carrello stesso
		var referer = Request.Headers["Referer"].ToString();
		if (!string.IsNullOrEmpty(referer) && !referer.Contains("Carrello"))
		{
			HttpContext.Session.SetString("CarrelloReferer", referer);
		}
		ViewBag.BackUrl = HttpContext.Session.GetString("CarrelloReferer") ?? "/";

		var lista = _gestioneCarrello.RecuperaCarrello();
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
		return RedirectToAction("Carrello");
	}
	
	//-----------------------------------------RIEPILOGO------------------------------------------------
	public IActionResult Riepilogo()
	{
		// Salva il referer (tengo conto della pagina precedente in cui ero) solo se non viene dal carrello stesso
		var referer = Request.Headers["Referer"].ToString();
		if (!string.IsNullOrEmpty(referer) && !referer.Contains("Carrello"))
		{
			HttpContext.Session.SetString("CarrelloReferer", referer);
		}
		ViewBag.BackUrl = HttpContext.Session.GetString("CarrelloReferer") ?? "/";

		var lista = _gestioneCarrello.RecuperaCarrello();
		return View(lista);
	}
	
	//-----------------------------------------RINGRAZIAMENTI------------------------------------------------
	[HttpPost]
	public IActionResult Conferma(Ordine ordine)
	{
		ordine.Data = DateTime.Now;
		ordine.ImportoTotale = _gestioneCarrello.RecuperaCarrello().Sum(a => a.Prezzo);
		
		if (ModelState.IsValid)
		{
			_gestioneDati.AggiungiOrdine(ordine); // VERIFICARE SE FUNZIONA !!
			return View("Conferma");
		}
		return RedirectToAction("Riepilogo");
	}
	
	
	//  ----------------------------------LOGIN/LOGOUT-------------------------------------------
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

	public IActionResult Logout()
	{
		_contextAccessor.HttpContext.Session.Clear();
		return RedirectToAction("Index");
	}
	
	//--------------------------------------SOLO PER ADMIN-SOLO AUTORIZZATI--------------------------------------------

	//Index fatto apposta per l'admin (l'utente normale non potra' accedervi)
	public IActionResult IndexAdmin()
	{
		//e' stata creata una nuova classe chiamata AdminViewModel
		//per riuscire a "trasportare" all'indexAdmin piu' di 1 elemento
		//la classe sara' il model e dopo verra' scomposta per tutte le
		//operazioni necessarie
		var adminViewModel = new IndexAdminViewModel()
		{
			Ordini = _gestioneDati.GetTuttiOrdini(),
			Associazioni = _gestioneDati.GetTutteAssociazioni(),
			Articoli = _gestioneDati.GetTuttiArticoli(),
			OrdiniTotaliDiOgniCategoria = _gestioneDati.GetOrdiniTotaliDiOgniCategoria(),
			ClassificaArticoli = _gestioneDati.GetTop10Articoli()
		};

		return View(adminViewModel);
	}

	//riprende Index
	public IActionResult CategorieAdmin()
	{
		// Visualizzo le categorie nell'Index
		var listCategorie = _gestioneDati.GetTutteCategorie();
		return View(listCategorie);
	}
	
	//riprende Articoli
	public IActionResult ArticoliAdmin(int idCategoria)
	{
		// Visualizzo tutti gli Articoli per categoria
		var listArticoli = _gestioneDati.GetArticoliPerCategoria(idCategoria);
		return View(listArticoli);
	}
	
	//normale vista della pogina
	[HttpGet]
	public IActionResult AggiungiNuovoArticolo()
	{
		var categorie = _gestioneDati.GetTutteCategorie();
		return View(categorie);
	}
	
	//quando clicchi il submit del form (in questo caso il pulsante fine)
	[HttpPost]
	public IActionResult AggiungiNuovoArticolo(AggiungiNuovoArticoloAdminViewModel model)
	{
		//per fare in modo che il form capisca dove mettere
		//il nome della variabile in AggiungiNuovoArticoloAdminViewModel
		//deve corrispondere al name dell'elemento input in AggiungiNuovoArticolo.cshtml
		//<input type="text" id="nome" name="nome"> il contenuto di questa text box andra' in Nome di AggiungiNuovoArticoloAdminViewModel (e' case insensitive)
		
		// Gestione dell'immagine (se presente) --> da rivedere
		if (model.Foto != null && model.Foto.Length > 0)
		{
			var nomefile = Path.GetFileName(model.Foto.FileName);
			var percorso = Path.Combine("wwwroot/img", nomefile);
			using (var stream = new FileStream(percorso, FileMode.Create))
			{
				model.Foto.CopyTo(stream);
			}
		}

		Categoria cat = new Categoria()
		{
			IdCategoria = model.IdCategoria,
			Nome = _gestioneDati.GetCategoria(model.IdCategoria).Nome,
			PercorsoFoto = _gestioneDati.GetCategoria(model.IdCategoria).PercorsoFoto,
		};

		Articolo art = new Articolo()
		{
			IdArticolo = -1,
			Nome = model.Nome,
			Foto = model.Foto.ToString(), //da rivedere IFormatFile.ToString()
			Descrizione = model.Descrizione,
			Prezzo = model.Prezzo, //da rivedere double o decimal
			NumeroOrdini = 0,
			Categoria = cat,
		};
		
		// Ora puoi usare i valori per salvare l'articolo nel DB
		_gestioneDati.AggiungiArticolo(art);

		//ritorna alla vista di tutti gli articoli corrispondenti alla categoria in cui sei dentro
		return RedirectToAction("ArticoliAdmin", new { model.IdCategoria });
	}

	//riprende InserimentoArticolo
	[HttpGet]
	public IActionResult ModificaArticolo(int id)
	{
		var categorie = _gestioneDati.GetTutteCategorie();
		
		var articoloScelto = _gestioneDati.GetArticoloScelto(id);

		// Recupera tutti gli articoli della stessa categoria, ordinati per ID, così posso scorrere tra di loro
		var articoliCategoria = _gestioneDati.GetArticoliPerCategoria(articoloScelto.Categoria.IdCategoria);
		var ids = articoliCategoria.Select(a => a.IdArticolo).ToList();

		int index = ids.IndexOf(id);
		ViewBag.IdPrev = index > 0 ? ids[index - 1] : (int?)null;
		ViewBag.IdNext = index < ids.Count - 1 ? ids[index + 1] : (int?)null;
		ViewBag.IdCategoria = articoloScelto.Categoria.IdCategoria;

		//tupla per mandare 2 elementi
		return View((articoloScelto,categorie));
	}
	[HttpPost]
	public IActionResult ModificaNomeArticolo(string nome, int id) //il id come lo trova? Con asp-route-id
	{
		_gestioneDati.ModificaNomeArticolo(id, nome);
		return RedirectToAction("ModificaArticolo", new {id});
	}
	[HttpPost]
	public IActionResult ModificaDescrizioneArticolo(string descrizione, int id)
	{
		_gestioneDati.ModificaDescrizioneArticolo(id, descrizione);
		return RedirectToAction("ModificaArticolo", new {id});
	}
	[HttpPost]
	public IActionResult ModificaPrezzo_listinoArticolo(double prezzo_listino, int id)
	{
		_gestioneDati.ModificaPrezzo_listinoArticolo(id, prezzo_listino);
		return RedirectToAction("ModificaArticolo", new {id});
	}
	[HttpPost]
	public IActionResult ModificaCategoriaArticolo(int idCategoria, int id)
	{
		_gestioneDati.ModificaIdCategoriaArticolo(id, idCategoria);
		return RedirectToAction("ModificaArticolo", new {id});
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