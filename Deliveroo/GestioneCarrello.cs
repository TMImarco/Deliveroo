using System.Text.Json;
using Deliveroo.Tabelle;

namespace Deliveroo;

public class GestioneCarrello
{
	private ISession _sessioneCorrente;

	public GestioneCarrello(ISession sessioneCorrente)
	{
		this._sessioneCorrente = sessioneCorrente;
	}
	
	public void SalvaCarrello(List<Articolo> articoliSelezionati)
	{
		string json = JsonSerializer.Serialize(articoliSelezionati);
		_sessioneCorrente.SetString("carrello", json);
	}

	public List<Articolo> RecuperaCarrello()
	{
		List<Articolo> lista;
		string json = _sessioneCorrente.GetString("carrello");
		if (json == null)
		{
			lista = new List<Articolo>();
		}
		else
		{
			lista = JsonSerializer.Deserialize<List<Articolo>>(json);
		}
        
		return lista;
	}

	public int NumeroElementiCarrello()
	{
		return RecuperaCarrello().Count;
	}

	public void SvuotaCarrello()
	{
		_sessioneCorrente.Remove("carrello");
	}
}