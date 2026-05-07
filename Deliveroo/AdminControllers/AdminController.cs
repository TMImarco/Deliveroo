using Deliveroo.AdminViewModels;
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
			ClassificaArticoli = _gestioneDati.GetArticoliOrdineNumero_ordini(),
			NumeroTotaleClassificati = numeroTotaleClassificati
		};
		return View(adminViewModel);
	}
	
	[HttpGet]
	public IActionResult DettaglioOrdine(int id)
	{
		var ordine = _gestioneDati.GetOrdinePerId(id);
		if (ordine == null) return NotFound();

		var viewModel = new DettaglioOrdineViewModel()
		{
			Ordine = ordine,
			Righe = _gestioneDati.GetRigheDettaglioPerOrdine(id)
		};
		return View(viewModel);
	}
}