Feature: BankPlayCard
	Draw two treasure cards.

@bank @playcard
Scenario: No treasure cards
	Given I have a player
	And the player has 1 Bank cards
	And the player has 5 Apartment cards
	When the player plays the Bank card
	Then the player has 0 cards in hand

@bank @playcard
Scenario: 1 treasure cards
	Given I have a player
	And the player has 1 Bank cards
	And the player has 1 Small Business cards
	And the player has 5 Apartment cards
	When the player plays the Bank card
	Then the player has 1 cards in hand
	And draw the card, the card is a treasure

@bank @playcard
Scenario: 2 treasure cards
	Given I have a player
	And the player has 1 Bank cards
	And the player has 2 Small Business cards
	And the player has 5 Apartment cards
	When the player plays the Bank card
	Then the player has 2 cards in hand
	And draw the card, the card is a treasure
	And draw the card, the card is a treasure

@bank @playcard
Scenario: 3 treasure cards
	Given I have a player
	And the player has 1 Bank cards
	And the player has 3 Small Business cards
	And the player has 5 Apartment cards
	When the player plays the Bank card
	Then the player has 2 cards in hand
	And draw the card, the card is a treasure
	And draw the card, the card is a treasure
