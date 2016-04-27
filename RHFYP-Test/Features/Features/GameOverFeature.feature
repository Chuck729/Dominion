Feature: GameOverFeature
	When the game should end
	And what should happen when it's over

@gameover @win
Scenario: AllRoseHulmansBought
	Given I have a game
	And the game has 2 players
	And player 0 has a Purdue card
	And there are no Rose-Hulman cards left in the buy deck
	And its the end of someones turn
	Then the game should be over
	And player 0 should win

@gameover @win
Scenario: ThreeBuyPilesEmpty
	Given I have a game
	And the game has 2 players
	And player 1 has a Purdue card
	And three buy deck piles are empty
	And its the end of someones turn
	Then the game should be over
	And player 1 should win
