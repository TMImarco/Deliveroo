using Deliveroo.Tabelle;

namespace Deliveroo.AdminViewModels;

public class IndexAdminViewModel
{
    public List<Ordine> Ordini { get; set; }
    public List<Associazione> Associazioni { get; set; }
    public List<Articolo> Articoli { get; set; }
    public List<Dictionary<string,int>> OrdiniTotaliDiOgniCategoria { get; set; }
    public List<Articolo> ClassificaArticoli { get; set; }
    public int NumeroTotaleClassificati { get; set; }
}