Feature: StartUpPlayCard
	When a player plays a start up card
	they lose the start up and get to buy something costing up to five

@mytag
Scenario: When the player already has coupon coins cant be played
	Given I have a game
	And the game has 2 players
	And player 0 has more than 0 coupons
	And player 0 has a StartUp card
	Then player 0 cant play the StartUp card
