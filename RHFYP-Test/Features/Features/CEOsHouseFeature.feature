Feature: CEOsHouseFeature
	As a player, you should be able to play a 
	CEOsHouse card

@CEOsHouse
Scenario: Play CEOsHouse
	Given I have a game
	And the game has 2 players
	And player 0 has 0 gold
	And player 0 has a CEOsHouse in their hand
	And player 0 has a Company in their hand
	And player 0 has 1 managers
	When player 0 plays the CEOsHouse card
	And player 0 plays the Company card
	Then player 0 has 4 gold
