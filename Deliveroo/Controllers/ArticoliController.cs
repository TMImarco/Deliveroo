using Deliveroo.Models;
using Deliveroo.Tabelle;
using Microsoft.AspNetCore.Mvc;

namespace Deliveroo.Controllers;

public class ArticoliController : Controller
{
	private readonly GestioneDati _gestioneDati;

	public ArticoliController()
	{
		_gestioneDati = new GestioneDati();
	}

	public IActionResult Articoli(int idCategoria)
	{
		var listArticoli = _gestioneDati.GetArticoliPerCategoria(idCategoria);
		return View("Articoli", listArticoli);
	}

	public IActionResult InserimentoArticolo(int id)
	{
		var articoloScelto = _gestioneDati.GetArticoloScelto(id);

		var articoliCategoria = _gestioneDati.GetArticoliPerCategoria(articoloScelto.Categoria.IdCategoria);
		var ids = articoliCategoria.Select(a => a.IdArticolo).ToList();

		int index = ids.IndexOf(id);
		ViewBag.IdPrev = index > 0 ? ids[index - 1] : (int?)null;
		ViewBag.IdNext = index < ids.Count - 1 ? ids[index + 1] : (int?)null;
		ViewBag.IdCategoria = articoloScelto.Categoria.IdCategoria;

		var listRaccomandazioni = GetRaccomandazioni(id);
		var viewmodel = new InserimentoArticoloViewModel
		{
			ArticoloScelto = articoloScelto,
			Raccomandazioni = listRaccomandazioni,
		};

		return View("InserimentoArticolo", viewmodel);
	}
	
	public List<Articolo> GetRaccomandazioni(int idArticolo)
	{
		List<int> idRaccomandati = _gestioneDati.GetAssociazione(idArticolo)
			.Where(a => a.IdArticolo2 != idArticolo)
			.OrderByDescending(a => a.Confidence)
			.Select(a => a.IdArticolo2)
			.Distinct()
			.Take(4)
			.ToList();

		return idRaccomandati.Select(id => _gestioneDati.GetArticoloScelto(id)).ToList();
	}
}