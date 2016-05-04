Feature: DeckCardIntegrationFeature
	Integration feature for Deck and Card

@appendDeck
Scenario: Append two decks
	Given I have 2 decks
	And deck 0 gets a Bank
	And deck 0 gets a Army
	And deck 1 gets a CIS
	And deck 1 gets a Bank
	When deck 1 is appended to deck 0
	Then deck 0 has 4 cards
	And deck 0 has a Bank
	And deck 0 has a Army
	And deck 0 has a CIS
	And deck 0 has a Bank
	

