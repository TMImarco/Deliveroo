using Deliveroo.AdminViewModels;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;

namespace Deliveroo.AdminControllers;

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
        var viewModel = new AssociazioniAdminViewModel();
        viewModel.ListaArticoliENomi = _gestioneDati.GetTutteIdArticolo1ENomi();

        // Legge l'ID selezionato da TempData se arriva da un POST, altrimenti usa il primo
        viewModel.IdArticoloSelezionato = TempData.ContainsKey("IdArticoloSelezionato")
            ? (int?)TempData["IdArticoloSelezionato"]
            : viewModel.ListaArticoliENomi
                .SelectMany(d => d.Keys)
                .Cast<int?>()
                .FirstOrDefault();

        if (viewModel.IdArticoloSelezionato.HasValue)
        {
            viewModel.ListaAssociazioni = _gestioneDati.GetAssociazione(viewModel.IdArticoloSelezionato.Value);
        }

        foreach (var dic in viewModel.ListaArticoliENomi)
        foreach (var id in dic.Keys)
            viewModel.ListaArticoliPresi.Add(_gestioneDati.GetArticoloScelto(id));

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult AssociazioniAdmin(AssociazioniAdminViewModel vm)
    {
        // Salva l'ID selezionato in TempData e reindirizza al GET
        TempData["IdArticoloSelezionato"] = vm.IdArticoloSelezionato;

        return RedirectToAction("AssociazioniAdmin");
    }

    [HttpPost]
    public IActionResult AggiornaConfidence()
    {
        try
        {
            _gestioneDati.AggiornaConfidence();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction("Error", "Home");
        }

        return RedirectToAction("AssociazioniAdmin");
    }
}