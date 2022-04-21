//Feedback
/*
 * Op dit moment heb je nog erg veel code in de Agent staan, terwijl je daar nou juist de Behaviour Tree voor kunt gebruiken. 
 * Ook beschouw je je nodes nu meer als een soort states van een stateMachine, terwijl je ze eigenlijk heel generiek op wilt stellen.
 * 
Stel dat je bijv. de ChasePlayer "state" wilt maken dan kunnen deze state opsplitsten in meerdere lossen nodes: CanSeeObject, MoveToObject, ConditionCloseToObject, AttackObject. 
Zoals je ziet zijn deze nodes context vrij dus er is geen weet van een player, dit geef je namelijk mee met de node zelf. 
Je krijgt dan een sequence: Sequence (CanSeeObject(Player), MoveToObject, ConditionCloseToObject, AttackObject).

Ook is het belangrijk om te weten dat de sequence zoals in het voorbeeld (en ook in jouw code) op dit moment elk keer opnieuw start vanaf het begin van de sequence. 
In de les heb ik het voorbeeld laten zien dat je ook verder kan gaan bij de Running node door de index op te slaan van de laatste gerunde node.
ipv een foreach-loop krijg je dan een for-loop in de sequence waarbij je itererende variabele start bij de laatstrunnende node.

Om te switchen van state kun je een Selector gebruiken of een eigen custom Selector (bijvoorbeeld een StateSelector die een stuk gedrag kiest aan de hand van een enumerator).

Ik raad sterk aan om eens te kijken naar de documentatie van bijv. NodeCanvas (een behaviour Tree op de Asset store): https://nodecanvas.paradoxnotion.com/documentation/Links to an external site.

Hier staat veel informatie over hoe een BehaviourTree (zou moeten) werken en welke nodes er zoal zijn.

Probeer voor de herkansing je code echt generieker te schrijven en context onafhankelijk, mocht je hier hulp mee willen, laat het me weten.
*/
