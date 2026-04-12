using Deliveroo.Tabelle;
using MySql.Data.MySqlClient;

namespace Deliveroo;

public class GestioneDati
{
    private readonly MySqlConnection _connection;

    public GestioneDati()
    {
        string connectionString = "database=deliveroo;host=localhost;port=3306;user=root;password=root";
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
                NumeroOrdini = (int)reader["Numero_ordini"]
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
    public List<Categoria> GetCategorie()
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
                Descrizione = (string) reader["descrizione"],
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
                Descrizione = (string) reader["descrizione"],
                NumeroOrdini = (int)reader["Numero_ordini"],
                Categoria = categoria
            };
        }

        reader.Close();
        return articolo;
    }

    //il metodo restiutira' una lista con tutte le categorie e associato il loro numero totale di ordini che ogni articolo di quella categoria ha fatto
    public List<Dictionary<string, int>> GetOrdiniTotaliDiOgniCategoria()
    {
        //key: nome della categoria
        //value: numero di oridni totali per quella categoria
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
            //lettura dei dati della tebella
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
}