-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: deliveroo
-- ------------------------------------------------------
-- Server version	8.4.6

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `articoli`
--

DROP TABLE IF EXISTS `articoli`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `articoli` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nome` text NOT NULL,
  `prezzo_listino` double NOT NULL,
  `numero_ordini` int NOT NULL,
  `idCategoria` int NOT NULL,
  `descrizione` text NOT NULL,
  `imageUrl` text,
  PRIMARY KEY (`id`),
  KEY `articoli_categorie_FK` (`idCategoria`),
  CONSTRAINT `articoli_categorie_FK` FOREIGN KEY (`idCategoria`) REFERENCES `categorie` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=121 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `articoli`
--

LOCK TABLES `articoli` WRITE;
/*!40000 ALTER TABLE `articoli` DISABLE KEYS */;
INSERT INTO `articoli` VALUES (1,'Cappuccino',1.5,8,1,'Cremoso e avvolgente, preparato con espresso intenso e latte montato a mano. Contiene: latte (allergeni: latte).','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714346/cappuccino_r08vdb.jpg'),(2,'Cornetto',1.2,8,1,'Soffice e dorato, sfogliato al burro con cuore vuoto o farcito a scelta. Allergeni: glutine, uova, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714352/cornetto_xq0vbd.jpg'),(3,'Toast prosciutto e formaggio',3.5,1,1,'Pane in cassetta tostato con prosciutto cotto e filante fontina. Croccante fuori, morbido dentro. Allergeni: glutine, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714387/toast_prosciutto_e_formaggio_qmrkrs.jpg'),(4,'Caffè espresso',1.1,2,1,'Miscela 100% arabica estratta alla perfezione, con crema densa e aroma intenso. Senza allergeni principali.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714346/caffe_espresso_mrfkhr.jpg'),(5,'Spremuta d\'arancia',2.5,4,1,'Arance fresche spremute al momento, senza zuccheri aggiunti. 100% naturale. Senza allergeni.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714381/spremuta_darancia_mvlz9v.jpg'),(6,'Nigiri salmone',5,0,2,'Riso acidulato con aceto di riso sormontato da fettine di salmone atlantico fresco. Allergeni: pesce, glutine (salsa di soia).','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714368/nigiri_salmone_knr5ch.jpg'),(7,'Uramaki california',6.5,2,2,'Roll rovesciato con surimi, avocado e cetriolo, ricoperto di sesamo tostato. Allergeni: crostacei, sesamo, glutine.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714387/uramaki_california_nhmi9z.jpg'),(8,'Hosomaki tonno',4.5,1,2,'Sottili roll di alga nori ripieni di tonno rosso e riso. Gusto essenziale e deciso. Allergeni: pesce, glutine.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714364/hosomaki_tonno_zyat7j.png'),(9,'Temaki gambero',5.5,0,2,'Cono di alga nori ripieno di gambero, riso e verdure fresche. Da mangiare con le mani! Allergeni: crostacei, glutine.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714384/temaki_gambero_yj8dia.jpg'),(10,'Sashimi misto',8,2,2,'Selezione di pesce fresco crudo: salmone, tonno, branzino. Servito con zenzero e wasabi. Allergeni: pesce.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714379/sashimi_misto_oyw3zz.jpg'),(11,'Hamburger classico',7.5,1,3,'Burger di manzo 180g, insalata, pomodoro e cipolla. Il classico intramontabile. Allergeni: glutine, uova, sesamo.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714364/hamburger_classico_h4fh0d.jpg'),(12,'Cheeseburger',8,1,3,'Burger di manzo con cheddar fondente, senape e ketchup. Semplice e irresistibile. Allergeni: glutine, latte, uova, sesamo.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714349/cheeseburger_hymd6m.jpg'),(13,'Double burger',9.5,2,3,'Doppio patty di manzo con doppio cheddar, insalata e salsa speciale. Per i veri appassionati. Allergeni: glutine, latte, uova, sesamo.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777732891/double_burger_xw9s0t.jpg'),(14,'Chicken burger',7,5,3,'Petto di pollo croccante in panatura, maionese e insalata. Leggero ma soddisfacente. Allergeni: glutine, uova, sesamo.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777732911/chicken_burger_fmvzdb.jpg'),(15,'Veggie burger',7.5,4,3,'Burger vegetale a base di legumi e cereali, con avocado e pomodoro. 100% plant-based. Allergeni: glutine, sesamo.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777732878/veggie_burger_lrqpxt.webp'),(16,'Margherita',5,1,4,'Pomodoro San Marzano, fior di latte e basilico fresco su impasto tradizionale napoletano. Allergeni: glutine, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714368/margherita_kldroy.jpg'),(17,'Diavola',6.5,1,4,'Salame piccante, pomodoro e mozzarella. Decisa e saporita, per chi ama il peperoncino. Allergeni: glutine, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714357/diavola_k3in29.jpg'),(18,'Quattro formaggi',7,1,4,'Mozzarella, gorgonzola, taleggio e parmigiano su base bianca. Cremosa e intensa. Allergeni: glutine, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714377/quattro_formaggi_rsq34u.jpg'),(19,'Capricciosa',7.5,1,4,'Prosciutto cotto, funghi, carciofi e olive su pomodoro e mozzarella. Ricca e gustosa. Allergeni: glutine, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714346/capricciosa_u6blhm.jpg'),(20,'Prosciutto e funghi',7,1,4,'Prosciutto cotto selezionato e funghi champignon su classica base rossa. Allergeni: glutine, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714379/prosciutto_e_funghi_ikjxmf.jpg'),(21,'Spaghetti al pomodoro',6,1,5,'Spaghetti con sugo di pomodoro fresco, basilico e olio extravergine. Semplicità italiana. Allergeni: glutine.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714379/spaghetti_al_pomodoro_wrlu5o.jpg'),(22,'Carbonara',7.5,2,5,'Rigatoni con guanciale croccante, pecorino romano e tuorlo d\'uovo. La vera ricetta romana. Allergeni: glutine, uova, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714350/carbonara_bdcvdi.jpg'),(23,'Lasagne',8,1,5,'Sfoglie di pasta all\'uovo con ragù di carne, besciamella e parmigiano. Cotto al forno. Allergeni: glutine, uova, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714364/lasagne_vwhier.webp'),(24,'Risotto ai funghi',7.5,1,5,'Riso Carnaroli mantecato con mix di funghi porcini e champignon, burro e parmigiano. Allergeni: latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714375/risotto_ai_funghi_nxwstc.webp'),(25,'Penne all\'arrabbiata',6.5,1,5,'Penne rigate con salsa di pomodoro piccante, aglio e peperoncino fresco. Vegana. Allergeni: glutine.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714374/penne_allarrabbiata_evbayd.jpg'),(26,'Bistecca alla griglia',12,1,6,'Controfiletto di manzo 250g grigliato al punto giusto, servito con sale e rosmarino. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714338/bistecca_alla_griglia_alnm6i.jpg'),(27,'Cotoletta',9,2,6,'Fetta di vitello impanata e fritta nel burro, alta e morbida alla milanese. Allergeni: glutine, uova, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777732900/cotoletta_kuafko.webp'),(28,'Pollo arrosto',8.5,1,6,'Pollo ruspante cotto in forno con rosmarino, aglio e olio extravergine. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714378/pollo_arrosto_g5iecc.jpg'),(29,'Salmone alla piastra',11,1,6,'Filetto di salmone atlantico cotto alla piastra con limone e erbe aromatiche. Allergeni: pesce.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714378/salmone_alla_piastra_e6hq03.jpg'),(30,'Frittura di pesce',10,1,6,'Mix di calamari, gamberi e alici in pastella leggera, fritti alla perfezione. Allergeni: pesce, crostacei, glutine.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714361/frittura_di_pesce_mxsbyi.jpg'),(31,'Bruschette',4.5,1,7,'Pane casereccio tostato con pomodorini freschi, aglio, basilico e olio extravergine. Allergeni: glutine.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714342/bruschette_bhrvf1.jpg'),(32,'Tagliere misto',9,3,7,'Selezione di salumi e formaggi italiani con miele e mostarda. Perfetto da condividere. Allergeni: latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714386/tagliere_misto_i3dsap.jpg'),(33,'Olive ascolane',5.5,4,7,'Olive ripiene di carne e impanate, fritte alla marchigiana. Croccanti e saporite. Allergeni: glutine, uova.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714371/olive_ascolane_ng6nww.jpg'),(34,'Carpaccio di manzo',8,3,7,'Fettine di manzo crude con rucola, grana, limone e olio extravergine. Allergeni: latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714351/carpaccio_di_manzo_qr6dzd.jpg'),(35,'Caprese',6.5,1,7,'Mozzarella di bufala DOP, pomodori cuore di bue e basilico fresco con olio evo. Allergeni: latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714344/caprese_twg1mb.jpg'),(36,'Tiramisù',5,4,8,'Il classico dolce italiano con savoiardi inzuppati nel caffè, mascarpone e cacao amaro. Allergeni: glutine, uova, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714389/tiramisu_hv8ipb.jpg'),(37,'Panna cotta',4.5,3,8,'Dolce al cucchiaio a base di panna e vaniglia con coulis di frutti di bosco. Allergeni: latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714370/panna_cotta_ipcul3.jpg'),(38,'Cheesecake',5.5,2,8,'Base croccante di biscotto, crema di formaggio e coulis di fragole fresche. Allergeni: glutine, latte, uova.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714349/cheesecake_argl5t.jpg'),(39,'Gelato coppetta',3.5,4,8,'Due palline di gelato artigianale a scelta tra i gusti del giorno. Allergeni: latte, uova (varia per gusto).','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714362/gelato_coppetta_le9tdp.jpg'),(40,'Brownie',4,2,8,'Tortino al cioccolato fondente con cuore morbido, servito tiepido con zucchero a velo. Allergeni: glutine, uova, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714345/brownie_akbzqj.jpg'),(41,'Patatine fritte',3.5,3,10,'Patate fresche tagliate a bastoncino e fritte in olio di semi. Croccanti e dorate. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714376/patatine_fritte_zuexfp.jpg'),(42,'Insalata mista',4,3,10,'Mix di lattuga, rucola, carote, pomodorini e cetriolo con olio e aceto. Fresca e leggera. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714367/insalata_mista_ajpkx9.jpg'),(43,'Verdure grigliate',4.5,1,10,'Melanzane, zucchine e peperoni grigliati con olio extravergine e origano. Vegano. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714389/verdure_grigliate_qr4bdy.jpg'),(44,'Patate al forno',4,2,10,'Spicchi di patata cotti in forno con rosmarino, aglio e olio evo. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714374/patate_al_forno_gmszul.jpg'),(45,'Riso bianco',3,2,10,'Riso basmati cotto al vapore, leggero e neutro. Ideale come accompagnamento. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714377/riso_bianco_jz0mbq.jpg'),(46,'Acqua naturale',1,9,9,'Acqua minerale naturale in bottiglia da 0,5L. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777732834/acqua_naturale_wccol3.jpg'),(47,'Acqua frizzante',1,1,9,'Acqua minerale frizzante in bottiglia da 0,5L. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714340/acqua_frizzante_qf8ls7.jpg'),(48,'Coca Cola',2.5,11,9,'La classica Coca-Cola in lattina da 33cl, fredda e frizzante. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714357/coca_cola_oovqoz.jpg'),(49,'Birra media',4,2,9,'Birra chiara in bottiglia da 33cl, fresca e dissetante. Allergeni: glutine.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714340/birra_media_bfnsla.jpg'),(50,'Vino rosso',5,0,9,'Calice di vino rosso della casa, selezione giornaliera. Allergeni: solfiti.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714390/vino_rosso_r8xeuu.jpg'),(51,'Tè caldo',1.5,2,1,'Tè in foglie con acqua bollente, servito con bustina di zucchero e fettina di limone. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714386/te_caldo_e0fki0.jpg'),(52,'Cioccolata calda',2.5,1,1,'Cioccolato fondente disciolto in latte caldo, denso e avvolgente. Perfetto nelle giornate fredde. Allergeni: latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714361/cioccolata_calda_wckcp3.jpg'),(53,'Succo di frutta',2,0,1,'Succo di frutta in brick da 200ml, disponibile in vari gusti. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714384/succo_di_frutta_e3omwc.jpg'),(54,'Edamame',4.5,1,2,'Baccelli di soia giovane cotti al vapore e salati. Snack leggero e proteico. Allergeni: soia.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714362/edamame_iszcs0.jpg'),(55,'Gyoza fritti',6,2,2,'Ravioli giapponesi ripieni di maiale e cavolo, fritti fino a doratura. Con salsa ponzu. Allergeni: glutine, soia.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714363/gyoza_fritti_edj3qx.jpg'),(56,'Miso soup',3,1,2,'Brodo tradizionale giapponese con pasta di miso, tofu, wakame e cipollotto. Allergeni: soia, molluschi.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714367/miso_soup_ci85ma.jpg'),(57,'BBQ Burger',10.5,1,3,'Burger di manzo con salsa BBQ affumicata, cipolla caramellata e cheddar. Allergeni: glutine, latte, uova, sesamo.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714342/bbq_burger_okfgwy.jpg'),(58,'Bacon Burger',11,0,3,'Doppio burger con bacon croccante, cheddar fuso e salsa burger. Deciso e appagante. Allergeni: glutine, latte, uova, sesamo.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714339/bacon_burger_dzg8pt.jpg'),(59,'Fish Burger',9.5,0,3,'Filetto di merluzzo in panatura croccante, maionese tartara e insalata. Allergeni: pesce, glutine, uova, sesamo.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714361/fish_burger_yhzll0.jpg'),(60,'Marinara',7,2,4,'Salsa di pomodoro, aglio, origano e olio extravergine. Vegana e senza lattosio. Allergeni: glutine.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714369/marinara_eihgsr.jpg'),(61,'Bufala',9.5,0,4,'Mozzarella di bufala DOP, pomodorini gialli e basilico fresco. Fresca e leggera. Allergeni: glutine, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714342/bufala_jnd39k.jpg'),(62,'Nduja e provola',10,0,4,'Salume calabrese piccante spalmato con provola affumicata filante. Intensa e speziata. Allergeni: glutine, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714371/nduja_e_provola_gy60ti.jpg'),(63,'Tagliatelle al ragù',10,0,5,'Pasta all\'uovo trafilata con ragù bolognese lento, parmigiano reggiano. Allergeni: glutine, uova, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714384/tagliatelle_al_ragu_clpvxv.jpg'),(64,'Gnocchi al pesto',9,0,5,'Gnocchi di patate con pesto genovese, fagiolini e patate. Fresco e profumato. Allergeni: glutine, frutta a guscio (pinoli), latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714368/gnocchi_al_pesto_x4oiqt.jpg'),(65,'Linguine alle vongole',12,1,5,'Linguine con vongole veraci, aglio, prezzemolo e vino bianco. La tradizione del mare. Allergeni: glutine, molluschi.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714365/linguine_alle_vongole_mbnndk.jpg'),(66,'Tagliata di manzo',16,1,6,'Controfiletto grigliato a punta di coltello su rucola e scaglie di grana. Allergeni: latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714384/tagliata_di_manzo_lzg1cv.jpg'),(67,'Branzino al forno',14,0,6,'Filetto di branzino al forno con erbe aromatiche, capperi e pomodorini. Allergeni: pesce.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714336/branzino_al_forno_bml3gs.jpg'),(68,'Polpette al sugo',10,1,6,'Polpette di manzo e maiale in sugo di pomodoro casalingo con basilico. Allergeni: glutine, uova.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714374/polpette_al_sugo_bcwo6c.jpg'),(69,'Supplì romani',5,1,7,'Arancini di riso al sugo con cuore di mozzarella, impanati e fritti. Tipici romani. Allergeni: glutine, uova, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714382/suppli_romani_xxhn3q.jpg'),(70,'Burrata con pomodori',8,1,7,'Burrata fresca pugliese su letto di pomodorini datterini e basilico. Allergeni: latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714342/burrata_con_pomodori_dvuss8.jpg'),(71,'Crostini al paté',5.5,0,7,'Fette di pane toscano abbrustolito con paté di fegatini di pollo. Allergeni: glutine.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714353/crostini_al_pate_bylqp0.jpg'),(72,'Cannolo siciliano',4.5,2,8,'Cialda fritta ripiena di ricotta di pecora zuccherata e gocce di cioccolato. Allergeni: glutine, latte, uova.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714343/cannolo_siciliano_vgm9j2.avif'),(73,'Profiteroles',5,1,8,'Bignè farciti di crema chantilly, ricoperti di cioccolato fondente caldo. Allergeni: glutine, uova, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714374/profiteroles_xfui1j.jpg'),(74,'Torta della nonna',4,1,8,'Crostata con crema pasticcera, pinoli e zucchero a velo. Dolce e confortante. Allergeni: glutine, uova, latte, frutta a guscio.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714386/torta_della_nonna_vqknd6.jpg'),(75,'Spinaci saltati',4,1,10,'Spinaci freschi saltati in padella con aglio e olio extravergine. Leggeri e nutrienti. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714382/spinaci_saltati_zqlo3l.jpg'),(76,'Zucchine grigliate',4,2,10,'Zucchine a fette grigliate con olio, limone e menta fresca. Vegano. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714388/zucchine_grigliate_uniw8d.webp'),(77,'Caponata siciliana',5,0,10,'Melanzane, sedano, olive e capperi in agrodolce. Ricetta tradizionale siciliana. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714343/caponata_siciliana_hrb28i.jpg'),(78,'Spritz',5.5,0,9,'Prosecco, acqua tonica e bitter Campari con fettina d\'arancia. L\'aperitivo italiano per eccellenza. Allergeni: solfiti.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714383/spritz_tlllad.jpg'),(79,'Limonata',3,2,9,'Limonata artigianale con succo di limoni freschi, acqua frizzante e zucchero di canna. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714366/limonata_lge0dw.jpg'),(80,'Succo ACE',2.5,2,9,'Succo di arancia, carota e limone con vitamina C. In brick da 200ml. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714382/succo_ace_ynocjx.jpg'),(81,'Caffè macchiato',1.3,2,1,'Espresso con una nuvola di latte caldo montato. Il classico italiano da bar. Allergeni: latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714343/caffe_macchiato_jltlup.jpg'),(82,'Caffè d\'orzo',1.2,0,1,'Bevanda calda a base di orzo tostato, senza caffeina. Ideale per chi evita il caffè. Allergeni: glutine.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714342/caffe_dorzo_zftjax.jpg'),(83,'Frappè al cioccolato',3.5,1,1,'Latte freddo frullato con cioccolato fondente e gelato, servito con panna. Allergeni: latte, uova.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714359/frappe_al_cioccolato_tkelcq.jpg'),(84,'Granita al limone',2.5,0,1,'Granita artigianale di limone siciliano con brioche a richiesta. Fresca e acidula. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714362/granita_al_limone_jzms17.png'),(85,'Dragon roll',12,2,2,'Uramaki con gambero tempura, ricoperto di fettine di avocado e salsa eel. Allergeni: crostacei, glutine, soia.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714356/dragon_roll_ik3pss.jpg'),(86,'Spicy tuna roll',10.5,1,2,'Roll con tonno rosso, salsa sriracha e cipollotto. Per chi ama il piccante. Allergeni: pesce, glutine, soia.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714380/spicy_tuna_roll_ekm8rm.jpg'),(87,'Nigiri gambero',8,1,2,'Riso acidulato sormontato da gambero bollito e sgusciato. Delicato e fresco. Allergeni: crostacei, glutine.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714368/nigiri_gambero_j9pqis.webp'),(88,'Tataki di tonno',13,1,2,'Filetto di tonno rosso scottato esternamente, servito a fette con salsa ponzu e sesamo. Allergeni: pesce, soia, sesamo.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714385/tataki_di_tonno_cmpxwq.webp'),(89,'Truffle burger',13,1,3,'Burger di manzo con crema al tartufo, fontina e rucola. Ricercato e aromatico. Allergeni: glutine, latte, uova, sesamo.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714386/truffle_burger_gva8ci.jpg'),(90,'Smash burger',10,1,3,'Patty schiacciato sulla piastra bollente per creare una crosticina perfetta, con cheddar e pickles. Allergeni: glutine, latte, sesamo.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714379/smash_burger_uujyrc.jpg'),(91,'Burger al pulled pork',12.5,0,3,'Maiale sfilacciato cotto per ore con salsa BBQ, coleslaw e senape. Allergeni: glutine, sesamo, uova.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714341/burger_al_pulled_pork_jvkn6v.jpg'),(92,'Burger di ceci',9,0,3,'Hamburger vegano a base di ceci, spezie e verdure grigliate. Plant-based. Allergeni: glutine, sesamo.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714342/burger_di_ceci_wjvavq.jpg'),(93,'Salsiccia e friarielli',10.5,0,4,'Salsiccia napoletana piccante con friarielli ripassati in padella. Tipica pizza campana. Allergeni: glutine, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714378/salsiccia_e_friarielli_ntnegc.jpg'),(94,'Prosciutto crudo e rucola',11,1,4,'Prosciutto crudo DOP con rucola fresca, scaglie di grana e pomodorini. Allergeni: glutine, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714374/prosciutto_crudo_e_rucola_x8mxl9.jpg'),(95,'Tonno e cipolla',9,1,4,'Tonno in olio d\'oliva e cipolla di Tropea su base rossa. Semplice e saporita. Allergeni: glutine, latte, pesce.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714385/tonno_e_cipolla_jomx4j.jpg'),(96,'Porcini e mozzarella',10,1,4,'Funghi porcini trifolati e mozzarella su base bianca profumata al timo. Allergeni: glutine, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714373/porcini_e_mozzarella_wamcnd.jpg'),(97,'Rigatoni alla norma',10,0,5,'Rigatoni con sugo di melanzane fritte, pomodoro, basilico e ricotta salata. Piatto siciliano. Allergeni: glutine, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714375/rigatoni_alla_norma_zyzznk.avif'),(98,'Tortellini in brodo',9.5,0,5,'Tortellini ripieni di carne in brodo di cappone fatto in casa. Tradizione emiliana. Allergeni: glutine, uova.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714386/tortellini_in_brodo_kahykf.jpg'),(99,'Orecchiette al pesto di broccoli',9,1,5,'Pasta pugliese con crema di broccoli, aglio e peperoncino. Rustica e saporita. Allergeni: glutine.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714369/orecchiette_al_pesto_di_broccoli_asnnaz.webp'),(100,'Zuppa di farro',8,1,5,'Farro perlato con verdure di stagione e legumi in brodo vegetale. Ricca e sostanziosa. Allergeni: glutine.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714388/zuppa_di_farro_hobsec.jpg'),(101,'Filetto al pepe verde',18,1,6,'Filetto di manzo in crosta di pepe verde in grani con salsa al cognac. Allergeni: latte, solfiti.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714359/filetto_al_pepe_verde_pfxona.jpg'),(102,'Scaloppine al limone',13,1,6,'Fettine di vitello infarinate con salsa al limone e prezzemolo fresco. Leggere e saporite. Allergeni: glutine, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714378/scaloppine_al_limone_fckphn.webp'),(103,'Baccalà alla livornese',14,0,6,'Baccalà dissalato in umido con pomodoro, olive taggiasche e capperi. Allergeni: pesce.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714336/baccala_alla_livornese_ljaqno.jpg'),(104,'Spiedini misti',12,1,6,'Spiedini di manzo, pollo e verdure grigliati con chimichurri. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714384/spiedini_misti_hohjee.jpg'),(105,'Arancini al ragù',5.5,2,7,'Riso fritto con ragù di carne e piselli, cuore di mozzarella filante. Allergeni: glutine, uova, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714336/arancini_al_ragu_axzwcv.webp'),(106,'Frittatine di pasta',4.5,1,7,'Pasta fritta napoletana con besciamella, prosciutto e piselli. Allergeni: glutine, uova, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714359/frittatine_di_pasta_olsi8z.jpg'),(107,'Crocchette di patate',4,6,7,'Purè di patate con prezzemolo, impanato e fritto. Croccanti fuori, morbide dentro. Vegane. Allergeni: glutine.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714353/crochette_di_patate_l7mzu3.jpg'),(108,'Tartare di salmone',9,2,7,'Salmone crudo battuto al coltello con avocado, limone e salsa di soia. Allergeni: pesce, soia.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714384/tartare_di_salmone_tflvi5.webp'),(109,'Sfogliatella',3.5,3,8,'Dolce pasticceria napoletana sfogliata ripiena di ricotta e semolino aromatizzato. Allergeni: glutine, latte, uova.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714378/sfogliatella_ty2dhx.jpg'),(110,'Mousse al cioccolato',4.5,7,8,'Mousse soffice di cioccolato fondente 70% con granella di nocciole. Intensa e vellutata. Allergeni: uova, latte, frutta a guscio.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714368/mousse_al_cioccolato_a8fhsq.jpg'),(111,'Crostata alla marmellata',3.5,4,8,'Pasta frolla con marmellata di albicocche artigianale. Dolce della tradizione casalinga. Allergeni: glutine, uova, latte.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714354/crostata_alla_marmellata_ovm7wo.jpg'),(112,'Budino al caramello',4,1,8,'Budino cremoso con salsa di caramello salato. Dolce e fondente in ogni cucchiaio. Allergeni: latte, uova.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714340/budino_al_caramello_rcr6lt.avif'),(113,'Fagioli all\'uccelletto',4.5,0,10,'Fagioli cannellini in umido con salvia, aglio e pomodoro. Contorno toscano classico. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714358/fagioli_alluccelletto_nk8q1x.webp'),(114,'Carciofi alla romana',5,1,10,'Carciofi cotti in padella con mentuccia, aglio e olio evo. Morbidi e profumati. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714351/carciofi_alla_romana_wehijk.jpg'),(115,'Peperonata',4,0,10,'Peperoni rossi e gialli in agrodolce con cipolla e pomodoro. Vegana e colorata. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714371/peperonata_i3fsun.jpg'),(116,'Funghi trifolati',5,1,10,'Misto di funghi saltati con aglio, prezzemolo e olio extravergine. Profumati e saporiti. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714359/funghi_trifolati_ovx4hm.jpg'),(117,'Aperol Spritz',6,4,9,'Aperol, Prosecco DOC e acqua tonica con fettina d\'arancia. L\'aperitivo più amato d\'Italia. Allergeni: solfiti.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714336/aperol_spritz_sgtdcf.webp'),(118,'Hugo',6,1,9,'Prosecco, sciroppo di sambuco, menta fresca e acqua tonica. Delicato e floreale. Allergeni: solfiti.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714363/hugo_eenykl.webp'),(119,'Mojito analcolico',4.5,0,9,'Lime, menta fresca, zucchero di canna e acqua tonica. Fresco e dissetante, zero alcol. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714368/mojito_analcolico_jvcj7h.webp'),(120,'Tè freddo',2.5,5,9,'Tè nero raffreddato con limone e zucchero, in bottiglia da 33cl. Allergeni: nessuno.','https://res.cloudinary.com/dii6jnk4o/image/upload/v1777714385/te_freddo_xokovb.jpg');
/*!40000 ALTER TABLE `articoli` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `articoli_preferiti`
--

DROP TABLE IF EXISTS `articoli_preferiti`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `articoli_preferiti` (
  `idUtente` int NOT NULL,
  `idArticolo` int NOT NULL,
  KEY `articoli_preferiti_articoli_FK` (`idArticolo`),
  KEY `articoli_preferiti_utenti_FK` (`idUtente`),
  CONSTRAINT `articoli_preferiti_articoli_FK` FOREIGN KEY (`idArticolo`) REFERENCES `articoli` (`id`),
  CONSTRAINT `articoli_preferiti_utenti_FK` FOREIGN KEY (`idUtente`) REFERENCES `utenti` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `articoli_preferiti`
--

LOCK TABLES `articoli_preferiti` WRITE;
/*!40000 ALTER TABLE `articoli_preferiti` DISABLE KEYS */;
INSERT INTO `articoli_preferiti` VALUES (1,102),(1,107),(1,22),(1,73),(1,87),(1,10),(1,86),(1,1),(2,2),(2,109),(2,90),(2,48),(2,32),(2,26),(2,117),(3,83),(3,88),(3,108),(3,112),(3,36),(3,91),(4,81),(4,40),(4,25),(4,57),(4,17),(5,27),(5,49),(5,33),(5,14),(5,41),(5,54),(6,34),(6,65),(6,98),(6,108),(6,111),(6,60),(7,101),(7,76),(7,49),(7,45),(7,110),(8,8),(8,7),(8,70),(9,69),(9,34),(9,37),(10,11),(10,104),(10,106),(10,80),(10,57),(10,29);
/*!40000 ALTER TABLE `articoli_preferiti` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `associazioni`
--

DROP TABLE IF EXISTS `associazioni`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `associazioni` (
  `id_articolo1` int NOT NULL,
  `id_articolo2` int NOT NULL,
  `numero_ordini` int NOT NULL,
  `confidence` double NOT NULL,
  KEY `associazioni_articoli_FK` (`id_articolo1`),
  KEY `associazioni_articoli_FK_1` (`id_articolo2`),
  CONSTRAINT `associazioni_articoli_FK` FOREIGN KEY (`id_articolo1`) REFERENCES `articoli` (`id`) ON DELETE CASCADE,
  CONSTRAINT `associazioni_articoli_FK_1` FOREIGN KEY (`id_articolo2`) REFERENCES `articoli` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `associazioni`
--

LOCK TABLES `associazioni` WRITE;
/*!40000 ALTER TABLE `associazioni` DISABLE KEYS */;
INSERT INTO `associazioni` VALUES (1,2,2,1),(1,72,1,0.5),(1,109,2,1),(2,1,2,0.666666666),(2,4,1,0.333333333),(2,72,2,0.666666666),(2,109,2,0.666666666),(4,2,1,0.5),(4,72,1,0.5),(4,111,1,0.5),(5,11,1,1),(5,41,1,1),(7,8,1,0.5),(7,46,1,0.5),(7,51,1,0.5),(7,55,1,0.5),(7,56,1,0.5),(7,88,1,0.5),(7,108,2,1),(8,7,1,1),(8,46,1,1),(8,51,1,1),(8,55,1,1),(8,108,1,1),(10,46,2,1),(10,51,1,0.5),(10,54,1,0.5),(10,55,1,0.5),(10,85,2,1),(10,86,1,0.5),(10,87,1,0.5),(11,5,1,1),(11,41,1,1),(12,107,1,1),(12,120,1,1),(14,39,1,1),(14,41,1,1),(14,48,1,1),(16,40,1,1),(16,48,1,1),(17,18,1,1),(17,42,1,1),(17,46,1,1),(17,48,1,1),(17,60,1,1),(17,94,1,1),(18,17,1,1),(18,42,1,1),(18,46,1,1),(18,48,1,1),(18,60,1,1),(18,94,1,1),(19,20,1,1),(19,46,1,1),(19,48,1,1),(19,60,1,1),(20,19,1,1),(20,46,1,1),(20,48,1,1),(20,60,1,1),(21,44,1,1),(21,48,1,1),(22,45,1,1),(22,66,1,1),(22,73,1,1),(22,107,1,1),(22,120,1,1),(25,80,1,1),(25,105,1,1),(26,38,1,1),(26,42,1,1),(26,43,1,1),(26,48,1,1),(26,104,1,1),(27,44,1,1),(27,46,1,1),(27,75,1,1),(28,33,1,1),(28,114,1,1),(28,117,1,1),(29,45,1,1),(29,106,1,1),(30,70,1,1),(30,74,1,1),(30,99,1,1),(30,120,1,1),(31,32,1,1),(31,33,1,1),(31,49,1,1),(32,31,1,0.333333333),(32,33,1,0.333333333),(32,35,1,0.333333333),(32,39,1,0.333333333),(32,49,1,0.333333333),(32,79,1,0.333333333),(32,112,1,0.333333333),(32,118,1,0.333333333),(33,28,1,0.333333333),(33,31,1,0.333333333),(33,32,1,0.333333333),(33,49,2,0.666666666),(33,114,1,0.333333333),(33,117,1,0.333333333),(34,37,1,0.5),(34,65,1,0.5),(34,69,1,0.5),(34,111,1,0.5),(34,120,1,0.5),(35,32,1,1),(35,39,1,1),(35,79,1,1),(36,46,1,1),(36,96,1,1),(37,34,1,0.5),(37,48,1,0.5),(37,69,1,0.5),(37,95,1,0.5),(38,26,1,0.5),(38,42,1,0.5),(38,43,1,0.5),(38,48,2,1),(38,90,1,0.5),(38,104,1,0.5),(39,14,1,0.5),(39,32,1,0.5),(39,35,1,0.5),(39,41,1,0.5),(39,48,1,0.5),(39,79,1,0.5),(40,16,1,0.5),(40,48,1,0.5),(40,81,1,0.5),(41,5,1,0.333333333),(41,11,1,0.333333333),(41,14,1,0.333333333),(41,39,1,0.333333333),(41,48,2,0.666666666),(41,57,1,0.333333333),(42,17,1,0.333333333),(42,18,1,0.333333333),(42,26,1,0.333333333),(42,38,1,0.333333333),(42,43,1,0.333333333),(42,46,2,0.666666666),(42,48,2,0.666666666),(42,60,1,0.333333333),(42,76,1,0.333333333),(42,94,1,0.333333333),(42,102,1,0.333333333),(42,104,1,0.333333333),(42,111,1,0.333333333),(43,26,1,1),(43,38,1,1),(43,42,1,1),(43,48,1,1),(43,104,1,1),(44,21,1,0.5),(44,27,1,0.5),(44,46,1,0.5),(44,48,1,0.5),(44,75,1,0.5),(45,22,1,0.5),(45,29,1,0.5),(45,66,1,0.5),(45,73,1,0.5),(45,106,1,0.5),(45,107,1,0.5),(45,120,1,0.5),(46,7,1,0.111111111),(46,8,1,0.111111111),(46,10,2,0.222222222),(46,17,1,0.111111111),(46,18,1,0.111111111),(46,19,1,0.111111111),(46,20,1,0.111111111),(46,27,1,0.111111111),(46,36,1,0.111111111),(46,42,2,0.222222222),(46,44,1,0.111111111),(46,48,2,0.222222222),(46,51,2,0.222222222),(46,54,1,0.111111111),(46,55,2,0.222222222),(46,60,2,0.222222222),(46,75,1,0.111111111),(46,76,2,0.222222222),(46,85,2,0.222222222),(46,86,1,0.111111111),(46,87,1,0.111111111),(46,94,1,0.111111111),(46,96,1,0.111111111),(46,101,1,0.111111111),(46,102,1,0.111111111),(46,108,1,0.111111111),(46,111,1,0.111111111),(46,116,1,0.111111111),(47,68,1,1),(47,100,1,1),(48,14,1,0.1),(48,16,1,0.1),(48,17,1,0.1),(48,18,1,0.1),(48,19,1,0.1),(48,20,1,0.1),(48,21,1,0.1),(48,26,1,0.1),(48,37,1,0.1),(48,38,2,0.2),(48,39,1,0.1),(48,40,1,0.1),(48,41,2,0.2),(48,42,2,0.2),(48,43,1,0.1),(48,44,1,0.1),(48,46,2,0.2),(48,57,1,0.1),(48,60,2,0.2),(48,89,1,0.1),(48,90,1,0.1),(48,94,1,0.1),(48,95,1,0.1),(48,104,1,0.1),(49,31,1,0.5),(49,32,1,0.5),(49,33,2,1),(51,7,1,0.5),(51,8,1,0.5),(51,10,1,0.5),(51,46,2,1),(51,55,2,1),(51,85,1,0.5),(51,86,1,0.5),(51,87,1,0.5),(51,108,1,0.5),(54,10,1,1),(54,46,1,1),(54,85,1,1),(55,7,1,0.5),(55,8,1,0.5),(55,10,1,0.5),(55,46,2,1),(55,51,2,1),(55,85,1,0.5),(55,86,1,0.5),(55,87,1,0.5),(55,108,1,0.5),(56,7,1,1),(56,88,1,1),(56,108,1,1),(57,41,1,1),(57,48,1,1),(60,17,1,0.5),(60,18,1,0.5),(60,19,1,0.5),(60,20,1,0.5),(60,42,1,0.5),(60,46,2,1),(60,48,2,1),(60,94,1,0.5),(65,34,1,1),(65,111,1,1),(65,120,1,1),(66,22,1,1),(66,45,1,1),(66,73,1,1),(66,107,1,1),(66,120,1,1),(68,47,1,1),(68,100,1,1),(69,34,1,1),(69,37,1,1),(70,30,1,1),(70,74,1,1),(70,99,1,1),(70,120,1,1),(72,1,1,0.5),(72,2,2,1),(72,4,1,0.5),(72,109,1,0.5),(73,22,1,1),(73,45,1,1),(73,66,1,1),(73,107,1,1),(73,120,1,1),(74,30,1,1),(74,70,1,1),(74,99,1,1),(74,120,1,1),(75,27,1,1),(75,44,1,1),(75,46,1,1),(76,42,1,0.5),(76,46,2,1),(76,101,1,0.5),(76,102,1,0.5),(76,111,1,0.5),(76,116,1,0.5),(79,32,1,0.5),(79,35,1,0.5),(79,39,1,0.5),(79,80,1,0.5),(79,110,1,0.5),(79,111,1,0.5),(80,25,1,0.5),(80,79,1,0.5),(80,105,1,0.5),(80,110,1,0.5),(80,111,1,0.5),(81,40,1,1),(83,109,1,1),(85,10,2,1),(85,46,2,1),(85,51,1,0.5),(85,54,1,0.5),(85,55,1,0.5),(85,86,1,0.5),(85,87,1,0.5),(86,10,1,1),(86,46,1,1),(86,51,1,1),(86,55,1,1),(86,85,1,1),(86,87,1,1),(87,10,1,1),(87,46,1,1),(87,51,1,1),(87,55,1,1),(87,85,1,1),(87,86,1,1),(88,7,1,1),(88,56,1,1),(88,108,1,1),(89,48,1,1),(90,38,1,1),(90,48,1,1),(94,17,1,1),(94,18,1,1),(94,42,1,1),(94,46,1,1),(94,48,1,1),(94,60,1,1),(95,37,1,1),(95,48,1,1),(96,36,1,1),(96,46,1,1),(99,30,1,1),(99,70,1,1),(99,74,1,1),(99,120,1,1),(100,47,1,1),(100,68,1,1),(101,46,1,1),(101,76,1,1),(101,116,1,1),(102,42,1,1),(102,46,1,1),(102,76,1,1),(102,111,1,1),(104,26,1,1),(104,38,1,1),(104,42,1,1),(104,43,1,1),(104,48,1,1),(105,25,1,1),(105,80,1,1),(106,29,1,1),(106,45,1,1),(107,12,1,0.5),(107,22,1,0.5),(107,45,1,0.5),(107,66,1,0.5),(107,73,1,0.5),(107,120,2,1),(108,7,2,1),(108,8,1,0.5),(108,46,1,0.5),(108,51,1,0.5),(108,55,1,0.5),(108,56,1,0.5),(108,88,1,0.5),(109,1,2,0.666666666),(109,2,2,0.666666666),(109,72,1,0.333333333),(109,83,1,0.333333333),(110,79,1,1),(110,80,1,1),(110,111,1,1),(111,4,1,0.25),(111,34,1,0.25),(111,42,1,0.25),(111,46,1,0.25),(111,65,1,0.25),(111,76,1,0.25),(111,79,1,0.25),(111,80,1,0.25),(111,102,1,0.25),(111,110,1,0.25),(111,120,1,0.25),(112,32,1,1),(112,118,1,1),(114,28,1,1),(114,33,1,1),(114,117,1,1),(116,46,1,1),(116,76,1,1),(116,101,1,1),(117,28,1,1),(117,33,1,1),(117,114,1,1),(118,32,1,1),(118,112,1,1),(120,12,1,0.25),(120,22,1,0.25),(120,30,1,0.25),(120,34,1,0.25),(120,45,1,0.25),(120,65,1,0.25),(120,66,1,0.25),(120,70,1,0.25),(120,73,1,0.25),(120,74,1,0.25),(120,99,1,0.25),(120,107,2,0.5),(120,111,1,0.25);
/*!40000 ALTER TABLE `associazioni` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `categorie`
--

DROP TABLE IF EXISTS `categorie`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categorie` (
  `nomeCategoria` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `id` int NOT NULL AUTO_INCREMENT,
  `imageUrl` text,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categorie`
--

LOCK TABLES `categorie` WRITE;
/*!40000 ALTER TABLE `categorie` DISABLE KEYS */;
INSERT INTO `categorie` VALUES ('Bar',1,'https://res.cloudinary.com/dii6jnk4o/image/upload/v1777665535/bar_gymqie.jpg'),('Sushi',2,'https://res.cloudinary.com/dii6jnk4o/image/upload/v1777665541/sushi_t88csi.jpg'),('Hamburger',3,'https://res.cloudinary.com/dii6jnk4o/image/upload/v1777665544/hamburger_bpa0fa.jpg'),('Pizza',4,'https://res.cloudinary.com/dii6jnk4o/image/upload/v1777665544/pizza_rlukfe.jpg'),('Primi Piatti',5,'https://res.cloudinary.com/dii6jnk4o/image/upload/v1777665544/primi_piatti_z6azq1.jpg'),('Secondi Piatti',6,'https://res.cloudinary.com/dii6jnk4o/image/upload/v1777665542/secondi_piatti_rwqcmq.jpg'),('Antipasti',7,'https://res.cloudinary.com/dii6jnk4o/image/upload/v1777665537/antipasti_lvf0uk.jpg'),('Dolci',8,'https://res.cloudinary.com/dii6jnk4o/image/upload/v1777665537/dolci_de3bna.jpg'),('Bevande',9,'https://res.cloudinary.com/dii6jnk4o/image/upload/v1777665537/bevande_l3m4u4.jpg'),('Contorni',10,'https://res.cloudinary.com/dii6jnk4o/image/upload/v1777665544/contorni_pysclt.jpg');
/*!40000 ALTER TABLE `categorie` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ordini`
--

DROP TABLE IF EXISTS `ordini`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ordini` (
  `id` int NOT NULL AUTO_INCREMENT,
  `data` datetime NOT NULL,
  `importo_totale` double NOT NULL,
  `idUtente` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `ordini_utenti_FK` (`idUtente`),
  CONSTRAINT `ordini_utenti_FK` FOREIGN KEY (`idUtente`) REFERENCES `utenti` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=58 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ordini`
--

LOCK TABLES `ordini` WRITE;
/*!40000 ALTER TABLE `ordini` DISABLE KEYS */;
INSERT INTO `ordini` VALUES (19,'2026-05-10 22:47:48',25.5,1),(20,'2026-05-10 22:49:18',48,1),(21,'2026-05-10 22:50:45',71,1),(22,'2026-05-10 22:51:18',18.5,1),(23,'2026-05-10 22:51:57',6.2,1),(24,'2026-05-10 22:52:47',15.5,2),(25,'2026-05-10 22:55:36',14.2,2),(26,'2026-05-10 22:56:12',18,2),(27,'2026-05-10 22:57:26',22,2),(28,'2026-05-10 22:58:28',25,2),(29,'2026-05-10 22:59:12',7,3),(30,'2026-05-10 23:00:06',31.5,3),(31,'2026-05-10 23:00:33',19,3),(32,'2026-05-10 23:01:09',16,3),(33,'2026-05-10 23:01:46',14.5,3),(34,'2026-05-10 23:02:28',5.3,4),(35,'2026-05-10 23:02:52',14.5,4),(36,'2026-05-10 23:03:17',16.5,4),(37,'2026-05-10 23:03:50',19,4),(38,'2026-05-10 23:04:47',46.5,4),(39,'2026-05-10 23:05:34',18,5),(40,'2026-05-10 23:06:11',23,5),(41,'2026-05-10 23:06:52',16.5,5),(42,'2026-05-10 23:07:15',25.5,5),(43,'2026-05-10 23:08:29',26,6),(44,'2026-05-10 23:09:08',12.5,6),(45,'2026-05-10 23:09:41',27.5,6),(46,'2026-05-10 23:10:34',28,7),(47,'2026-05-10 23:10:53',33.5,7),(48,'2026-05-10 23:11:22',18.5,7),(49,'2026-05-10 23:11:52',13.5,7),(50,'2026-05-10 23:12:23',6.8,7),(51,'2026-05-10 23:12:43',15.5,7),(52,'2026-05-10 23:13:45',28.5,8),(53,'2026-05-10 23:14:00',4.6,8),(54,'2026-05-10 23:14:40',33.5,8),(55,'2026-05-10 23:15:34',22,9),(56,'2026-05-10 23:16:30',13.5,10),(57,'2026-05-10 23:17:10',52.5,10);
/*!40000 ALTER TABLE `ordini` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `righe_dettaglio`
--

DROP TABLE IF EXISTS `righe_dettaglio`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `righe_dettaglio` (
  `id_ordine` int NOT NULL,
  `id_articolo` int NOT NULL,
  `quantita` int NOT NULL,
  `prezzo` double NOT NULL,
  KEY `riga_dettaglio_ordini_FK` (`id_ordine`),
  KEY `riga_dettaglio_articoli_FK` (`id_articolo`),
  CONSTRAINT `riga_dettaglio_articoli_FK` FOREIGN KEY (`id_articolo`) REFERENCES `articoli` (`id`) ON DELETE CASCADE,
  CONSTRAINT `riga_dettaglio_ordini_FK` FOREIGN KEY (`id_ordine`) REFERENCES `ordini` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `righe_dettaglio`
--

LOCK TABLES `righe_dettaglio` WRITE;
/*!40000 ALTER TABLE `righe_dettaglio` DISABLE KEYS */;
INSERT INTO `righe_dettaglio` VALUES (19,102,1,13),(19,42,1,4),(19,76,1,4),(19,46,1,1),(19,111,1,3.5),(20,107,1,4),(20,22,1,7.5),(20,66,1,16),(20,120,1,2.5),(20,73,3,15),(20,45,1,3),(21,87,1,8),(21,10,4,32),(21,85,1,12),(21,86,1,10.5),(21,55,1,6),(21,46,1,1),(21,51,1,1.5),(22,95,1,9),(22,37,1,4.5),(22,48,2,5),(23,1,1,1.5),(23,2,1,1.2),(23,109,1,3.5),(24,16,1,5),(24,48,1,2.5),(24,40,2,8),(25,1,1,1.5),(25,2,1,1.2),(25,109,2,7),(25,72,1,4.5),(26,90,1,10),(26,48,1,2.5),(26,38,1,5.5),(27,32,1,9),(27,35,1,6.5),(27,79,1,3),(27,39,1,3.5),(28,28,1,8.5),(28,33,1,5.5),(28,114,1,5),(28,117,1,6),(29,83,1,3.5),(29,109,1,3.5),(30,88,1,13),(30,108,1,9),(30,56,1,3),(30,7,1,6.5),(31,118,1,6),(31,32,1,9),(31,112,1,4),(32,96,1,10),(32,36,1,5),(32,46,1,1),(33,12,1,8),(33,120,1,2.5),(33,107,1,4),(34,81,1,1.3),(34,40,1,4),(35,105,1,5.5),(35,25,1,6.5),(35,80,1,2.5),(36,57,1,10.5),(36,48,1,2.5),(36,41,1,3.5),(37,68,1,10),(37,100,1,8),(37,47,1,1),(38,17,1,6.5),(38,18,1,7),(38,94,1,11),(38,60,1,7),(38,48,4,10),(38,46,1,1),(38,42,1,4),(39,27,1,9),(39,44,1,4),(39,75,1,4),(39,46,1,1),(40,49,1,4),(40,32,1,9),(40,33,1,5.5),(40,31,1,4.5),(41,14,1,7),(41,48,1,2.5),(41,39,1,3.5),(41,41,1,3.5),(42,54,1,4.5),(42,10,1,8),(42,85,1,12),(42,46,1,1),(43,34,1,8),(43,65,1,12),(43,111,1,3.5),(43,120,1,2.5),(44,21,1,6),(44,44,1,4),(44,48,1,2.5),(45,60,1,7),(45,20,1,7),(45,19,1,7.5),(45,48,2,5),(45,46,1,1),(46,101,1,18),(46,46,1,1),(46,76,1,4),(46,116,1,5),(47,49,7,28),(47,33,1,5.5),(48,29,1,11),(48,45,1,3),(48,106,1,4.5),(49,111,1,3.5),(49,110,1,4.5),(49,80,1,2.5),(49,79,1,3),(50,4,1,1.1),(50,2,1,1.2),(50,72,1,4.5),(51,89,1,13),(51,48,1,2.5),(52,8,1,4.5),(52,7,1,6.5),(52,55,1,6),(52,108,1,9),(52,46,1,1),(52,51,1,1.5),(53,4,1,1.1),(53,111,1,3.5),(54,70,1,8),(54,99,1,9),(54,30,1,10),(54,120,1,2.5),(54,74,1,4),(55,69,1,5),(55,34,1,8),(55,37,2,9),(56,11,1,7.5),(56,5,1,2.5),(56,41,1,3.5),(57,104,2,24),(57,26,1,12),(57,42,1,4),(57,43,1,4.5),(57,48,1,2.5),(57,38,1,5.5);
/*!40000 ALTER TABLE `righe_dettaglio` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `aumentaNumeroOrdiniArticolo` AFTER INSERT ON `righe_dettaglio` FOR EACH ROW begin
	update articoli
	set numero_ordini = numero_ordini + 1
	where id = new.id_articolo;
end */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `aggiornaAssociazioni` AFTER INSERT ON `righe_dettaglio` FOR EACH ROW begin
	
	
    UPDATE associazioni a
    JOIN righe_dettaglio rd
        ON rd.id_ordine = NEW.id_ordine
    SET a.numero_ordini = a.numero_ordini + 1
    WHERE rd.id_articolo = a.id_articolo1
      AND NEW.id_articolo = a.id_articolo2;

    
    UPDATE associazioni a
    JOIN righe_dettaglio rd
        ON rd.id_ordine = NEW.id_ordine
    SET a.numero_ordini = a.numero_ordini + 1
    WHERE rd.id_articolo = a.id_articolo2
      AND NEW.id_articolo = a.id_articolo1;
	
end */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `utenti`
--

DROP TABLE IF EXISTS `utenti`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `utenti` (
  `nome` text NOT NULL,
  `telefono` text NOT NULL,
  `password` text NOT NULL,
  `id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL,
  `indirizzo` text NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `utenti_unique` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `utenti`
--

LOCK TABLES `utenti` WRITE;
/*!40000 ALTER TABLE `utenti` DISABLE KEYS */;
INSERT INTO `utenti` VALUES ('Stefania Agapi','123456789','infostefi',1,'stefi_','Via Roma 42'),('Marco Garbin','123456789','password123',2,'marco_garbin','Via Marcoai 4'),('Mario Rossi','3331234567','mario123',3,'mrossi','Via Roma 12'),('Giulia Bianchi','3409876543','giulia456',4,'gbianchi','Corso Francia 45'),('Luca Verdi','3475551122','lverdi',5,'lucaverdi','Via Po 18'),('Sara Colombo','3294447788','sara789',6,'scolombo','Piazza Castello 7'),('Andrea Ferri','3512223344','andreapwd',7,'aferri','Via Garibaldi 30'),('Elena Russo','3486669911','elena2025',8,'erusso','Corso Vittorio 80'),('Davide Gallo','3357778899','davidepass',9,'dgallo','Via Nizza 120'),('Chiara Moretti','3311112233','chiara321',10,'cmoretti','Via Madama Cristina 55');
/*!40000 ALTER TABLE `utenti` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'deliveroo'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-05-10 23:18:26
