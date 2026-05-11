using Deliveroo.AdminViewModels;
using Deliveroo.Tabelle;
using Microsoft.AspNetCore.Mvc;

namespace Deliveroo.AdminControllers;

public class UtentiAdminController : Controller
{
	private readonly GestioneDati _gestioneDati;

	public UtentiAdminController()
	{
		_gestioneDati = new GestioneDati();
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