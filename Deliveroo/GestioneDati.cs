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

        string query = "SELECT * FROM articoli order by id";
        MySqlCommand command = new MySqlCommand(query, _connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Articolo articolo = new Articolo()
            {
                IdArticolo = (int)reader["id"],
                Nome = (string)reader["nome"],
                Foto = (string)reader["foto"],
                Prezzo = (double)reader["prezzo_listino"],
                NumeroOrdini = (int)reader["Numero_ordini"],
                Categoria = (string)reader["categoria"]
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
        string query = "SELECT categoria, MIN(foto) AS foto FROM articoli GROUP BY categoria";

        MySqlCommand command = new MySqlCommand(query, _connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Categoria cat = new()
            {
                Nome = reader["categoria"].ToString(),
                PercorsoFoto = reader["foto"].ToString()
            };

            listCategorie.Add(cat);
        }

        reader.Close();
        return listCategorie;
    }

    public List<Articolo> GetArticoloPerCategoria(string categoria)
    {
        List<Articolo> listaArticoli = new List<Articolo>();

        string query = "SELECT * FROM articoli where categoria = @categoria order by id";
        MySqlCommand command = new MySqlCommand(query, _connection);
        command.Parameters.AddWithValue("@categoria", categoria);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Articolo articolo = new Articolo()
            {
                IdArticolo = (int)reader["id"],
                Nome = (string)reader["nome"],
                Foto = (string)reader["foto"],
                Prezzo = (double)reader["prezzo_listino"],
                NumeroOrdini = (int)reader["Numero_ordini"],
                Categoria = (string)reader["categoria"]
            };
            
            listaArticoli.Add(articolo);
        }

        reader.Close();
        return listaArticoli;
    }

}