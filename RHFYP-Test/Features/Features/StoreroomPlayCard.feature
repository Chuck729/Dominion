Feature: StoreroomPlayCard
	When a player plays the storeroom
	the players get +2 managers
	and +2 gold
	and +1 investment

@mytag
Scenario: Play storeroom
	Given I have a player
	And the player has 1 Storeroom card
	And the player has 0 Gold
	And the player has 0 Investments
	And the player has 0 Managers
	When the player plays the Storeroom card
	Then the player has 2 Gold
	And the player has 1 Investments
	And the player has 2 Managers
