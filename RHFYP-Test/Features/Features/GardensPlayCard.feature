Feature: GardensPlayCardFeature
	The gardens card should give +1 vp
	for evry 10 tiles on map (rounded down)

@playcard @gardens
Scenario: 9 cards in deck
	Given I have player
	And the player is in buy mode
	And the player has 8 Corperation cards
	And the player has 1 Gardens cards
	And the player has a trash deck
    When the player has 1 victory cards
	And the players turn ends
	Then the player should have 0 victory points

@playcard @gardens
Scenario: 10 cards in deck
	Given I have player
	And the player is in buy mode
	And the player has 9 Corperation cards
	And the player has 1 Gardens cards
	And the player has a trash deck
    When the player has 1 victory cards
	And the players turn ends
	Then the player should have 1 victory points

@playcard @gardens
Scenario: 19 cards in deck
	Given I have player
	And the player is in buy mode
	And the player has 18 Corperation cards
	And the player has 1 Gardens cards
	And the player has a trash deck
    When the player has 1 victory cards
	And the players turn ends
	Then the player should have 1 victory points

@playcard @gardens
Scenario: 20 cards in deck
	Given I have player
	And the player is in buy mode
	And the player has 19 Corperation cards
	And the player has 1 Gardens cards
	And the player has a trash deck
    When the player has 1 victory cards
	And the players turn ends
	Then the player should have 2 victory points

@playcard @gardens
Scenario: 19 cards in deck and 2 gardens
	Given I have player
	And the player is in buy mode
	And the player has 17 Corperation cards
	And the player has 2 Gardens cards
	And the player has a trash deck
    When the player has 2 victory cards
	And the players turn ends
	Then the player should have 2 victory points

@playcard @gardens
Scenario: 20 cards in deck and 2 gardens
	Given I have player
	And the player is in buy mode
	And the player has 18 Corperation cards
	And the player has 2 Gardens cards
	And the player has a trash deck
    When the player has 2 victory cards
	And the players turn ends
	Then the player should have 4 victory points

@playcard @error handling
Scenario: Play Gardens no player given
	Given I have a game
	And there is a Gardens card in the game
	And the Gardens card is played without a player
	Then An ArgumentNullException is thrown

@playcard @error handling
Scenario: Play Gardens no game given
	Given I have a game
	And there is a Gardens card in the game
	And the Gardens card is played without a game
	Then An ArgumentNullException is thrown