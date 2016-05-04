Feature: MuseumPlayCard
	As a player when I play museum
	I should gain a company on the top of my draw pile
	Opponents that do not have a moat should have to put a victory card 
	from their hand on top of their draw pile

@playcard @Museum
Scenario: Play Museum Card opponet has victory
	Given I have a game
	And the game has 2 players
	And player 0 has a Museum card
	And player 1 has a Purdue card
	And player 1 does not have a Military Base
	And player 0 is in Action mode
	And player 0 has managers
	When player 0 plays the Museum card
	Then player 0 has a Company card on top of their draw pile
	And player 0 Museum is discarded
	And player 1 has a Purdue card on top of thier draw pile

@playcard @Museum
Scenario: Play Museum Card opponent does not have victory
	Given I have a game
	And the game has 2 players
	And player 0 has a Museum card
	And player 1 does not have a Victory card
	And player 1 does not have a Military Base
	And player 0 is in Action mode
	And player 0 has managers 
	When player 0 plays the Museum card
	Then player 0 has a Company card on top of their draw pile
	And player 0 Museum is discarded

Scenario: Play Museum Card No Managers
	Give i have a game
	And the game has 2 players
	And player 0 has a Museum card
	And player 0 is in Action mode
	And player 0 does not have managers
	When player 0 plays the Museum card
	Then player 0 can not play the Museum card