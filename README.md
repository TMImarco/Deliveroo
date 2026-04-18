# Deliveroo
## Progetto di coppia
Coppia: Agapi, Garbin
Scandeza 01/05/2026

Dividiamo il progetto in tre macro argomenti:
## Admin
###### Programmatore: Garbin

### Funzioni implementate:
1. Login (utente: admin pwd: admin)
2. Logout
3. Pagina principale (IndexAdmin)
4. Modifica/elimina articolo (non testato)
5. Aggiungi articolo (non testato / da fare ultimi tocchi)

### Funzioni da implementare:
1. Aggiungi categoria
2. Elimina categoria
3. Modifica categoria (descrizione, nome)
3. Vedere le associazioni di acquisto con le "confidence" in 'A'

## Utente
###### Programmatore: Agapi

### Funzioni implementate:
1. HomePage delle MacroCategorie (pagina Index)
2. Pagine di tutti gli Articoli e del singolo Articolo selezionato
3. Aggiunta al Carrello + Pagina del Carrello
4. Pagina del Riepilogo (NON completato)

### Funzioni da implementare:
1. Aggiungere campi per i dati del cliente e pagamento nel Riepilogo
2. Fare le raccomandazioni
3. Provare API per le foto 
4. Migliorare la grafica (Utente e Admin) 

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
2. Tabella articoli on delate cascace (forse?):
   (Quando elimini un articolo, elimina tutte le fk articoli in gioro per le tabelle o no? - se no si potrebbe fare in modo che teniamo sempre tutti gli articoli visibili nel DB ma con una colonna isEliminato che con in dovuti controlli non verranno fuori nel programma)

### Note:
Assicurarsi sempre se il TUO dumb del DB sia agggiornato.