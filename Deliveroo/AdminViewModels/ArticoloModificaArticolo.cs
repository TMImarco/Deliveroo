using Deliveroo.Tabelle;

namespace Deliveroo.AdminViewModels;

public class ArticoloModificaArticolo
{
    public string? Nome { get; set; }
    public string? Descrizione { get; set; }
    public string? Prezzo { get; set; }
    public int IdCategoria { get; set; }
    public IFormFile? ImageUrl { get; set; }
    public bool ResetImage { get; set; }
    public string? CloudinaryImageUrl { get; set; }

}