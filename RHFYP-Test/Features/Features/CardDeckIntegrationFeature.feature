Feature: DeckCardIntegrationFeature
	Integration feature for Deck and Card

@appendDeck
Scenario: Integration Append two decks
	Given I have 2 decks
	And deck 0 gets an action card Bank
	And deck 0 gets an action card Army
	And deck 1 gets an action card CIS
	And deck 1 gets an action card Bank
	When deck 1 is appended to deck 0
	Then deck 0 has 4 cards
	And deck 0 has a Bank
	And deck 0 has a Army
	And deck 0 has a CIS
	And deck 0 has a Bank
	
@shuffleIn
Scenario: Integration Shuffle in
	Given I have 2 decks
	And deck 0 gets an action card Bank
	And deck 0 gets an action card Army
	And deck 1 gets an action card CIS
	And deck 1 gets an action card Bank
	When deck 1 is shuffled into deck 0
	Then deck 0 has 4 cards
	And deck 1 has 0 cards
	And deck 0 has a Bank
	And deck 0 has a Army
	And deck 0 has a CIS
	And deck 0 has a Bank

