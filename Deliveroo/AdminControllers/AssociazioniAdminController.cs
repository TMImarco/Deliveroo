using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Deliveroo.Models;
using Deliveroo.Tabelle;

namespace Deliveroo.Controllers;

public class AssociazioniAdminController : Controller
{
	private readonly GestioneDati _gestioneDati;

	public AssociazioniAdminController()
	{
		_gestioneDati = new GestioneDati();
	}

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
			vm.ListaAssociazioni = _gestioneDati.GetAssociazione(vm.IdArticoloSelezionato.Value);
		}
		return View(vm);
	}

	[HttpPost]
	public IActionResult AggiornaConfidence()
	{
		try { _gestioneDati.AggiornaConfidence(); }
		catch (Exception e) { Console.WriteLine(e); return RedirectToAction("Error", "Home"); }
		return RedirectToAction("AssociazioniAdmin");
	}
}