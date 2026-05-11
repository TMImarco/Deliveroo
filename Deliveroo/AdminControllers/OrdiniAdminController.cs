using Deliveroo.AdminViewModels;
using Deliveroo.Tabelle;
using Microsoft.AspNetCore.Mvc;

namespace Deliveroo.AdminControllers;

public class OrdiniAdminController : Controller
{
	private readonly GestioneDati _gestioneDati;

	public OrdiniAdminController()
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
}