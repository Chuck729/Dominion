Feature: PlugPlayCard
	Every other player in the game gets a hippie camp
	The player who plays this gets +2 cards

@plug @playcard
Scenario: Playing the card without hippie camp cards in buy deck
	Given I have a game
	And the game has 2 players
    And player 0 has no cards to draw
	And player 0 has a Plug card
	When player 0 plays the Plug card
	Then player 1 has 0 of Hippie Camp cards
	And player 0 has 0 of Hippie Camp cards

@plug @playcard
Scenario: Playing the card with cards to draw
	Given I have a game
	And the game has 2 players
	And player 0 has a Plug card
    And player 0 has 0 cards in hand
    And player 0 has 2 cards in draw pile
	When player 0 plays the Plug card
	Then player 0 has 2 cards in hand