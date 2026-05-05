using Deliveroo.AdminViewModels;
using Deliveroo.Tabelle;
using Microsoft.AspNetCore.Mvc;

namespace Deliveroo.AdminControllers;

public class ArticoliAdminController : Controller
{
    private readonly GestioneDati _gestioneDati;
    private readonly CloudinaryService _cloudinaryService;

    public ArticoliAdminController(CloudinaryService cloudinaryService)
    {
        _gestioneDati = new GestioneDati();
        _cloudinaryService = cloudinaryService;
    }

    public IActionResult ArticoliAdmin(int idCategoria)
    {
        var listArticoli = _gestioneDati.GetArticoliPerCategoria(idCategoria);
        return View(listArticoli);
    }

    [HttpGet]
    public IActionResult AggiungiNuovoArticolo()
    {
        var categorie = _gestioneDati.GetTutteCategorie();
        return View(categorie);
    }

    [HttpPost]
    public IActionResult AggiungiNuovoArticolo(AggiungiNuovoArticoloAdminViewModel model)
    {
        var categoriaDb = _gestioneDati.GetCategoria(model.IdCategoria);
        var imageUrl = _cloudinaryService.UploadImage(model.UrlFoto);

        Categoria cat = new Categoria()
        {
            IdCategoria = categoriaDb.IdCategoria,
            Nome = categoriaDb.Nome,
            ImageUrl = categoriaDb.ImageUrl,
        };

        Articolo art = new Articolo()
        {
            IdArticolo = -1,
            Nome = model.Nome,
            Descrizione = model.Descrizione,
            Prezzo = model.Prezzo,
            NumeroOrdini = 0,
            Categoria = cat,
            ImageUrl = imageUrl
        };

        try
        {
            _gestioneDati.AggiungiArticolo(art);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction("Error", "Home");
        }

        return RedirectToAction("ArticoliAdmin", new { idCategoria = model.IdCategoria });
    }

    [HttpGet]
    public IActionResult ModificaArticolo(int id)
    {
        var categorie = _gestioneDati.GetTutteCategorie();
        var articoloScelto = _gestioneDati.GetArticoloScelto(id);

        var articoliCategoria = _gestioneDati.GetArticoliPerCategoria(articoloScelto.Categoria.IdCategoria);
        var ids = articoliCategoria.Select(a => a.IdArticolo).ToList();

        int index = ids.IndexOf(id);
        ViewBag.IdPrev = index > 0 ? ids[index - 1] : (int?)null;
        ViewBag.IdNext = index < ids.Count - 1 ? ids[index + 1] : (int?)null;
        ViewBag.IdCategoria = articoloScelto.Categoria.IdCategoria;

        ModificaArticoloViewModel model = new ModificaArticoloViewModel()
        {
            ArticoloScelto = articoloScelto,
            ListaCategorie = categorie
        };
        
        return View(model);
    }

    [HttpPost]
    public IActionResult ModificaFotoArticolo(IFormFile imageFile, int id)
    {
        string? percorsoFoto = null;
        try
        {
            percorsoFoto = _cloudinaryService.UploadImage(imageFile);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction("Error", "Home");
        }
        
        
        try { _gestioneDati.ModificaFotoArticolo(id, percorsoFoto); }
        catch (Exception e) { Console.WriteLine(e); return RedirectToAction("Error", "Home"); }
        
        return RedirectToAction("ModificaArticolo", new { id });
    }

    [HttpPost]
    public IActionResult ModificaNomeArticolo(string nome, int id)
    {
        try { _gestioneDati.ModificaNomeArticolo(id, nome); }
        catch (Exception e) { Console.WriteLine(e); return RedirectToAction("Error", "Home"); }
        return RedirectToAction("ModificaArticolo", new { id });
    }

    [HttpPost]
    public IActionResult ModificaDescrizioneArticolo(string descrizione, int id)
    {
        try { _gestioneDati.ModificaDescrizioneArticolo(id, descrizione); }
        catch (Exception e) { Console.WriteLine(e); return RedirectToAction("Error", "Home"); }
        return RedirectToAction("ModificaArticolo", new { id });
    }

    [HttpPost]
    public IActionResult ModificaPrezzo_listinoArticolo(double prezzo_listino, int id)
    {
        try { _gestioneDati.ModificaPrezzo_listinoArticolo(id, prezzo_listino); }
        catch (Exception e) { Console.WriteLine(e); return RedirectToAction("Error", "Home"); }
        return RedirectToAction("ModificaArticolo", new { id });
    }

    [HttpPost]
    public IActionResult ModificaCategoriaArticolo(int idCategoria, int id)
    {
        try { _gestioneDati.ModificaIdCategoriaArticolo(id, idCategoria); }
        catch (Exception e) { Console.WriteLine(e); return RedirectToAction("Error", "Home"); }
        return RedirectToAction("ModificaArticolo", new { id });
    }

    public IActionResult EliminaArticolo(int id)
    {
        var art = _gestioneDati.GetArticoloScelto(id);
        
        try
        {
            _gestioneDati.EliminaArticolo(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction("Error", "Home");
        }
        return RedirectToAction("ArticoliAdmin", new { art.Categoria.IdCategoria });
    }
}