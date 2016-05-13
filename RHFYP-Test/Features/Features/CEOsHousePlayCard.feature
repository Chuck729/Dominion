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
	Then player 0 has 2 gold

Scenario: Play CEOsHouse two plays after
	Given I have a game
	And the game has 2 players
	And player 0 has 0 gold
	And player 0 has a CEOsHouse in their hand
	And player 0 has a Company in their hand
	And player 0 has a Small Business in their hand
	And player 0 has 2 managers
	When player 0 plays the CEOsHouse card
	And player 0 plays the Company card
	And player 0 plays the SmallBusiness card
	Then player 0 has 3 gold

Scenario: Play CEOsHouse with Action
	Given I have a game
	And the game has 2 players
	And player 0 has 0 gold
	And player 0 has a CEOsHouse in their hand
	And player 0 has 1 managers
	And player 0 has a Apartment in their hand
	And player 0 has 2 managers
	When player 0 plays the CEOsHouse card
	And player 0 plays the Apartment card
	Then player 0 has 4 managers left

Scenario: Play CEOsHouse with 2 Action
	Given I have a game
	And the game has 2 players
	And player 0 has 0 gold
	And player 0 has a CEOsHouse in their hand
	And player 0 has 1 managers
	And player 0 has a Apartment in their hand
	And player 0 has a Apartment in their hand
	And player 0 has 2 managers
	When player 0 plays the CEOsHouse card
	And player 0 plays the Apartment card
	And player 0 plays the Apartment card
	Then player 0 has 5 managers left

@mytag @error handling
Scenario: Play CEOsHouse no player given
	Given I have a game
	And there is a CEOsHouse card in the game
	And the CEOsHouse card is played without a player
	Then An ArgumentNullException is thrown

@mytag @error handling
Scenario: Play CEOsHouse no game given
	Given I have a game
	And there is a CEOsHouse card in the game
	And the CEOsHouse card is played without a game
	Then An ArgumentNullException is thrown