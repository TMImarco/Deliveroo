namespace Deliveroo.Tabelle;

public class Ordine
{
    public int IdOrdine { get; set; }
    public DateTime Data { get; set; }
    public string NomeCliente { get; set; }
    public string Indirizzo { get; set; }
    public double ImportoTotale { get; set; }

}