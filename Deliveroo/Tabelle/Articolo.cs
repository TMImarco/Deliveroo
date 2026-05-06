namespace Deliveroo.Tabelle;

public class Articolo
{
    public int IdArticolo { get; set; }
    public string Nome { get; set; }
    public string Descrizione { get; set; }
    public decimal Prezzo { get; set; }
    public int NumeroOrdini { get; set; }
    public Categoria Categoria { get; set; }
    public string ImageUrl { get; set; }
}