Feature: ScholarshipPlayCard
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
	When player 0 plays the Scholarship card
	Then player 0 draws a card
	And player 0 has 1 gold
	And player 0 has 1 managers
	And player 0 has 1 investments