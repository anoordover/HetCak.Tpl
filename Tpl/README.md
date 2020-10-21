# Aanleiding

In het component BRI (Broninhouding) moeten wij in een bluk-verwerking
een inhoudingsbestand aanmaken.
Bij het aanmaken van dit bestand moeten wij persoon-identificaties omzetten in BSN's.
Om optimaal gebruik te maken van resources zouden we deze omzettingen parallel willen doen.
Aandachtspunt hierbij is echter dat de methode die de omzetting naar de BSN doet een Task
teruggeeft.
Uitgangspunt van de aanpak was:
- Omzettingen van persoon-id naar BSN moet parallel worden gedaan;
- Er moet optimaal gebruik gemaakt worden van de thread-pool.

# Aanpak 1
- Bouw een lijst op van om te zetten persoon-id's;
- Roep met een AsParallel.ForAll constructie de BSN omzetting aan;
- Doe een async/await op die BSN omzetting.

 ## Conclusie
 Na afronding van de AsParallel zijn er blijkbaar 10 threads gestart die allemaal nog lopen.
 Deze komen pas in de Sleep tot afronding.
 Deze manier van deze functionaliteit parallel uitvoeren werkt niet goed.
 
 # Aanpak 2
 Bij deze oplossing is ervoor gekozen om binnen de ForAll ook het resultaat van de task
 op te vragen.
 Hierdoor wordt de lopende thread geblokkeerd en komt de logging wel beschikbaar tijdens de uitvoering van de ForAll.
 
 ## Conclusie
 De oplossing levert en werkende oplossing.
 De keerzijde is echter dat er threads staan te wachten op een resultaat.
 Er wordt dus niet optimaal gebruik gemaakt van de thead-pool.
 
 # Wat dan wel?
 Naar aanleiding van bovenstaande realiseerde ik me dat ik ook weleens wat had gelezen
 over TPL Dataflow.
 Hiervoor het ik twee TplDemo's gemaakt TplDemo1 en TplDemo2 zodat ik kon ervaren of het
 hiermee wel mogelijk is om parallel asynchrone code uit te voeren waarbij een vereiste
 is dat het aantal asynchrone instanties niet te groot wordt.
 
 
 # Te onderzoeken en uit te werken
 Eerst moet natuurlijk worden bepaald of we deze programmeer oplossing willen
 toepassen in onze code.
 Als we tot de conclusie komen dat we dit willen doen is het naar mijn gevoel verstandig om:
 - Best-practices te definieren voor wat betreft de inzet van DataFlow;
 - Onderzoeken of vanuit een ActionBlock dezelfde DB-context gebruikt kan worden als
 de DB-context die in de aanroepende methode beschikbaar is;
 - ...
 
**Graag aanvullen :)
