using Microsoft.AspNetCore.Mvc;
using Deliveroo.Models;
using Deliveroo.Tabelle;

namespace Deliveroo.Controllers;

public class CarrelloController : Controller
{
    private readonly GestioneDati _gestioneDati;
    private readonly GestioneCarrello _gestioneCarrello;
    private readonly IHttpContextAccessor _contextAccessor;

    public CarrelloController(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
        _gestioneDati = new GestioneDati();
        _gestioneCarrello = new GestioneCarrello(_contextAccessor.HttpContext.Session);
    }
    //-----------------------------------------CARRELLO------------------------------------------------

    // reindirizzamento verso la pagina riepilogo del carrello
    /*public IActionResult Carrello()
    {
        // Salva il referer (tengo conto della pagina precedente in cui ero) solo se non viene dal carrello stesso
        var referer = Request.Headers["Referer"].ToString();
        if (!string.IsNullOrEmpty(referer) && !referer.Contains("Carrello"))
        {
            HttpContext.Session.SetString("CarrelloReferer", referer);
        }
        ViewBag.BackUrl = HttpContext.Session.GetString("CarrelloReferer") ?? "/";

        var lista = _gestioneCarrello.RecuperaCarrello();
        return View(lista);
    }*/

    public IActionResult Carrello()
    {
        List<Articolo> listaCarrello = new();

        // Salva il referer (tengo conto della pagina precedente in cui ero) solo se non viene dal carrello stesso
        var referer = Request.Headers["Referer"].ToString();
        if (!string.IsNullOrEmpty(referer) && !referer.Contains("Carrello"))
        {
            HttpContext.Session.SetString("CarrelloReferer", referer);
        }

        ViewBag.BackUrl = HttpContext.Session.GetString("CarrelloReferer") ?? "/";

        listaCarrello = _gestioneCarrello.RecuperaCarrello();
        List<int> idCarrello = listaCarrello.Select(a => a.IdArticolo).ToList();
        List<Articolo> raccomandazioni = GetRaccomandazioni(idCarrello);

        var viewModel = new CarrelloViewModel
        {
            ArticoliCarrello = listaCarrello,
            Raccomandazioni = raccomandazioni
        };

        return View("Carrello", viewModel);
    }

    public IActionResult MettiNelCarrello(int id, int qty = 1)
    {
        Articolo articolo = _gestioneDati.GetArticoloScelto(id);
        List<Articolo> lista = _gestioneCarrello.RecuperaCarrello();
        for (int i = 0; i < qty; i++)
            lista.Add(articolo);
        _gestioneCarrello.SalvaCarrello(lista);
        return RedirectToAction("InserimentoArticolo", "Articoli");
    }

    // per aggiornare il numero badge del carrello
    public IActionResult NumeroElementi()
    {
        return Content(_gestioneCarrello.NumeroElementiCarrello().ToString());
    }

    /* AGGIUNGERE O TOGLIERE DALLA QUANTITA DAL CARRELLO */
    public IActionResult Incrementa(int id)
    {
        List<Articolo> lista = _gestioneCarrello.RecuperaCarrello();
        Articolo articolo = _gestioneDati.GetArticoloScelto(id);
        lista.Add(articolo);
        _gestioneCarrello.SalvaCarrello(lista);
        return RedirectToAction("Carrello");
    }

    public IActionResult Decrementa(int id)
    {
        List<Articolo> lista = _gestioneCarrello.RecuperaCarrello();
        Articolo daRimuovere = lista.FirstOrDefault(a => a.IdArticolo == id);
        if (daRimuovere != null)
            lista.Remove(daRimuovere);
        _gestioneCarrello.SalvaCarrello(lista);
        
        TempData["Toast"] = "Articolo rimosso dal carrello.";
        return RedirectToAction("Carrello");
    }

    public IActionResult Rimuovi(int id)
    {
        List<Articolo> lista = _gestioneCarrello.RecuperaCarrello();
        lista.RemoveAll(a => a.IdArticolo == id);
        _gestioneCarrello.SalvaCarrello(lista);
        
        TempData["Toast"] = "Articolo rimosso dal carrello.";
        return RedirectToAction("Carrello");
    }

    public IActionResult SvuotaCarrello()
    {
        _gestioneCarrello.SvuotaCarrello();
        List<Articolo> lista = _gestioneCarrello.RecuperaCarrello();
        _gestioneCarrello.SalvaCarrello(lista);
        
        TempData["Toast"] = "Carrello svuotato.";
        return RedirectToAction("Carrello");
    }

    public List<Articolo> GetRaccomandazioni(List<int> idArticoliCarrello)
    {
        // Raccolgo tutte le associazioni per ogni articolo nel carrello
        List<Associazione> tutteAssociazioni = new List<Associazione>();

        foreach (int idArticolo in idArticoliCarrello)
        {
            var associazioni = _gestioneDati.GetAssociazione(idArticolo);
            tutteAssociazioni.AddRange(associazioni);
        }

        // Escludo gli articoli già nel carrello e prendo i 4 con confidence più alta
        List<int> idRaccomandati = tutteAssociazioni
            .Where(a => !idArticoliCarrello.Contains(a.IdArticolo2))
            .OrderByDescending(a =>
                a.Confidence) // non serve GetConfidence perché ordina di base in base alla confidence piu alta
            .Select(a => a.IdArticolo2)
            .Distinct()
            .Take(4)
            .ToList();

        // Recupero gli articoli completi
        return idRaccomandati.Select(id => _gestioneDati.GetArticoloScelto(id)).ToList();
    }
}