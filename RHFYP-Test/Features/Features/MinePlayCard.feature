Feature: MinePlayCard
	The action that should be taken
	When the mine card is played

@playcard @mine
Scenario: Does nothing case
	Given I have a game
	And the game has 2 players
	And player 0 is in Action mode
	And player 0 has a Mine card in their hand
	And player 0 doesnt have a Small Business in their hand
	And player 0 doesnt have a Company in their hand
	And x is the number of cards player 0 has
	And player 0 has managers
	When player 0 plays the Mine card
	Then x - 1 is the number of cards player 0 has


@playcard @mine
Scenario: Upgrades a Small Business
	Given I have a game
	And the game has 2 players
    And player 0 is in Action mode
	And player 0 has a Mine card in their hand
	And player 0 has a Small Business in their hand
	And player 0 doesnt have a Company in their hand
    And x is the number of cards player 0 has
	And player 0 has managers
	When player 0 plays the Mine card
	Then player 0 has a Company card in their hand
	And x - 1 is the number of cards player 0 has

@playcard @mine
Scenario: Upgrades a Company
	Given I have a game
	And the game has 2 players
    And player 0 is in Action mode
	And player 0 has a Mine card in their hand
	And player 0 has a Company in their hand
	And player 0 doesnt have a Small Business in their hand
	And player 0 doesnt have a Corporation in their hand
    And x is the number of cards player 0 has
	And player 0 has managers
	When player 0 plays the Mine card
	Then player 0 has a Corporation card in their hand
	And x - 1 is the number of cards player 0 has

@playcard @mine
Scenario: Player does not have managers
	Given I have a game
	And the game has 2 players
    And player 0 is in Action mode
	And player 0 has a Mine card in their hand
	And player 0 has a Company in their hand
	And player 0 does not have managers
	Then player 0 cant play the Mine card




