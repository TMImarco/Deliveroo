using Deliveroo.Tabelle;

namespace Deliveroo.Models;

public class PreferitiViewModel
{
	public Articolo ArticoloScelto { get; set; }
	public List<Articolo> Preferiti { get; set; }
}