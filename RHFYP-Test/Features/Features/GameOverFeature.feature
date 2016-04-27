Feature: GameOverFeature
	When the game should end
	And what should happen when it's over

@mytag
Scenario: AllRoseHulmansBought
	Given I have a game
	And the game has 2 players
	And player 0 has a Purdue card
	And there are no Rose-Hulman cards left in the buy deck
	And its the end of someones turn
	Then the game should be over
	And player 0 should win
