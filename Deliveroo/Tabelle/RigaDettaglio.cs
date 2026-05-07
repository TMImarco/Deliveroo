namespace Deliveroo.Tabelle;

public class RigaDettaglio
{
    public int IdOrdine { get; set; }
    public int IdArticolo { get; set; }
    public int Quantita { get; set; }
    public double Prezzo { get; set; }
    
    //Aggiunta per la pagina DettaglioOrdine
    public string NomeArticolo { get; set; } = "";
}