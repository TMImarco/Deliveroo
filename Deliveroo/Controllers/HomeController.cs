using System.Diagnostics;
using System.Globalization;
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

	public HomeController(ILogger<HomeController> logger, IConfiguration configuration,
		IHttpContextAccessor contextAccessor, CloudinaryService cloudinaryService)
	{
		_logger = logger;
		_configuration = configuration; //NON RIMUOVERE (file di configurazione)
		_contextAccessor = contextAccessor;
		_gestioneDati = new GestioneDati();
		contatore = 0;
		_gestioneCarrello = new GestioneCarrello(_contextAccessor.HttpContext.Session);
		_cloudinaryService = cloudinaryService;
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
	/*public IActionResult Carrello()
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
	}*/

	public IActionResult Carrello()
	{
		List<Articolo> listaCarrello = new();
		
		// Salva il referer (tengo conto della pagina precedente in cui ero) solo se non viene dal carrello stesso
		var referer = Request.Headers["Referer"].ToString();
		if (!string.IsNullOrEmpty(referer) && !referer.Contains("Carrello"))
		{
			HttpContext.Session.SetString("CarrelloReferer", referer);
		}
		ViewBag.BackUrl = HttpContext.Session.GetString("CarrelloReferer") ?? "/";

		listaCarrello = _gestioneCarrello.RecuperaCarrello();
		List<int> idCarrello = listaCarrello.Select(a => a.IdArticolo).ToList();
		List<Articolo> raccomandazioni = GetRaccomandazioni(idCarrello);

		var viewModel = new CarrelloViewModel
		{
			ArticoliCarrello = listaCarrello,
			Raccomandazioni = raccomandazioni
		};

		return View(viewModel);
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
	
	public List<Articolo> GetRaccomandazioni(List<int> idArticoliCarrello)
	{
		// Raccolgo tutte le associazioni per ogni articolo nel carrello
		List<Associazione> tutteAssociazioni = new List<Associazione>();

		foreach (int idArticolo in idArticoliCarrello)
		{
			var associazioni = _gestioneDati.GetAssociazione(idArticolo);
			tutteAssociazioni.AddRange(associazioni);
		}

		// Escludo gli articoli già nel carrello e prendo i 4 con confidence più alta
		List<int> idRaccomandati = tutteAssociazioni
			.Where(a => !idArticoliCarrello.Contains(a.IdArticolo2))
			.OrderByDescending(a => a.Confidence) // non serve GetConfidence perché ordina di base in base alla confidence piu alta
			.Select(a => a.IdArticolo2)
			.Distinct()
			.Take(4)
			.ToList();

		// Recupero gli articoli completi
		return idRaccomandati.Select(id => _gestioneDati.GetArticoloScelto(id)).ToList();
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
			long idOrdine = _gestioneDati.AggiungiOrdine(ordine); // recupero l'ID

			var articoli = _gestioneCarrello.RecuperaCarrello();
			_gestioneDati.AggiungiRigheDettaglio(idOrdine, articoli); // inserisco i dettagli
			_gestioneDati.AggiornaAssociazioni(articoli);
			_gestioneCarrello.SvuotaCarrello();

			return View("Conferma");
		}

		return RedirectToAction("Riepilogo"); //wikfjnkjfnwf
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
	[HttpGet]
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
			ClassificaArticoli = _gestioneDati.GetArticoliOrdineNumero_ordini(),
			NumeroTotaleClassificati = 4
		};

		return View(adminViewModel);
	}

	[HttpPost]
	public IActionResult IndexAdmin(int numeroTotaleClassificati)
	{
		var adminViewModel = new IndexAdminViewModel()
		{
			Ordini = _gestioneDati.GetTuttiOrdini(),
			Associazioni = _gestioneDati.GetTutteAssociazioni(),
			Articoli = _gestioneDati.GetTuttiArticoli(),
			OrdiniTotaliDiOgniCategoria = _gestioneDati.GetOrdiniTotaliDiOgniCategoria(),
			ClassificaArticoli = _gestioneDati
				.GetArticoliOrdineNumero_ordini(),

			NumeroTotaleClassificati = numeroTotaleClassificati
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

		var categoriaDb = _gestioneDati.GetCategoria(model.IdCategoria);
		var imageUrl = _cloudinaryService.UploadImage(model.UrlFoto);
		var nomeImmagine = model.UrlFoto.FileName;
		
		Categoria cat = new Categoria()
		{
			IdCategoria = categoriaDb.IdCategoria,
			Nome = categoriaDb.Nome,
			PercorsoFoto = categoriaDb.PercorsoFoto,
		};

		Articolo art = new Articolo()
		{
			IdArticolo = -1,
			Nome = model.Nome,
			Foto = nomeImmagine,
			Descrizione = model.Descrizione,
			Prezzo = model.Prezzo,
			NumeroOrdini = 0,
			Categoria = cat,
			ImageUrl = imageUrl
		};

		// Ora puoi usare i valori per salvare l'articolo nel DB
		_gestioneDati.AggiungiArticolo(art);

		//ritorna alla vista di tutti gli articoli corrispondenti alla categoria in cui sei dentro
		return RedirectToAction("ArticoliAdmin", new { idCategoria = model.IdCategoria });
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
		return View((articoloScelto, categorie));
	}

	[HttpPost]
	public IActionResult ModificaFotoArticolo(string percorsoFoto, int id)
	{
		_gestioneDati.ModificaFotoArticolo(id, percorsoFoto);
		return RedirectToAction("ModificaArticolo", new { id });
	}
	
	[HttpPost]
	public IActionResult ModificaNomeArticolo(string nome, int id) //il id come lo trova? Con asp-route-id
	{
		_gestioneDati.ModificaNomeArticolo(id, nome);
		return RedirectToAction("ModificaArticolo", new { id });
	}

	[HttpPost]
	public IActionResult ModificaDescrizioneArticolo(string descrizione, int id)
	{
		_gestioneDati.ModificaDescrizioneArticolo(id, descrizione);
		return RedirectToAction("ModificaArticolo", new { id });
	}

	[HttpPost]
	public IActionResult ModificaPrezzo_listinoArticolo(double prezzo_listino, int id)
	{
		_gestioneDati.ModificaPrezzo_listinoArticolo(id, prezzo_listino);
		return RedirectToAction("ModificaArticolo", new { id });
	}

	[HttpPost]
	public IActionResult ModificaCategoriaArticolo(int idCategoria, int id)
	{
		_gestioneDati.ModificaIdCategoriaArticolo(id, idCategoria);
		return RedirectToAction("ModificaArticolo", new { id });
	}

	[HttpPost]
	public IActionResult EliminaArticolo(int id)
	{
		var articolo = _gestioneDati.GetArticoloScelto(id);

		TempData["IdArticolo"] = articolo.IdArticolo;
		TempData["Nome"] = articolo.Nome;
		TempData["Foto"] = articolo.Foto;
		TempData["Descrizione"] = articolo.Descrizione;
		TempData["Prezzo"] = articolo.Prezzo.ToString(CultureInfo.InvariantCulture); //perche' TempData non permette double
		TempData["NumeroOrdini"] = articolo.NumeroOrdini;
		TempData["IdCategoria"] = articolo.Categoria.IdCategoria;
		TempData["NomeCategoria"] = articolo.Categoria.Nome;
		TempData["FotoCategoria"] = articolo.Categoria.PercorsoFoto;
		
		_gestioneDati.EliminaArticolo(id);

		return RedirectToAction("ArticoloEliminato");
	}

	public IActionResult ArticoloEliminato()
	{
		var categoria = new Categoria()
		{
			IdCategoria = (int)TempData["IdCategoria"],
			Nome = TempData["NomeCategoria"] as string,
			PercorsoFoto = TempData["FotoCategoria"] as string
		};
		
		var articolo = new Articolo
		{
			IdArticolo = (int)TempData["IdArticolo"],
			Nome = TempData["Nome"] as string,
			Foto =  TempData["Foto"] as string,
			Descrizione = TempData["Descrizione"] as string,
			Prezzo = Convert.ToDouble(TempData["Prezzo"]),
			NumeroOrdini = (int)TempData["NumeroOrdini"],
			Categoria = categoria
		};

		return View(articolo);
	}
	
	//ASSOCIAZIONi
	[HttpGet]
	public IActionResult AssociazioniAdmin()
	{
		var vm = new AssociazioniAdminViewModel
		{
			ListaArticoliENomi = _gestioneDati.GetTutteIdArticolo1ENomi()
		};

		return View(vm);
	}

	[HttpPost]
	public IActionResult AssociazioniAdmin(AssociazioniAdminViewModel vm)
	{
		vm.ListaArticoliENomi = _gestioneDati.GetTutteIdArticolo1ENomi();

		if (vm.IdArticoloSelezionato.HasValue)
		{
			vm.ListaAssociazioni =
				_gestioneDati.GetAssociazione(vm.IdArticoloSelezionato.Value);
		}

		return View(vm);
	}
	
	[HttpPost]
	public IActionResult AggiornaConfidence()
	{
		_gestioneDati.AggiornaConfidence();
		return RedirectToAction("AssociazioniAdmin");
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