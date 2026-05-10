using Microsoft.AspNetCore.Mvc;
using Deliveroo.Tabelle;

namespace Deliveroo.Controllers;

public class OrdiniController : Controller
{
	private readonly GestioneDati _gestioneDati;
	private readonly GestioneCarrello _gestioneCarrello;
	private readonly IHttpContextAccessor _contextAccessor;

	public OrdiniController(IHttpContextAccessor contextAccessor)
	{
		_contextAccessor = contextAccessor;
		_gestioneDati = new GestioneDati();
		_gestioneCarrello = new GestioneCarrello(_contextAccessor.HttpContext.Session);
	}

	public IActionResult Riepilogo()
	{
		/* se non ha fatto l'accesso o non ha l'account, è obbligato a farlo per completare l'ordine */
		if (HttpContext.Session.GetInt32("userId") == null)
			return RedirectToAction("Login", "Auth", new { returnUrl = "/Ordini/Riepilogo" });
		
		var referer = Request.Headers["Referer"].ToString();
		if (!string.IsNullOrEmpty(referer) && !referer.Contains("Carrello"))
		{
			HttpContext.Session.SetString("CarrelloReferer", referer);
		}
		ViewBag.BackUrl = HttpContext.Session.GetString("CarrelloReferer") ?? "/";
		
		// Autocompila se loggato
		int? idUtente = HttpContext.Session.GetInt32("userId");
		if (idUtente != null)
		{
			var utente = _gestioneDati.GetUtente(idUtente.Value);
			ViewBag.NomeUtente = utente?.Nome;
			ViewBag.TelefonoUtente = utente?.Telefono;
			ViewBag.IndirizzoUtente = utente?.Indirizzo;
		}

		var lista = _gestioneCarrello.RecuperaCarrello();
		return View(lista);
	}

	[HttpPost]
	public IActionResult Conferma(Ordine ordine)
	{
		ordine.Data = DateTime.Now;
		ordine.ImportoTotale = (double)_gestioneCarrello.RecuperaCarrello().Sum(a => a.Prezzo);
		
		foreach (var entry in ModelState)
		{
			foreach (var error in entry.Value.Errors)
			{
				Console.WriteLine($"Campo: {entry.Key} | Errore: {error.ErrorMessage}");
			}
		}

		if (ModelState.IsValid)
		{
			long idOrdine = _gestioneDati.AggiungiOrdine(ordine);
			var articoli = _gestioneCarrello.RecuperaCarrello();
			_gestioneDati.AggiungiRigheDettaglio(idOrdine, articoli);
			_gestioneDati.AggiornaAssociazioni(articoli);
			_gestioneCarrello.SvuotaCarrello();
			return View("Conferma");
		}

		return RedirectToAction("Riepilogo");
	}
}