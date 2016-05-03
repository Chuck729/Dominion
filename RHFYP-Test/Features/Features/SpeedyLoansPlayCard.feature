Feature: SpeedyLoansPlayCard
	As a player when I have a SpeedyLoans card in my hand
	And I have a small business in my hand And I play the card
	Then the Small Business card should be put in the trash pile 
	And I gain 3 gold

@playcard @SpeedyLoans
Scenario: SpeedyLoans and Small business cards in hand
	Given I have a game
	And the game has 2 players
	And player 1 has a SpeedyLoans in thier hand
	And player 1 has a Small Business in their hand
	And player 1 has managers
	And player 1 is in Action mode
	When player 1 plays the SpeedyLoans card
	Then player 1 small business card is put in the trash pile
	And player 1 gains 3 gold
	And player 1 SpeedyLoans is discarded
