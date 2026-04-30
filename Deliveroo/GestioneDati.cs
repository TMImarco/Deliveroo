using Deliveroo.Tabelle;
using MySql.Data.MySqlClient;

namespace Deliveroo;

public class GestioneDati
{
	private readonly MySqlConnection _connection;

	public GestioneDati()
	{
		string connectionString = @"database=deliveroo;
host=localhost;
port=3306;
user=root;
password=root";
		_connection = new MySqlConnection(connectionString);
		_connection.Open();
	}

	public List<Articolo> GetTuttiArticoli()
	{
		List<Articolo> listaArticoli = new List<Articolo>();

		string query = @"SELECT a.*, c.nomeCategoria, c.foto AS foto_categoria
                     FROM articoli a
                     JOIN categorie c ON a.idCategoria = c.id
                     ORDER BY a.id";

		MySqlCommand command = new MySqlCommand(query, _connection);
		MySqlDataReader reader = command.ExecuteReader();

		while (reader.Read())
		{
			Categoria categoria = new Categoria()
			{
				IdCategoria = (int)reader["idCategoria"],
				Nome = (string)reader["nomeCategoria"],
				PercorsoFoto = (string)reader["foto_categoria"]
			};

			Articolo articolo = new Articolo()
			{
				IdArticolo = (int)reader["id"],
				Nome = (string)reader["nome"],
				Foto = (string)reader["foto"],
				Prezzo = (double)reader["prezzo_listino"],
				NumeroOrdini = (int)reader["numero_ordini"],
				Descrizione = (string)reader["descrizione"],
				Categoria = categoria
			};

			listaArticoli.Add(articolo);
		}

		reader.Close();
		return listaArticoli;
	}

	public List<Associazione> GetTutteAssociazioni()
	{
		List<Associazione> listaAssociazioni = new List<Associazione>();

		string query = "SELECT * FROM associazioni order by id_articolo1";
		MySqlCommand command = new MySqlCommand(query, _connection);
		MySqlDataReader reader = command.ExecuteReader();

		while (reader.Read())
		{
			Associazione articolo = new Associazione()
			{
				IdArticolo1 = (int)reader["id_articolo1"],
				IdArticolo2 = (int)reader["id_articolo2"],
				NumeroOrdini = (int)reader["Numero_ordini"],
				Confidence = (double)reader["confidence"]
			};


			listaAssociazioni.Add(articolo);
		}

		reader.Close();

		return listaAssociazioni;
	}

	public List<Ordine> GetTuttiOrdini()
	{
		List<Ordine> listaOrdini = new List<Ordine>();

		string query = "SELECT * FROM ordini order by id";
		MySqlCommand command = new MySqlCommand(query, _connection);
		MySqlDataReader reader = command.ExecuteReader();

		while (reader.Read())
		{
			Ordine ordine = new Ordine()
			{
				IdOrdine = (int)reader["id"],
				Data = (DateTime)reader["data"],
				NomeCliente = (string)reader["nome_cliente"],
				Indirizzo = (string)reader["indirizzo"],
				ImportoTotale = (double)reader["importo_totale"],
			};


			listaOrdini.Add(ordine);
		}

		reader.Close();

		return listaOrdini;
	}

	public List<RigaDettaglio> GetRigheDettaglio()
	{
		List<RigaDettaglio> listaRigheDettaglio = new List<RigaDettaglio>();

		string query = "SELECT * FROM righe_dettaglio order by id_ordine";
		MySqlCommand command = new MySqlCommand(query, _connection);
		MySqlDataReader reader = command.ExecuteReader();

		while (reader.Read())
		{
			RigaDettaglio rigaDettaglio = new RigaDettaglio()
			{
				IdOrdine = (int)reader["id_ordine"],
				IdArticolo = (int)reader["id_articolo"],
				Quantita = (int)reader["quantita"],
				Prezzo = (double)reader["prezzo"],
			};


			listaRigheDettaglio.Add(rigaDettaglio);
		}

		reader.Close();

		return listaRigheDettaglio;
	}

	// METODI AGGIUNTIVI
	public List<Categoria> GetTutteCategorie()
	{
		List<Categoria> listCategorie = new();
		string query = "SELECT * FROM categorie order by id;";

		MySqlCommand command = new MySqlCommand(query, _connection);
		MySqlDataReader reader = command.ExecuteReader();

		while (reader.Read())
		{
			Categoria cat = new()
			{
				IdCategoria = (int)reader["id"],
				Nome = (string)reader["nomeCategoria"],
				PercorsoFoto = (string)reader["foto"]
			};

			listCategorie.Add(cat);
		}

		reader.Close();
		return listCategorie;
	}

	public List<Articolo> GetArticoliPerCategoria(int idCategoria)
	{
		List<Articolo> listaArticoli = new List<Articolo>();

		string query = @"SELECT a.*, c.nomeCategoria, c.foto AS foto_categoria
                     FROM articoli a
                     JOIN categorie c ON a.idCategoria = c.id
                     WHERE a.idCategoria = @idCategoria
                     ORDER BY a.id";
		MySqlCommand command = new MySqlCommand(query, _connection);
		command.Parameters.AddWithValue("@idCategoria", idCategoria);
		MySqlDataReader reader = command.ExecuteReader();

		while (reader.Read())
		{
			Categoria categoria = new Categoria()
			{
				IdCategoria = (int)reader["idCategoria"],
				Nome = (string)reader["nomeCategoria"],
				PercorsoFoto = (string)reader["foto"]
			};

			Articolo articolo = new Articolo()
			{
				IdArticolo = (int)reader["id"],
				Nome = (string)reader["nome"],
				Foto = (string)reader["foto"],
				Prezzo = (double)reader["prezzo_listino"],
				Descrizione = (string)reader["descrizione"],
				NumeroOrdini = (int)reader["Numero_ordini"],
				Categoria = categoria
			};

			listaArticoli.Add(articolo);
		}

		reader.Close();
		return listaArticoli;
	}

	public Articolo GetArticoloScelto(int id)
	{
		Articolo articolo = null;

		string query = @"SELECT a.*, c.nomeCategoria, c.foto AS foto_categoria
FROM articoli a
JOIN categorie c ON a.idCategoria = c.id
WHERE a.id = @id;";
		MySqlCommand command = new MySqlCommand(query, _connection);
		command.Parameters.AddWithValue("@id", id);
		MySqlDataReader reader = command.ExecuteReader();

		if (reader.Read())
		{
			Categoria categoria = new Categoria()
			{
				IdCategoria = (int)reader["idCategoria"],
				Nome = (string)reader["nomeCategoria"],
				PercorsoFoto = (string)reader["foto"]
			};

			articolo = new Articolo()
			{
				IdArticolo = (int)reader["id"],
				Nome = (string)reader["nome"],
				Foto = (string)reader["foto"],
				Prezzo = (double)reader["prezzo_listino"],
				Descrizione = (string)reader["descrizione"],
				NumeroOrdini = (int)reader["Numero_ordini"],
				Categoria = categoria
			};
		}

		reader.Close();
		return articolo;
	}

	//il metodo restituirà una lista con tutte le categorie e associato il loro numero totale di ordini che ogni articolo di quella categoria ha fatto
	public List<Dictionary<string, int>> GetOrdiniTotaliDiOgniCategoria()
	{
		//key: nome della categoria
		//value: numero di ordini totali per quella categoria
		List<Dictionary<string, int>> categorieENumeroOrdiniTotali = new List<Dictionary<string, int>>();

		//la query restituisce la categoria e il numero di ordini totale che tutti gli articoli di quella categoria hanno fatto
		string query = @"SELECT c.nomeCategoria, SUM(a.numero_ordini) AS totale_ordini
FROM categorie c
    JOIN articoli a ON c.id = a.idCategoria
GROUP BY c.id, c.nomeCategoria";

		MySqlCommand command = new MySqlCommand(query, _connection);
		MySqlDataReader reader = command.ExecuteReader();
		while (reader.Read())
		{
			//lettura dei dati della tabella
			string nomeCategoria = (string)reader["nomeCategoria"];
			int totaleOrdiniCategoria = reader.GetInt32("totale_ordini");

			//uso un dizionario di appoggio per inserire i dati su quello totale
			Dictionary<string, int> dict = new Dictionary<string, int>();
			dict.Add(nomeCategoria, totaleOrdiniCategoria);

			categorieENumeroOrdiniTotali.Add(dict);
		}

		reader.Close();

		return categorieENumeroOrdiniTotali;
	}

	public List<Articolo> GetTop10Articoli()
	{
		List<Articolo> classifica = new List<Articolo>();

		string query = @"SELECT a.*, c.nomeCategoria, c.foto AS foto_categoria
                     FROM articoli a
                     JOIN categorie c ON a.idCategoria = c.id
                     ORDER BY a.numero_ordini DESC
                     limit 10";

		MySqlCommand command = new MySqlCommand(query, _connection);
		MySqlDataReader reader = command.ExecuteReader();
		while (reader.Read())
		{
			Categoria categoria = new Categoria()
			{
				IdCategoria = (int)reader["idCategoria"],
				Nome = (string)reader["nomeCategoria"],
				PercorsoFoto = (string)reader["foto_categoria"]
			};

			Articolo articolo = new Articolo()
			{
				IdArticolo = (int)reader["id"],
				Nome = (string)reader["nome"],
				Foto = (string)reader["foto"],
				Prezzo = (double)reader["prezzo_listino"],
				NumeroOrdini = (int)reader["numero_ordini"],
				Descrizione = (string)reader["descrizione"],
				Categoria = categoria
			};

			classifica.Add(articolo);
		}

		return classifica;
	}

	//metodo per aggiungere un nuovo articolo da 0
	//id = null, perchè è un autoincrement nel DB
	//numero_ordini = 0, perchè se è un articolo nuovo nessuno può averlo mai ordinato
	public void AggiungiArticolo(Articolo articolo)
	{
		string query = @"INSERT INTO articoli(nome,foto,prezzo_listino,numero_ordini,idCategoria,descrizione)
VALUES (@nome,@foto,@prezzo_listino,@numero_ordini,@idCategoria,@descrizione)";
		MySqlCommand command = new MySqlCommand(query, _connection);
		command.Parameters.AddWithValue("@nome", articolo.Nome);
		command.Parameters.AddWithValue("@foto", articolo.Foto);
		command.Parameters.AddWithValue("@prezzo_listino", articolo.Prezzo);
		command.Parameters.AddWithValue("@numero_ordini", articolo.NumeroOrdini);
		command.Parameters.AddWithValue("@idCategoria", articolo.Categoria);
		command.Parameters.AddWithValue("@descrizione", articolo.Descrizione);

		//DA FARE:
		//che quando c'è un errore nell'esecuzione della query venga un errore a schermo
		try
		{
			command.ExecuteNonQuery();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	//----------METODI MODIFICA ARTICOLO ---------------
	public void ModificaNomeArticolo(int id, string nome)
	{
		string query = @"UPDATE articoli
SET nome = @nome
WHERE id = @id";
		MySqlCommand command = new MySqlCommand(query, _connection);
		command.Parameters.AddWithValue("@id", id);
		command.Parameters.AddWithValue("@nome", nome);

		//DA FARE:
		//se errore e/o articolo non trovato fare errore a schermo
		try
		{
			//se 0 = articolo non trovato e modifica non fatta
			//se 1 = articolo trovato e modifica fatta
			int esito = command.ExecuteNonQuery();
			if (esito == 0)
			{
				Console.WriteLine("Riga non trovata");
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public void ModificaFotoArticolo(int id, string foto)
	{
		string query = @"UPDATE articoli
SET foto = @foto
WHERE id = @id";
		MySqlCommand command = new MySqlCommand(query, _connection);
		command.Parameters.AddWithValue("@id", id);
		command.Parameters.AddWithValue("@foto", foto);

		//DA FARE:
		//se errore e/o articolo non trovato fare errore a schermo
		try
		{
			//se 0 = articolo non trovato e modifica non fatta
			//se 1 = articolo trovato e modifica fatta
			int esito = command.ExecuteNonQuery();
			if (esito == 0)
			{
				Console.WriteLine("Riga non trovata");
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public void ModificaPrezzo_listinoArticolo(int id, double prezzoListino)
	{
		string query = @"UPDATE articoli
SET prezzo_listino = @prezzo_listino
WHERE id = @id";
		MySqlCommand command = new MySqlCommand(query, _connection);
		command.Parameters.AddWithValue("@id", id);
		command.Parameters.AddWithValue("@prezzo_listino", prezzoListino);

		//DA FARE:
		//se errore e/o articolo non trovato fare errore a schermo
		try
		{
			//se 0 = articolo non trovato e modifica non fatta
			//se 1 = articolo trovato e modifica fatta
			int esito = command.ExecuteNonQuery();
			if (esito == 0)
			{
				Console.WriteLine("Riga non trovata");
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	//numero_ordini non si modifica?

	public void ModificaIdCategoriaArticolo(int id, int idCategoria)
	{
		string query = @"UPDATE articoli
SET idCategoria = @idCategoria
WHERE id = @id";
		MySqlCommand command = new MySqlCommand(query, _connection);
		command.Parameters.AddWithValue("@id", id);
		command.Parameters.AddWithValue("@idCategoria", idCategoria);

		//DA FARE:
		//se errore e/o articolo non trovato fare errore a schermo
		try
		{
			//se 0 = articolo non trovato e modifica non fatta
			//se 1 = articolo trovato e modifica fatta
			int esito = command.ExecuteNonQuery();
			if (esito == 0)
			{
				Console.WriteLine("Riga non trovata");
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public void ModificaDescrizioneArticolo(int id, string descrizione)
	{
		string query = @"UPDATE articoli
SET descrizione = @descrizione
WHERE id = @id";
		MySqlCommand command = new MySqlCommand(query, _connection);
		command.Parameters.AddWithValue("@id", id);
		command.Parameters.AddWithValue("@descrizione", descrizione);

		//DA FARE:
		//se errore e/o articolo non trovato fare errore a schermo
		try
		{
			//se 0 = articolo non trovato e modifica non fatta
			//se 1 = articolo trovato e modifica fatta
			int esito = command.ExecuteNonQuery();
			if (esito == 0)
			{
				Console.WriteLine("Riga non trovata");
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
	//--------------------------------------------------------------------------------

	public Categoria GetCategoria(int idCategoria)
	{
		Categoria cat = null;

		string query = @"SELECT *
FROM categorie
WHERE id = @idCategoria";
		MySqlCommand command = new MySqlCommand(query, _connection);
		command.Parameters.AddWithValue("@idCategoria", idCategoria);
		MySqlDataReader reader = command.ExecuteReader();

		if (reader.Read())
		{
			cat = new Categoria()
			{
				IdCategoria = (int)reader["idCategoria"],
				Nome = (string)reader["nome"],
				PercorsoFoto = (string)reader["percorsoFoto"],
			};
		}

		reader.Close();

		return cat;
	}

	public void EliminaArticolo(int id)
	{
		//modificare il db ed elminare a cascata le righe dettaglio con quell'articolo (?), le associazioni con quell'articolo (?)
	}

	public void AggiungiCategoria(Categoria categoria)
	{
		//semplice creazione di una nuova categorias
	}

	//----------METODI AGGIUNGI ORDINE NEL DATABASE ---------------
	public long AggiungiOrdine(Ordine ordine)
	{
		string query = @"INSERT INTO ordini(nome_cliente,indirizzo,data,importo_totale, telefono)
VALUES (@nome_cliente,@indirizzo,@data,@importo_totale, @telefono)";
		MySqlCommand command = new MySqlCommand(query, _connection);
		command.Parameters.AddWithValue("@nome_cliente", ordine.NomeCliente);
		command.Parameters.AddWithValue("@indirizzo", ordine.Indirizzo);
		command.Parameters.AddWithValue("@importo_totale", ordine.ImportoTotale);
		command.Parameters.AddWithValue("@data", ordine.Data);
		command.Parameters.AddWithValue("@telefono", ordine.Telefono);

		try
		{
			command.ExecuteNonQuery();
			return command.LastInsertedId; // proprietà di MySqlCommand
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public void AggiungiRigheDettaglio(long idOrdine, List<Articolo> articoli)
	{
		string query = @"INSERT INTO righe_dettaglio(id_ordine, id_articolo, quantita, prezzo)
VALUES (@id_ordine, @id_articolo, @quantita, @prezzo)";

		var righe = articoli
			.GroupBy(a => a.IdArticolo)
			.Select(g => new
			{
				IdArticolo = g.Key,
				Quantita = g.Count(),
				Prezzo = g.Sum(a => a.Prezzo)
			});

		foreach (var riga in righe)
		{
			MySqlCommand command = new MySqlCommand(query, _connection);
			command.Parameters.AddWithValue("@id_ordine", idOrdine);
			command.Parameters.AddWithValue("@id_articolo", riga.IdArticolo);
			command.Parameters.AddWithValue("@quantita", riga.Quantita);
			command.Parameters.AddWithValue("@prezzo", riga.Prezzo);

			try
			{
				command.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}

	public void AggiornaAssociazioni(List<Articolo> articoli)
	{
		var ids = articoli.Select(a => a.IdArticolo).Distinct().ToList();

		var coppie = ids
			.SelectMany((id1, i) => ids.Skip(i + 1), (id1, id2) => new
			{
				Id1 = Math.Min(id1, id2),
				Id2 = Math.Max(id1, id2)
			});

		foreach (var coppia in coppie)
		{
			string queryCheck = "SELECT COUNT(*) FROM associazioni WHERE id_articolo1 = @id1 AND id_articolo2 = @id2";
			MySqlCommand cmdCheck = new MySqlCommand(queryCheck, _connection);
			cmdCheck.Parameters.AddWithValue("@id1", coppia.Id1);
			cmdCheck.Parameters.AddWithValue("@id2", coppia.Id2);
			long count = (long)cmdCheck.ExecuteScalar();

			if (count > 0)
			{
				string queryUpdate =
					"UPDATE associazioni SET numero_ordini = numero_ordini + 1 WHERE id_articolo1 = @id1 AND id_articolo2 = @id2";
				MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, _connection);
				cmdUpdate.Parameters.AddWithValue("@id1", coppia.Id1);
				cmdUpdate.Parameters.AddWithValue("@id2", coppia.Id2);
				cmdUpdate.ExecuteNonQuery();
			}
			else
			{
				// 1. Conta quante volte id_articolo1 appare in totale negli ordini
				string queryTotale = "SELECT COUNT(*) FROM ordini WHERE id = @id1";
				MySqlCommand cmdTotale = new MySqlCommand(queryTotale, _connection);
				cmdTotale.Parameters.AddWithValue("@id1", coppia.Id1);
				int totaleOrdiniA = Convert.ToInt32(cmdTotale.ExecuteScalar());

// 2. Calcola confidence
				double confidence = totaleOrdiniA > 0 ? 1.0 / totaleOrdiniA : 0.0;
// (numero_ordini è 1 perché è la prima volta che compaiono insieme)

// 3. Inserisci con confidence
				string queryInsert = "INSERT INTO associazioni(id_articolo1, id_articolo2, numero_ordini, confidence) VALUES (@id1, @id2, 1, @conf)";
				MySqlCommand cmdInsert = new MySqlCommand(queryInsert, _connection);
				cmdInsert.Parameters.AddWithValue("@id1", coppia.Id1);
				cmdInsert.Parameters.AddWithValue("@id2", coppia.Id2);
				cmdInsert.Parameters.AddWithValue("@conf", confidence);
				cmdInsert.ExecuteNonQuery();
			}
		}

		// Ricalcola confidence per tutta la tabella dopo l'aggiornamento
		AggiornaConfidence();
	}

	public void AggiornaConfidence()
	{

		string queryTruncate = @"TRUNCATE TABLE associazioni";
		
		string queryConfidence = @"
INSERT INTO associazioni (
    id_articolo1,
    id_articolo2,
    numero_ordini,
    confidence
)
SELECT 
    r1.id_articolo,
    r2.id_articolo,
    COUNT(DISTINCT r1.id_ordine),
    COUNT(DISTINCT r1.id_ordine) / s.supporto
FROM righe_dettaglio r1
JOIN righe_dettaglio r2
    ON r1.id_ordine = r2.id_ordine
    AND r1.id_articolo <> r2.id_articolo
JOIN (
    SELECT 
        id_articolo,
        COUNT(DISTINCT id_ordine) AS supporto
    FROM righe_dettaglio
    GROUP BY id_articolo
) s ON r1.id_articolo = s.id_articolo
GROUP BY r1.id_articolo, r2.id_articolo
ON DUPLICATE KEY UPDATE
    numero_ordini = VALUES(numero_ordini),
    confidence = VALUES(confidence);";

		MySqlCommand commandTruncate = new MySqlCommand(queryTruncate, _connection);
		MySqlCommand commandConfidence = new MySqlCommand(queryConfidence, _connection);
		try
		{
			commandTruncate.ExecuteNonQuery();
			commandConfidence.ExecuteNonQuery();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
	
	//----------METODI PER LE RACCOMANDAZIONI ---------------
	public List<Associazione> GetAssociazione(int id_articolo1)
	{
		string query = "SELECT * FROM associazioni WHERE id_articolo1 = @id";
		List<Associazione> lista = new List<Associazione>();

		MySqlCommand cmd = new MySqlCommand(query, _connection);
		cmd.Parameters.AddWithValue("@id", id_articolo1);

		using (MySqlDataReader reader = cmd.ExecuteReader())
		{
			while (reader.Read())
			{
				lista.Add(new Associazione
				{
					IdArticolo1 = reader.GetInt32("id_articolo1"),
					IdArticolo2 = reader.GetInt32("id_articolo2"),
					NumeroOrdini = reader.GetInt32("numero_ordini"),
					Confidence = reader.GetDouble("confidence")
				});
			}
		}

		return lista;
	}
}