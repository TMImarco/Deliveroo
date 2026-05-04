using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Deliveroo.Models;
using Deliveroo.Tabelle;

namespace Deliveroo.Controllers;

public class CategorieAdminController : Controller
{
	private readonly GestioneDati _gestioneDati;

	public CategorieAdminController()
	{
		_gestioneDati = new GestioneDati();
	}

	public IActionResult CategorieAdmin()
	{
		var listCategorie = _gestioneDati.GetTutteCategorie();
		return View(listCategorie);
	}
}