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
    public IActionResult ModificaArticolo(int id, ArticoloModificaArticolo input)
    {
        var articoloDb = _gestioneDati.GetArticoloScelto(id);

        // Nome
        if (!string.IsNullOrWhiteSpace(input.Nome) &&
            input.Nome != articoloDb.Nome)
        {
            articoloDb.Nome = input.Nome;
        }

        // Descrizione
        if (!string.IsNullOrWhiteSpace(input.Descrizione) &&
            input.Descrizione != articoloDb.Descrizione)
        {
            articoloDb.Descrizione = input.Descrizione;
        }

        // Prezzo
        if (input.Prezzo != articoloDb.Prezzo)
        {
            articoloDb.Prezzo = (decimal)input.Prezzo!;
        }

        // Categoria
        if (input.IdCategoria != articoloDb.Categoria.IdCategoria)
        {
            articoloDb.Categoria.IdCategoria = (int)input.IdCategoria!;
        }

        // =========================
        // GESTIONE IMMAGINE
        // =========================

        if (input.ResetImage)
        {
            // Caso 1: l'utente ha cliccato "Ripristina"
            // → NON fare nulla → mantieni immagine attuale
        }
        else if (input.ImageUrl != null && input.ImageUrl.Length > 0)
        {
            // Caso 2: nuova immagine caricata
            var percorso = _cloudinaryService.UploadImage(input.ImageUrl);

            if (!string.IsNullOrEmpty(percorso) &&
                percorso != articoloDb.ImageUrl)
            {
                articoloDb.ImageUrl = percorso;
            }
        }
        // Caso 3: nessuna modifica → non fare nulla

        try
        {
            _gestioneDati.ModificaArticolo(id, articoloDb);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction("Error", "Home");
        }

        return RedirectToAction("ArticoliAdmin",
            new { idCategoria = articoloDb.Categoria.IdCategoria });
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