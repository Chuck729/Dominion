Feature: StartUpPlayCard
	When a player plays a start up card
	they lose the start up and get to buy something costing up to five

@startup @playcard
Scenario: When the player already has coupon coins cant be played
	Given I have a game
	And the game has 2 players
	And player 0 has more than 0 coupons
	And player 0 has a StartUp card
	Then player 0 cant play the StartUp card

@startup @playcard
Scenario: When the player plays the card they have 5 coupons
	Given I have a game
	And the game has 2 players
	And player 0 has 0 coupons
	And player 0 has a StartUp card
	When player 0 plays the StartUp card
	Then player 0 has 5 coupons

@startup @playcard
Scenario: When the player plays the card they no longer have it
	Given I have a game
	And the game has 2 players
	And player 0 has 0 coupons
	And player 0 has 1 Managers
	And player 0 has a StartUp card
	When player 0 plays the StartUp card
    Then player 0 has 5 coupons
	And player 0 has no StartUp cards
