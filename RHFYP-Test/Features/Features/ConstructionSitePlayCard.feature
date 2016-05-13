Feature: ConstructionSitePlayCard
	When a player plays a construction site card
	they lose the start up and get to buy something costing up to four

@constructionsite @playcard
Scenario: When the player already has coupon coins cant be played
	Given I have a game
	And the game has 2 players
	And player 0 has more than 0 coupons
	And player 0 has a ConstructionSite card
	Then player 0 cant play the ConstructionSite card

@constructionsite @playcard
Scenario: When the player plays the card they have 4 coupons
	Given I have a game
	And the game has 2 players
	And player 0 has 0 coupons
	And player 0 has a ConstructionSite card
	When player 0 plays the ConstructionSite card
	Then player 0 has 4 coupons

@constructionsite @playcard
Scenario: When the player plays the card they no longer have it
	Given I have a game
	And the game has 2 players
	And player 0 has 0 coupons
	And player 0 has 1 Managers
	And player 0 has a ConstructionSite card
	When player 0 plays the ConstructionSite card
    Then player 0 has 4 coupons
	And player 0 has no ConstructionSite cards