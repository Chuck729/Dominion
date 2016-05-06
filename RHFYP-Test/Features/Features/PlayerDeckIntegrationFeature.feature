Feature: PlayerDeckIntegrationFeature
	Integration feature for Deck and Card

@mytag
Scenario: Integration test play all treasures
	Given I have 1 players
	And player 0 has a Corporation in their hand
	And player 0 has a SmallBusiness in their hand
	And player 0 has a Bank in their hand
	When palyer 0 plays all treaures
	Then player 0 has 1 card in their hand
	And player 0 has a Bank in their hand
