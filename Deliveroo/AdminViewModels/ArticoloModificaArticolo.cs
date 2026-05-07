using Deliveroo.Tabelle;

namespace Deliveroo.AdminViewModels;

public class ArticoloModificaArticolo
{
    public string? Nome { get; set; }
    public string? Descrizione { get; set; }
    public decimal? Prezzo { get; set; }
    public int? IdCategoria { get; set; }
    public IFormFile? ImageUrl { get; set; }
    public bool ResetImage { get; set; }

}