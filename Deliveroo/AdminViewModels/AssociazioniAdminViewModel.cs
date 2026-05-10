using Deliveroo.Tabelle;

namespace Deliveroo.AdminViewModels;

public class AssociazioniAdminViewModel
{
    public int? IdArticoloSelezionato { get; set; }

    public List<Dictionary<int, string>> ListaArticoliENomi { get; set; } = new();

    public List<Associazione> ListaAssociazioni { get; set; } = new();
    public List<Articolo> ListaArticoliPresi { get; set; } = new();
}