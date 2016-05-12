Feature: HomelessGuyFeature
	As A player, I want to be able to play a HomelessGuy Card

@playcard @homelessguy
Scenario: Play homeless guy
	Given I have a player
	And the player has a small business in their hand
	And the player has a company in their hand
	And the player has a corporation in their hand
	And the player has a homelessguy in their hand
	And the player has a bank in their draw deck
	And the player has a Cis in thier draw deck
	When the player plays the homeless guy
	And the player chooses to discard the small business
	And the player chooses to discard the company
	And HomelessGuyMode is set to false
	Then the small business is not in their hand 
	And the company is not in their hand
	And the bank is in their hand
	And the Cis is in their hand

