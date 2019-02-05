# Note
Documentation should not make room for discussions, since this is part of an interview process this documentation will also include this.

# Topics and queues used in RabbitMQ:
- SearchType
- ListMultType
- ListAllLegendary
- SearchName 
- ListHeaders
- SearchHeader 
- Battle 

# Endpoints for the API
[GET] /api/searchtype/{Type}
[GET] /api/listmulttype/
[GET] /api/listalllegendary/
[GET] /api/searchname/{Name}
[GET] /api/listheaders/
[GET] /api/searchheader/{Header}/{Value}
[GET] /api/battle/{PokemonAName}/{PokemonBName}

# The Battle System
The battle system takes the names for two pokemons, tries to retrieve them in the pokemon db and lets them fight.

If no pokemons are found, the system returns "no contest". If only one pokemon is found the other pokemon wins on no show.

The system could iterate over the amount of rounds and for each round determine the life of each pokemon and issue a winner.

The runtime of the battle system will thus be dependent on the amount of rounds. It is not unthinkable that stronger pokemons can fight much more than 8 rounds.

We can do better.

Instead it is calculated how many rounds it takes for each pokemon to win. EnemyHP / (Attack-EnemyDefense).

The pokemon with lowest amount of rounds to win, will be the winner. If the amount of rounds to win is equal, the fastest win. If they have same speed, it's a tie. (This can be discussed)

But we do have to take care of some additional special cases.

If it takes more than 8 rounds for both pokemons to win. The amount of damage done over 8 rounds must be calculated and subtracted from the pokemons hp. Highest HP left wins. It is a tie if they have the same amount of HP left. (This can be discussed)

It can be discussed if the amount of defense is larger than the attack of the opponent, if the attack will heal the attacked pokemon. As of the current implementation it is corrected to do 0 damage, since this is the most sensible solution. But negative damage could of course be introduced.

# Tests
Unit tests contained in project PokeBackend.Tests and integration test done using soapUI in configuration REST-POKEMON-SOAPUI.xml.

# Future work
- Currently the webservice returns json wrapped in xml, this might not be what most people want.
- Caching in webservice calls containing just a list. I.e. type search cannot easily be cached due to many different types might be searched. Could be worthwile if most people search same types or functiality is heavily used.
- Search for headers could be improved by introducing a dictionary to avoid the current very verbose solution.
- Searching pokemon by name is case-sensitive. An case-insensitive solution is also provided in a comment. This also affect the battle command.
- Client creates and closes the connection for each call. Depending on usage of the client this might not be optimal.
- Support for Docker