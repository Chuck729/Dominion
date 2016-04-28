Feature: MinePlayCard
	The action that should be taken
	When the mine card is played

@playcard @mine
Scenario: Add two numbers
	Given I have a game
	And the game has 2 players
	And player 1 has a Mine card in thier hand
	And player 1 doesnt have a Small Business in their hand
	And player 1 doesnt have a Company in their hand
	Then player 1 cant play the Mine card

@playcard @mine
Scenario: Add two numbers
	Given I have a game
	And the game has 2 players
	And player 1 has a Mine card in their hand
	And player 1 has a Small Business in their hand
	And player 1 plays their Mine card
	Then player 1 doesnt have a Small Business in their hand
	And 




