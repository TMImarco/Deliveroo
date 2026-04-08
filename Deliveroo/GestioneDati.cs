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
                NumeroOrdini = (int)reader["Numero_ordini"],
                Categoria = categoria
            };
        }

        reader.Close();
        return articolo;
    }

}