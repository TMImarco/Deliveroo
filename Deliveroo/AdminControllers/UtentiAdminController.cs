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

		GestioneUtentiViewModel viewModel = new GestioneUtentiViewModel()
		{
			Utenti = utenti,
			NumeroIscritti = utenti.Count()
		};
		
		return View(viewModel);
	}
	
	
	[HttpPost]
	public IActionResult ModificaUtente(int id, DettaglioUtenteViewModel input)
	{
		var utente = _gestioneDati.GetUtente(id);
		
		// Nome
		if (!string.IsNullOrWhiteSpace(input.Utente.Nome) &&
		    input.Utente.Nome != utente.Nome)
		{
			utente.Nome = input.Utente.Nome;
		}
		// Username
		if (!string.IsNullOrWhiteSpace(input.Utente.Username) &&
		    input.Utente.Username != utente.Username)
		{
			utente.Username = input.Utente.Username;
		}
		// Telefono
		if (!string.IsNullOrWhiteSpace(input.Utente.Telefono) &&
		    input.Utente.Telefono != utente.Telefono)
		{
			utente.Telefono = input.Utente.Telefono;
		}
		// Indirizzo
		if (!string.IsNullOrWhiteSpace(input.Utente.Indirizzo) &&
		    input.Utente.Indirizzo != utente.Indirizzo)
		{
			utente.Indirizzo = input.Utente.Indirizzo;
		}
		// Password
		if (!string.IsNullOrWhiteSpace(input.Utente.Password) &&
		    input.Utente.Password != utente.Password)
		{
			utente.Password = input.Utente.Password;
		}
		
		try
		{
			_gestioneDati.ModificaUtente(utente);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return RedirectToAction("Error", "Home");
		}

		return RedirectToAction("DettaglioUtenteAdmin", new { id = id });
	}
}