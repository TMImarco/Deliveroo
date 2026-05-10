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

        // Carica la lista articoli
        viewModel.ListaArticoliENomi = _gestioneDati.GetTutteIdArticolo1ENomi();

        // Prendi il primo ID come int?
        viewModel.IdArticoloSelezionato = viewModel.ListaArticoliENomi
            .SelectMany(d => d.Keys)
            .Cast<int?>()
            .FirstOrDefault();

        // Carica le associazioni solo se il valore c'è
        if (viewModel.IdArticoloSelezionato.HasValue)
        {
            viewModel.ListaAssociazioni = _gestioneDati.GetAssociazione(viewModel.IdArticoloSelezionato.Value);
        }

        foreach (var dic in viewModel.ListaArticoliENomi)
        {
            foreach (var id in dic.Keys)
            {
                viewModel.ListaArticoliPresi.Add(_gestioneDati.GetArticoloScelto(id));
            }
        }
        
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult AssociazioniAdmin(AssociazioniAdminViewModel vm)
    {
        vm.ListaArticoliENomi = _gestioneDati.GetTutteIdArticolo1ENomi();
        vm.ListaArticoliPresi = _gestioneDati.GetTuttiArticoli(); // ← aggiunta

        if (vm.IdArticoloSelezionato.HasValue)
        {
            vm.ListaAssociazioni = _gestioneDati.GetAssociazione(vm.IdArticoloSelezionato.Value);
        }

        return View(vm);
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