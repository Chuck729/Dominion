﻿Feature: ScholarshipPlayCard
	As a player when I have a Scholarship card in my hand
	When I play the card
	Then I draw a card
	And my managers count increases by one
	And my gold count increases by one
	And my investment count increases by one

@playcard @Scholarship
Scenario: Scholarship is played
	Given I have a game
	And the game has 2 players
	And player 0 has a Scholarship in their hand
	And player 0 has 1 managers
	And player 0 has 0 gold
	And player 0 has 0 investments
	And player 0 is in Action mode
	And x is the number of cards player 0 has
	When player 0 plays the Scholarship card
	Then x is the number of cards player 0 has
	And player 0 has 1 gold
	And player 0 has 1 managers
	And player 0 has 1 investments

@playcard @Scholarship
Scenario: Scholarship is played but the player is not in the action state
	Given I have a game
	And the game has 2 players
	And player 0 has a Scholarship in their hand
	And player 0 has 1 managers
	And player 0 has 0 gold
	And player 0 has 0 investments
	And player 0 is in Buy mode
	And x is the number of cards player 0 has
	When player 0 plays the Scholarship card
	Then x is the number of cards player 0 has
	And player 0 has 0 gold
	And player 0 has 1 managers
	And player 0 has 0 investments

@playcard @Scholarship
Scenario: Scholarship is played but the player does not have a manager
	Given I have a game
	And the game has 2 players
	And player 0 has a Scholarship in their hand
	And player 0 has 0 managers
	And player 0 has 0 gold
	And player 0 has 0 investments
	And player 0 is in Action mode
	And x is the number of cards player 0 has
	When player 0 plays the Scholarship card
	Then x is the number of cards player 0 has
	And player 0 has 0 gold
	And player 0 has 0 managers
	And player 0 has 0 investments



@errorhandle @Scholarship
Scenario: Scholarship is played no player
	Given I have a game
	And there is a Scholarship card in the game
	And the Scholarship is played without a player
	Then An ArgumentNullException is thrown

@errorhandle @Scholarship
Scenario: Scholarship is played no game
	Given I have a game
	And there is a Scholarship card in the game
	And the Scholarship is played without a game
	Then An ArgumentNullException is thrown
