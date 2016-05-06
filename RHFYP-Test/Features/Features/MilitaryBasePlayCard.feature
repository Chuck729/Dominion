Feature: MilitaryBasePlayCard
	When a player plays the military base card
	They should get 2 cards

@mytag
Scenario: Play military base
	Given I have a player
	And the player has 0 cards in hand
	And the player has 1 MilitaryBase
	When the player plays the MilitaryBase
	Then the player has 3 cards in hand

