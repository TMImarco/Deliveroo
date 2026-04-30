using Deliveroo.Tabelle;

namespace Deliveroo.Models;

public class CarrelloViewModel
{
	public List<Articolo> ArticoliCarrello { get; set; }
	public List<Articolo> Raccomandazioni { get; set; }
}