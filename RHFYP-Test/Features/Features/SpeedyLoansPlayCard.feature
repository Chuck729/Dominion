Feature: SpeedyLoansPlayCard
	As a player when I have a SpeedyLoans card in my hand
	And I have a small business in my hand And I play the card
	Then the Small Business card should be put in the trash pile 
	And I gain 3 gold

@playcard @SpeedyLoans
Scenario: SpeedyLoans and Small business card in hand
	Given I have a game
	And the game has 2 players
	And player 0 has a SpeedyLoans in their hand
	And player 0 has a Small Business in their hand
	And player 0 has managers
	And player 0 is in Action mode
	And player 0 has 0 gold
	When player 0 plays the SpeedyLoans card
	Then player 0 Small Business card is put in the trash pile
	And player 0 has 3 gold
	And player 0 SpeedyLoans is discarded

@playcard @SpeedyLoans
Scenario: SpeedyLoans no small business in hand
	Given I have a game
	And the game has 2 players
	And player 0 has a SpeedyLoans in their hand
	And player 0 does not have a Small Business in their hand
	And player 0 has managers
	And player 0 is in Action mode
	And player 0 has 0 gold
	When player 0 plays the SpeedyLoans card
	Then player 0 SpeedyLoans is discarded
	And player 0 has 0 gold

@playcard @SpeedyLoans
Scenario: SpeedyLoans no player given
	Given I have a game
	And there is a SpeedyLoans card in the game
	And the SpeedyLoans card is played without a player
	Then an ArgumentNullException is thrown

@playcard @SpeedyLoans
Scenario: SpeedyLoans no game given
	Given I have a game
	And the game has 2 players
	And there is a SpeedyLoans card in the game
	And the SpeedyLoans card is played without a game
	Then an ArgumentNullException is thrown
