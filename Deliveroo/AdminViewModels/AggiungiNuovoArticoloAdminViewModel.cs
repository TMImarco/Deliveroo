namespace Deliveroo.AdminViewModels;

public class AggiungiNuovoArticoloAdminViewModel
{
    public string Nome { get; set; }
    public string Descrizione { get; set; }
    public decimal Prezzo { get; set; }
    public int IdCategoria { get; set; }
    public IFormFile UrlFoto { get; set; }
}