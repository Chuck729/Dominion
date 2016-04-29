Feature: MinePlayCard
	The action that should be taken
	When the mine card is played

@playcard @mine
Scenario: Does nothing case
	Given I have a game
	And the game has 2 players
	And player 1 is in Action mode
	And player 1 has a Mine card in their hand
	And player 1 doesnt have a Small Business in their hand
	And player 1 doesnt have a Company in their hand
	And x is the number of cards player 1 has
	When player 1 plays the Mine card
	Then x - 1 is the number of cards player 1 has


#@playcard @mine
#Scenario: Add two numbers
#	Given I have a game
#	And the game has 2 players
#	And player 1 has a Mine card in their hand
#	And player 1 has a Small Business in their hand
#	And player 1 plays their Mine card
#	When player 1 plays a Mine card
#	Then player 1 doesnt have a Small Business in their hand
#	And player 1 has a Corperaton in thier hand




