Feature: WallStreetPlayCard
	When a wall streed card is played
	the player who played it should get 2 coins
	And all the cards in thier hand should be put on the the top of thier draw pile

@playcard @wallstreet
Scenario: Normal game the player discards onto draw pile
	Given I have a game
	And the game has 2 players
	And player 0 has 1 Manager
	And player 0 has a Wall Street card
	And player 0 has x cards in thier hand
	When player 0 plays the Wall Street card
	And player 0 ends thier turn
	Then x cards are all on the top of player 0 draw pile
