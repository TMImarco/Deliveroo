using Deliveroo.Models;
using Deliveroo.Tabelle;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

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
		
		int? idUtente = HttpContext.Session.GetInt32("userId");
		ViewBag.IsPreferitoAttivo = idUtente != null && _gestioneDati.IsPreferitoPerUtente(idUtente.Value, articoloScelto.IdArticolo);
		
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
	
	/* BARRA DI RICERCA */
	[HttpGet]
	public IActionResult Cerca(string q)
	{
		if (string.IsNullOrWhiteSpace(q))
			return Json(new List<object>());

		var risultati = _gestioneDati.GetTuttiArticoli()
			.Where(a => a.Nome.Contains(q, StringComparison.OrdinalIgnoreCase))
			.Take(8)
			.Select(a => new {
				a.IdArticolo,
				a.Nome,
				a.ImageUrl,
				Prezzo = a.Prezzo.ToString("F2"),
				Categoria = a.Categoria.Nome
			});

		return Json(risultati);
	}
	
	/* PAGINA PREFERITI */
	public IActionResult Preferiti(int idUtente)
	{
		var preferiti = _gestioneDati.GetPreferitiPerUtente(idUtente);
		return View(preferiti);
	}

	[HttpPost]
	public IActionResult TogglePreferito(int idArticolo)
	{
		int? idUtente = HttpContext.Session.GetInt32("userId");
		if (idUtente == null)
			return Unauthorized();

		bool isPreferitoAttivo = _gestioneDati.IsPreferitoPerUtente(idUtente.Value, idArticolo);
    
		if (isPreferitoAttivo)
			_gestioneDati.RimuoviPreferitoPerUtente(idUtente.Value, idArticolo);
		else
			_gestioneDati.AggiungiPreferitoPerUtente(idUtente.Value, idArticolo);

		return Ok();
	}
}