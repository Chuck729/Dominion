Feature: BankPlayCard
	Draw two treasure cards.

@mytag
Scenario: No treasure cards
	Given I have a player
	And the player has 1 Bank cards
	And the player has 5 Apartment cards in hand
	When the player plays the Bank card
	Then the player has 0 cards in hand
