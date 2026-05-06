# Deliveroo
## Progetto di coppia
Coppia: Agapi, Garbin
Scandeza 09/05/2026

Dividiamo il progetto in tre macro argomenti:
## Admin
###### Programmatore: Garbin

### Funzioni implementate:
1. Login (utente: admin pwd: admin)
2. Logout
3. Pagina principale (IndexAdmin)
4. Modifica articolo
5. Aggiungi articolo
6. Vedere le associazioni di acquisto con le "confidence" in 'A' + refresh confidence
7. Elimina articolo

### Funzioni da implementare:
1. Grafica
2. ModificaArticoli: i pulsanti piccoli vicino ad ogni elemento solo per abilitare la modifica
3. Modificare la visualizzazione delle associazioni

### Note
1. Opzione elimina/modifica/aggiungi categoria scartate
2. Quando seleziono un articolo da modificare, ci mette sempre tantissimo, PERCHE'?

## Utente
###### Programmatore: Agapi

### Funzioni implementate:
1. HomePage delle MacroCategorie (pagina Index)
2. Pagine di tutti gli Articoli e del singolo Articolo selezionato
3. Aggiunta al Carrello + Pagina del Carrello
4. Pagina del Riepilogo + Aggiungere l'ordine nel DB
5. Raccomandazioni nel Riepilogo
6. Visualizzazione delle foto Categorie e Articoli
7. Controller organizzati (Utente e Admin)
8. Popup dei dettagli del pagamento (dati della carta; scopo di bellezza)
9. Raccomandazioni nell'InserimentoArticolo ("Consigliati per te")

### Funzioni da implementare:
1. Grafica!!!
2. ???Barra di ricerca/filtri negli articoli
3. ???Toast Notification quando aggiungo/tolgo un articolo dal carrello (and somewhere else)
4. ???Sistema di recensioni + Articoli preferiti (questo significa che l'utente ha un suo proprio login)

### Note
TUTTE IDEE che sarebbero carine da implementare, ma visto la scadenza in 2gg it's challenging 

## Database
###### Programmatore: Agapi,Garbin

### Funzioni implementate:
1. Tabella articoli
2. Tabella associazioni
3. Tabella categoria
4. Tabella ordini
5. Tabella righe_dettaglio
6. Trigger aggiornaAssociazioni
7. Trigger aumentaNumeroOrdiniArticolo

### Funzioni da implementare
1. Costrains totaleImporto (forse?):
   (La somma delle righe dettaglio di un certo ordine deve essere uguale all'importo totale scritto nella tabella articoli - per controllo che le righe dettaglio siano inserite correttamente)
2. Eliminare la colonna foto dalla tabella articoli e anche da categoria (modificare tutti i metodi crud)

## Come funziona il caricamento immagini
1. Caricare le foto su cloudinary
2. Le immagini hanno un loro url specifico

## Sistema di ottimizzazione immagini
1. Ricavo l'url "crudo" cioe' la foto a qualita' (dimensioni) massima
   var rawUrl = (reader["imageUrl"] is DBNull) ? "" : (string)reader["imageUrl"];
2. Rimpiazzio una parte dell'url con una stringa per ridurre le dimensione dell'immagine
   var refinedUrl = rawUrl.Replace("/upload/", "/upload/w_400,h_300,c_fill,f_auto,q_auto/")

### Note:
1. Assicurarsi sempre se il TUO dumb del DB sia agggiornato. (Ultimo aggiornamento 02-05-2026)
2. Come ho gestito l'eliminazione di un articolo:
   1. idArticolo e' FK con ON DELETE CASCADE nella tabella righe_dettaglio, quindi quando elimino un articolo si elimina tutte le righe_dettaglio che hanno la sua FK 
   2. idArticolo e' FK con ON DELETE CASCADE nella tabella associaizoni, quindi quando elimino un articolo si elimina anche tutte le righe di associazioni che hanno la sua FK
