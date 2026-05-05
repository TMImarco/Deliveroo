namespace Deliveroo.AdminViewModels;

public class AggiungiNuovoArticoloAdminViewModel
{
    public string Nome { get; set; }
    public string Descrizione { get; set; }
    public double Prezzo { get; set; }
    public int IdCategoria { get; set; }
    public IFormFile UrlFoto { get; set; }
}