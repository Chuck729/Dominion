Feature: MilitaryBasePlayCard
	When a player plays the military base card
	They should get 2 cards

@mytag
Scenario: Play military base 0 cards to draw
	Given I have a player
	And the player has 0 cards in hand
	And the player has 0 cards in draw pile
	And the player has 1 MilitaryBase
	When the player plays the MilitaryBase
	Then the player has 1 cards in hand

@mytag
Scenario: Play military base 1 cards to draw
	Given I have a player
	And the player has 0 cards in hand
	And the player has 1 cards in draw pile
	And the player has 1 MilitaryBase
	When the player plays the MilitaryBase
	Then the player has 2 cards in hand

@mytag
Scenario: Play military base 2 cards to draw
	Given I have a player
	And the player has 0 cards in hand
	And the player has 2 cards in draw pile
	And the player has 1 MilitaryBase
	When the player plays the MilitaryBase
	Then the player has 2 cards in hand

@mytag
Scenario: Play military base 3 cards to draw
	Given I have a player
	And the player has 0 cards in hand
	And the player has 3 cards in draw pile
	And the player has 1 MilitaryBase
	When the player plays the MilitaryBase
	Then the player has 2 cards in hand

