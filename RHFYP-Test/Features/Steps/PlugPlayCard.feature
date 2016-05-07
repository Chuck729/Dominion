Feature: PlugPlayCard
	Every other player in the game gets a hippie camp
	The player who plays this gets +2 cards

@plug @playcard
Scenario: Playing the card without cards to draw
	Given I have a game
	And the game has 2 players
    And player 0 has no cards to draw
	And player 0 has a Plug card
	When player 0 plays the Plug card
	Then player 1 has 1 Hippie Camp cards
	And player 0 has 0 Hippie Camp cards
	And player 0 has 1 cards in hand
