Feature: GardensPlayCardFeature
	The gardens card should give +1 vp
	for evry 10 tiles on map (rounded down)

@playcard @gardens
Scenario: 9 cards in deck
	Given I have a player
	And the player has 8 Corperation cards
	And the player has 1 Gardens cards
	And the player has a trash deck
    When the player has 1 victory cards
	Then the player should have 0 victory points
