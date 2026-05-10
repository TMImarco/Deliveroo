using Deliveroo.Tabelle;

namespace Deliveroo.AdminViewModels;

public class DettaglioOrdineViewModel
{
    public Ordine Ordine { get; set; }
    public List<RigaDettaglio> Righe { get; set; }
    public Utente Utente { get; set; }
}