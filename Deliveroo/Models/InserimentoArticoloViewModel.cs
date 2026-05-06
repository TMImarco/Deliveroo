using Deliveroo.Tabelle;

namespace Deliveroo.Models;

public class InserimentoArticoloViewModel
{
	public Articolo ArticoloScelto { get; set; }
	public List<Articolo> Raccomandazioni { get; set; }
}