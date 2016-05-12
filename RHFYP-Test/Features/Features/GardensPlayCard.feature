Feature: GardensPlayCardFeature
	The gardens card should give +1 vp
	for evry 10 tiles on map (rounded down)

@playcard @gardens
Scenario: 11 cards in deck (player initializes with 10 cards, 3 Purdues, 7 Small Businesses)
	Given I have a game
	And the game has 2 players
	And player 0 is in Buy mode
	And player 0 has 1 Gardens cards
	And player 0 has a trash deck
    When player 0 turn ends
	Then player 0 should have 4 victory cards
	And player 0 should have 4 victory points
	

@playcard @gardens
Scenario: 20 cards in deck
	Given I have a game
	And the game has 2 players
	And player 0 is in Buy mode
	And player 0 has 9 Corporation cards
	And player 0 has 1 Gardens cards
	And player 0 has a trash deck
    When player 0 turn ends
	Then player 0 should have 4 victory cards
	And player 0 should have 5 victory points

@playcard @gardens
Scenario: 19 cards in deck
	Given I have a game
	And the game has 2 players
	And player 0 is in Buy mode
	And player 0 has 8 Corporation cards
	And player 0 has 1 Gardens cards
	And player 0 has a trash deck
    When player 0 turn ends
	Then player 0 should have 4 victory cards
	And player 0 should have 4 victory points

@playcard @gardens
Scenario: 30 cards in deck
	Given I have a game
	And the game has 2 players
	And player 0 is in Buy mode
	And player 0 has 9 Corporation cards
	And player 0 has 10 Company cards
	And player 0 has 1 Gardens cards
	And player 0 has a trash deck
    When player 0 turn ends
	Then player 0 should have 4 victory cards
	And player 0 should have 6 victory points

@playcard @gardens
Scenario: 19 cards in deck and 2 gardens
	Given I have a game
	And the game has 2 players
	And player 0 is in Buy mode
	And player 0 has 7 Corporation cards
	And player 0 has 2 Gardens cards
	And player 0 has a trash deck
    When player 0 turn ends
	Then player 0 should have 5 victory cards
	And player 0 should have 5 victory points

@playcard @gardens
Scenario: 20 cards in deck and 2 gardens
	Given I have a game
	And the game has 2 players
	And player 0 is in Buy mode
	And player 0 has 8 Corporation cards
	And player 0 has 2 Gardens cards
	And player 0 has a trash deck
    When player 0 turn ends
	Then player 0 should have 5 victory cards
	And player 0 should have 7 victory points

@playcard @error handling
Scenario: Play Gardens no player given
	Given I have a game
	And there is a Gardens card in the game
	When the Gardens card is played without a player
	Then An ArgumentNullException is thrown

@playcard @error handling
Scenario: Play Gardens no game given
	Given I have a game
	And there is a Gardens card in the game
	When the Gardens card is played without a game
	Then An ArgumentNullException is thrown