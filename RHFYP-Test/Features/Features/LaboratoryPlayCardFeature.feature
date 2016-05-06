Feature: LaboratoryPlayCardFeature
	As a player, when I have a Laboratory card in my hand
	When I play the card
	Then I draw two cards
	And I gain a manager

@mytag @Laboratory
Scenario: Laboratory is played
	Given I have a game
	And the game has 2 players
	And player 0 has a Laboratory in their hand
	And player 0 has 1 managers
	And player 0 is in Action mode
	When player 0 plays the Laboratory card
	Then player 0 draws a card
	And player 0 draws a card
	And player 0 has 1 managers