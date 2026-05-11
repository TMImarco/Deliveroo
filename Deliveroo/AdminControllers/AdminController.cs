using Deliveroo.AdminViewModels;
using Deliveroo.Tabelle;
using Microsoft.AspNetCore.Mvc;

namespace Deliveroo.AdminControllers;

public class AdminController : Controller
{
	private readonly GestioneDati _gestioneDati;

	public AdminController()
	{
		_gestioneDati = new GestioneDati();
	}

	[HttpGet]
	public IActionResult IndexAdmin()
	{
		var adminViewModel = new IndexAdminViewModel()
		{
			Ordini = _gestioneDati.GetTuttiOrdini(),
			Associazioni = _gestioneDati.GetTutteAssociazioni(),
			Articoli = _gestioneDati.GetTuttiArticoli(),
			OrdiniTotaliDiOgniCategoria = _gestioneDati.GetOrdiniTotaliDiOgniCategoria(),
			ClassificaArticoli = _gestioneDati.GetArticoliOrdineNumero_ordini(),
			NumeroTotaleClassificati = 4,
			Utenti = _gestioneDati.GetTuttiUtenti(),
			isMinore = false
		};
		return View(adminViewModel);
	}

	[HttpPost]
	public IActionResult IndexAdmin(int numeroTotaleClassificati)
	{
		bool isMinore = false;
		// Forza il minimo a 3
		if (numeroTotaleClassificati < 3)
		{
			numeroTotaleClassificati = 3;
			isMinore = true;
		}
			
		
		var adminViewModel = new IndexAdminViewModel()
		{
			Ordini = _gestioneDati.GetTuttiOrdini(),
			Associazioni = _gestioneDati.GetTutteAssociazioni(),
			Articoli = _gestioneDati.GetTuttiArticoli(),
			OrdiniTotaliDiOgniCategoria = _gestioneDati.GetOrdiniTotaliDiOgniCategoria(),
			ClassificaArticoli = _gestioneDati.GetArticoliOrdineNumero_ordini(),
			NumeroTotaleClassificati = numeroTotaleClassificati,
			Utenti = _gestioneDati.GetTuttiUtenti(),
			isMinore =  isMinore
		};
		return View(adminViewModel);
	}
	
	[HttpGet]
	public IActionResult DettaglioOrdineAdmin(int id)
	{
		var ordine = _gestioneDati.GetOrdinePerId(id);
		if (ordine == null) return NotFound();

		var utente = _gestioneDati.GetUtente(ordine.IdUtente);
		
		var viewModel = new DettaglioOrdineViewModel()
		{
			Ordine = ordine,
			Righe = _gestioneDati.GetRigheDettaglioPerOrdine(id),
			Utente = utente
		};
		return View(viewModel);
	}

	[HttpGet]
	public IActionResult DettaglioUtenteAdmin(int id)
	{
		var utente = _gestioneDati.GetUtente(id);
		var ordini = _gestioneDati.GetOrdiniPerUtente(utente.Id);
		var articoliPreferiti = _gestioneDati.GetPreferitiPerUtente(utente.Id);

		var viewModel = new DettaglioUtenteViewModel()
		{
			Utente = utente,
			ListaOrdini = ordini,
			ListaArticoliPreferiti = articoliPreferiti
		};
		
		return View(viewModel);
	}
	
	[HttpGet]
	public IActionResult GestioneUtentiAdmin()
	{
		var utenti = _gestioneDati.GetTuttiUtenti();
		
		return View(utenti);
	}
}