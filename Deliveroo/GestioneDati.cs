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
password=root;
Allow User Variables=True;";
        _connection = new MySqlConnection(connectionString);
        _connection.Open();
    }

    //-------- ARTICOLI ----------------------------------------------------------
    public List<Articolo> GetTuttiArticoli()
    {
        List<Articolo> listaArticoli = new List<Articolo>();

        string query = @"SELECT a.*, c.nomeCategoria, c.imageUrl AS foto_categoria
                     FROM articoli a
                     JOIN categorie c ON a.idCategoria = c.id
                     ORDER BY a.id";

        MySqlCommand command = new MySqlCommand(query, _connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            var rawUrlCat = (reader["foto_categoria"] is DBNull) ? "" : (string)reader["foto_categoria"];
            Categoria categoria = new Categoria()
            {
                IdCategoria = (int)reader["idCategoria"],
                Nome = (string)reader["nomeCategoria"],
                ImageUrl = rawUrlCat.Replace("/upload/", "/upload/w_400,h_300,c_fill,f_auto,q_auto/")
            };

            var rawUrlArt = (reader["imageUrl"] is DBNull) ? "" : (string)reader["imageUrl"];
            Articolo articolo = new Articolo()
            {
                IdArticolo = (int)reader["id"],
                Nome = (string)reader["nome"],
                Prezzo = Convert.ToDecimal((double)reader["prezzo_listino"]),
                NumeroOrdini = (int)reader["numero_ordini"],
                Descrizione = (string)reader["descrizione"],
                Categoria = categoria,
                ImageUrl = rawUrlArt.Replace("/upload/", "/upload/w_400,h_300,c_fill,f_auto,q_auto/")
            };

            listaArticoli.Add(articolo);
        }

        reader.Close();
        return listaArticoli;
    }

    public List<Articolo> GetArticoliPerCategoria(int idCategoria)
    {
        List<Articolo> listaArticoli = new List<Articolo>();

        string query = @"SELECT a.*, c.nomeCategoria, c.imageUrl AS foto_categoria
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
                ImageUrl = ((string)reader["foto_categoria"]).Replace("/upload/",
                    "/upload/w_400,h_300,c_fill,f_auto,q_auto/")
            };

            /* per ottimizzare il peso delle foto cambio il suo url */
            var rawUrl = (reader["imageUrl"] is DBNull) ? "" : (string)reader["imageUrl"];
            Articolo articolo = new Articolo()
            {
                IdArticolo = (int)reader["id"],
                Nome = (string)reader["nome"],
                Prezzo = Convert.ToDecimal((double)reader["prezzo_listino"]),
                Descrizione = (string)reader["descrizione"],
                NumeroOrdini = (int)reader["Numero_ordini"],
                Categoria = categoria,
                ImageUrl = rawUrl.Replace("/upload/", "/upload/w_200,h_200,c_fill,g_auto,f_auto,q_auto/")
            };

            listaArticoli.Add(articolo);
        }

        reader.Close();
        return listaArticoli;
    }

    public Articolo GetArticoloScelto(int id)
    {
        Articolo articolo = null;

        string query = @"SELECT a.*, c.nomeCategoria, c.imageUrl AS foto_categoria
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
                ImageUrl = ((string)reader["foto_categoria"]).Replace("/upload/",
                    "/upload/w_400,h_300,c_fill,f_auto,q_auto/")
            };

            /* per ottimizzare il peso delle foto cambio il suo url */
            var rawUrl = (reader["imageUrl"] is DBNull) ? "" : (string)reader["imageUrl"];
            articolo = new Articolo()
            {
                IdArticolo = (int)reader["id"],
                Nome = (string)reader["nome"],
                Prezzo = Convert.ToDecimal((double)reader["prezzo_listino"]),
                Descrizione = (string)reader["descrizione"],
                NumeroOrdini = (int)reader["Numero_ordini"],
                Categoria = categoria,
                ImageUrl = rawUrl.Replace("/upload/", "/upload/w_500,h_600,c_fill,f_auto,q_auto/")
            };
        }

        reader.Close();
        return articolo;
    }

    public List<Articolo> GetArticoliOrdineNumero_ordini()
    {
        List<Articolo> classifica = new List<Articolo>();

        string query = @"SELECT a.*, c.nomeCategoria, c.imageUrl AS foto_categoria
                     FROM articoli a
                     JOIN categorie c ON a.idCategoria = c.id
                     ORDER BY a.numero_ordini DESC";

        MySqlCommand command = new MySqlCommand(query, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Categoria categoria = new Categoria()
            {
                IdCategoria = (int)reader["idCategoria"],
                Nome = (string)reader["nomeCategoria"]
            };

            Articolo articolo = new Articolo()
            {
                IdArticolo = (int)reader["id"],
                Nome = (string)reader["nome"],
                Prezzo = Convert.ToDecimal((double)reader["prezzo_listino"]),
                NumeroOrdini = (int)reader["numero_ordini"],
                Categoria = categoria
            };

            classifica.Add(articolo);
        }

        return classifica;
    }
    //----------------------------------------------------------------------------

    //======= ASSOCIAZIONI =========================================================
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
                string queryInsert =
                    "INSERT INTO associazioni(id_articolo1, id_articolo2, numero_ordini, confidence) VALUES (@id1, @id2, 1, @conf)";
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
    //=============================================================================

    //++++++++ ORDINI ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
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
                Telefono = (string)reader["telefono"],
            };


            listaOrdini.Add(ordine);
        }

        reader.Close();

        return listaOrdini;
    }

    public Ordine? GetOrdinePerId(int id)
    {
        string query = "SELECT * FROM ordini WHERE id = @id";
        MySqlCommand command = new MySqlCommand(query, _connection);
        command.Parameters.AddWithValue("@id", id);
        MySqlDataReader reader = command.ExecuteReader();

        Ordine? ordine = null;
        if (reader.Read())
        {
            ordine = new Ordine()
            {
                IdOrdine = (int)reader["id"],
                Data = (DateTime)reader["data"],
                NomeCliente = (string)reader["nome_cliente"],
                Indirizzo = (string)reader["indirizzo"],
                ImportoTotale = (double)reader["importo_totale"],
                Telefono = (string)reader["telefono"],
            };
        }

        reader.Close();
        return ordine;
    }
    
    public List<Dictionary<string, int>> GetOrdiniTotaliDiOgniCategoria()
    {
        //il metodo restituirà una lista con tutte le categorie e associato il loro numero totale di ordini che ogni articolo di quella categoria ha fatto

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
    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    //########## CATEGORIE ########################################################
    public List<Categoria> GetTutteCategorie()
    {
        List<Categoria> listCategorie = new();
        string query = "SELECT * FROM categorie order by id;";

        MySqlCommand command = new MySqlCommand(query, _connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            /* per ottimizzare il peso delle foto cambio il suo url */
            var rawUrl = (reader["imageUrl"] is DBNull) ? "" : (string)reader["imageUrl"];
            Categoria cat = new()
            {
                IdCategoria = (int)reader["id"],
                Nome = (string)reader["nomeCategoria"],
                ImageUrl = rawUrl.Replace("/upload/", "/upload/w_400,h_300,c_fill,f_auto,q_auto/")
            };

            listCategorie.Add(cat);
        }

        reader.Close();
        return listCategorie;
    }

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
                IdCategoria = (int)reader["id"],
                Nome = (string)reader["nomeCategoria"],
                ImageUrl = ((string)reader["imageUrl"]).Replace("/upload/", "/upload/w_400,h_300,c_fill,f_auto,q_auto/")
            };
        }

        reader.Close();

        return cat;
    }
    //###########################################################################

    //********** RIGHE DETTAGLIO ************************************************
    public List<RigaDettaglio> GetRigheDettaglioPerOrdine(int idOrdine)
    {
        List<RigaDettaglio> righe = new List<RigaDettaglio>();

        string query = @"SELECT r.id_ordine, r.id_articolo, r.quantita, r.prezzo, a.nome
                     FROM righe_dettaglio r
                     JOIN articoli a ON r.id_articolo = a.id
                     WHERE r.id_ordine = @id_ordine";

        MySqlCommand command = new MySqlCommand(query, _connection);
        command.Parameters.AddWithValue("@id_ordine", idOrdine);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            righe.Add(new RigaDettaglio()
            {
                IdOrdine = (int)reader["id_ordine"],
                IdArticolo = (int)reader["id_articolo"],
                Quantita = (int)reader["quantita"],
                Prezzo = (double)reader["prezzo"],
                NomeArticolo = (string)reader["nome"],
            });
        }

        reader.Close();
        return righe;
    }
    //************************************************************************

    //|||||||||| CONFIDENCE ||||||||||||||||||||||||||||||||||||||||||||||||||||
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
    //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    
    //<<<<<<< AGGIUNGI <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    public void AggiungiArticolo(Articolo articolo)
    {
        string query = @"INSERT INTO articoli(nome,prezzo_listino,numero_ordini,idCategoria,descrizione,imageUrl)
VALUES (@nome,@prezzo_listino,@numero_ordini,@idCategoria,@descrizione,@imageUrl)";
        MySqlCommand command = new MySqlCommand(query, _connection);
        command.Parameters.AddWithValue("@nome", articolo.Nome);
        command.Parameters.AddWithValue("@prezzo_listino", articolo.Prezzo);
        command.Parameters.AddWithValue("@numero_ordini", articolo.NumeroOrdini);
        command.Parameters.AddWithValue("@idCategoria", articolo.Categoria.IdCategoria);
        command.Parameters.AddWithValue("@descrizione", articolo.Descrizione);
        command.Parameters.AddWithValue("@imageUrl", articolo.ImageUrl);

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
    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    
    //>>>>>>>>>>>> MODIFICA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public void ModificaArticolo(int id, Articolo modificheArticolo)
    {
        Console.WriteLine("Gestione dati: " + modificheArticolo.Prezzo);

        string query = @"UPDATE articoli
SET nome = @nome,
    imageUrl = @foto,
    prezzo_listino = @prezzo_listino,
    idCategoria = @idCategoria,
    descrizione = @descrizione
WHERE id = @id;";
        MySqlCommand command = new MySqlCommand(query, _connection);
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@nome", modificheArticolo.Nome);
        command.Parameters.AddWithValue("@foto", modificheArticolo.ImageUrl);
        command.Parameters.AddWithValue("@prezzo_listino", modificheArticolo.Prezzo);
        command.Parameters.AddWithValue("@descrizione", modificheArticolo.Descrizione);
        command.Parameters.AddWithValue("@idCategoria", modificheArticolo.Categoria.IdCategoria);
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
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    
    //~~~~~~~~ ELIMINA ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void EliminaArticolo(int id)
    {
        string query = @"DELETE FROM articoli where id = @id";
        MySqlCommand command = new MySqlCommand(query, _connection);
        command.Parameters.AddWithValue("@id", id);
        try
        {
            int esito = command.ExecuteNonQuery();
            if (esito == 0)
            {
                Console.WriteLine("ID articolo non trovato");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    
    //>>>>>>>>>>>> METODI UTENTE >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public int GetLoginUtente(string username, string password)
    {
        int idUtente = 0;
        
        string query = @"SELECT id
FROM utenti
WHERE username = @username AND password = @password";
        MySqlCommand command = new MySqlCommand(query, _connection);
        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@password", password);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            idUtente = (int)reader["id"];
        }

        reader.Close();
        return idUtente;
    }

    public Utente GetUtente(int id)
    {
        Utente utente = null;
        string query = @"SELECT *
FROM utenti
WHERE Id = @id";
        MySqlCommand command = new (query, _connection);
        command.Parameters.AddWithValue("@id", id);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            utente = new Utente()
            {
                Id = (int)reader["id"],
                Nome = (string)reader["nome"],
                Indirizzo = (string)reader["indirizzo"],
                Telefono = (string)reader["telefono"],
            };
        }
        
        reader.Close();
        return utente;
    }

    public List<Articolo> GetArticoliPreferitiPerUtente(int idUtente)
    {
        List<Articolo> listArticoliPreferiti = new();
        string query = @"SELECT a.*
FROM articoli a
INNER JOIN articoli_preferiti f ON a.Id = f.IdArticolo
WHERE f.IdUtente = @idUtente";
        MySqlCommand command = new MySqlCommand(query, _connection);
        command.Parameters.AddWithValue("@idUtente", idUtente);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Categoria categoria = new Categoria()
            {
                IdCategoria = (int)reader["idCategoria"],
                Nome = (string)reader["nomeCategoria"]
            };

            Articolo articolo = new Articolo()
            {
                IdArticolo = (int)reader["id"],
                Nome = (string)reader["nome"],
                Prezzo = Convert.ToDecimal((double)reader["prezzo_listino"]),
                NumeroOrdini = (int)reader["numero_ordini"],
                Categoria = categoria
            };
            
            listArticoliPreferiti.Add(articolo);
        }
        
        reader.Close();
        return listArticoliPreferiti;
    }
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    
    //^^^^^^^^^ ALTRO ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
    public List<Dictionary<int, string>> GetTutteIdArticolo1ENomi()
    {
        List<Dictionary<int, string>> ListaIdArticolo1ENome = new List<Dictionary<int, string>>();

        string query = @"SELECT ass.id_articolo1, art.nome
from associazioni ass
inner join articoli art on ass.id_articolo1 = art.id
group by id_articolo1;";

        MySqlCommand command = new MySqlCommand(query, _connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            var id_articolo1 = reader.GetInt32("id_articolo1");
            var nomeArticolo = reader.GetString("nome");

            Dictionary<int, string> tempDic = new Dictionary<int, string>();
            tempDic.Add(id_articolo1, nomeArticolo);

            ListaIdArticolo1ENome.Add(tempDic);
        }

        reader.Close();
        return ListaIdArticolo1ENome;
    }
    //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
}