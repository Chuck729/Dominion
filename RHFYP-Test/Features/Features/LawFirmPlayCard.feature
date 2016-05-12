Feature: LawFirmPlayCard
	When a player plays the law firm card
	They get +1 investment
	And +2 gold

@lawfirm @playcard
Scenario: Play law firm
	Given I have a player
	And the player has 1 LawFirm cards
	And the player has 0 Gold
	And the player has 0 Investments
	And the player has 0 Managers
	When the player plays the LawFirm card
	Then the player has 2 Gold
	And the player has 1 Investments
	And the player has 0 Managers
