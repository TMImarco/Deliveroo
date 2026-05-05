using Microsoft.AspNetCore.Mvc;

namespace Deliveroo.AdminControllers;

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