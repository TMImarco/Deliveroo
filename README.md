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
4. Modifica articolo (per ora si puo' modificare solo il percorso della foto)
5. Aggiungi articolo (da vedere per le foto, per ora si inserisce solo il path + fare in modo che si debbano riempire tutti gli input)
6. Vedere le associazioni di acquisto con le "confidence" in 'A' + refresh confidence
7. Elimina articolo (insieme a modifica)

### Funzioni da implementare:
1. Grafica
2. Modificare schermate di visualizzazione per modifica/elimina articolo (forse?)
3. Modificare fase di riepilogo dopo avere eliminato un articolo e mettere una schermata di conferma con il riepilogo
4. Modificare la visualizzazione delle associazioni

### Note
1. Opzione elimina/modifica/aggiungi categoria scartate (per ora?)

## Utente
###### Programmatore: Agapi

### Funzioni implementate:
1. HomePage delle MacroCategorie (pagina Index)
2. Pagine di tutti gli Articoli e del singolo Articolo selezionato
3. Aggiunta al Carrello + Pagina del Carrello
4. Pagina del Riepilogo + Aggiungere l'ordine nel DB
5. Fare le raccomandazioni

### Funzioni da implementare:
1. Provare API per le foto 
2. Migliorare la grafica (Utente e Admin) 

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

### Note:
1. Assicurarsi sempre se il TUO dumb del DB sia agggiornato. (MI raccomando Agapi. Grazie)
2. Come ho gestito l'eliminazione di un articolo:
   1. idArticolo e' FK con ON DELETE CASCADE nella tabella righe_dettaglio, quindi quando elimino un articolo si elimina tutte le righe_dettaglio che hanno la sua FK 
   2. idArticolo e' FK con ON DELETE CASCADE nella tabella associaizoni, quindi quando elimino un articolo si elimina anche tutte le righe di associazioni che hanno la sua FK