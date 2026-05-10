using Deliveroo.Tabelle;

namespace Deliveroo.AdminViewModels;

public class DettaglioUtenteViewModel
{
    public Utente Utente { get; set; }
    public List<Ordine> ListaOrdini { get; set; }
    public List<Articolo> ListaArticoliPreferiti { get; set; }
}