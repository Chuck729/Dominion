Feature: SubdivisionPlayCard
	When a playerplays a subdivision card
	Then the player should gain 1 manager
	And the the player should draw 4 cards
	And all of the other players in the game should draw a card

@subdivision @playcard
Scenario: Subdivision is played and player gets 1 manager
	Given I have a game
	And the game has 2 players
	And player 0 has 0 managers
	And player 0 has a Subdivision
	When player 0 plays the Subdivision
	Then player 0 has 1 managers

@subdivision @playcard
Scenario: Subdivision is played and player gets 4 cards
	Given I have a game
	And the game has 2 players
	And player 0 has a Subdivision
	And x is the number of cards player 0 has
	When player 0 plays the Subdivision
	Then player 0 has 10 cards in hand

@subdivision @playcard
Scenario: Subdivision is played and all other players get 1 card
	Given I have a game
	And the game has 2 players
	And player 0 has a Subdivision
	And x is the number of cards player 0 has
	When player 0 plays the Subdivision
	Then player 1 has 6 cards in hand
