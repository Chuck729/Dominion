Feature: HomelessGuyFeature
	As A player, I want to be able to play a HomelessGuy Card

@playcard @homelessguy
Scenario: Play homeless guy
	Given I have a game
	And I have a player
	And the player has no cards
	And the player has a Small Business in their hand
	And the player has a company in their hand
	And the player has a corporation in their hand
	And the player has a homelessguy in their hand
	And the player has a bank in their draw deck
	And the player has a Cis in thier draw deck
	And the player has 1 manager
	
	When the player plays the homeless guy
	And the player chooses to discard the small business
	And the player chooses to discard the company
	And HomelessGuyMode is set to false
	Then the small business is not in their hand 
	And the company is not in their hand
	And the bank is in their hand
	And the Cis is in their hand

@playcard @error handling
Scenario: Play Homeless Guy no player given
	Given I have a game
	And there is a Homeless Guy card in the game
	When the Homeless Guy card is played without a player
	Then An ArgumentNullException is thrown

@playcard @error handling
Scenario: Play Homeless Guy no game given
	Given I have a game
	And there is a Homeless Guy card in the game
	When the Homeless Guy card is played without a game
	Then An ArgumentNullException is thrown