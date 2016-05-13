Feature: LaboratoryBottomUp
	Bottom up integration testing of Laboratory card.

@bottomup @laboratory @unit
Scenario: Create a new Laboratory card
	Given there are no cards
	When I create a Laboratory card
	Then a Laboratory card is created

@bottomup @laboratory @deck
Scenario: Add a new Laboratory card to a deck
	Given there is a deck with 0 Laboratory cards
	And there is a Laboratory card
	When I add the Laboratory card to the deck
	Then the deck contains 1 cards

@bottomup @laboratory @deck
Scenario: Draw an existing Laboratory card from a deck
	Given there is a deck with 1 Laboratory cards
	When I draw a card from the deck
	Then the deck contains 0 cards
	And the drawn card is a Laboratory card

@bottomup @laboratory @player
Scenario: A player plays a Laboratory card
	Given there is a player
	And the player has a Laboratory card in their hand
	And the player has 1 managers
	And the player is in the Action state
	And the player has 3 Laboratory cards in their hand
	And the player has 2 Laboratory cards in their draw pile
	When the player plays the Laboratory card
	Then the player has 1 managers
	And the player has 4 cards in their hand
	And the Laboratory card was played

@bottomup @laboratory @player
Scenario: A player plays a Laboratory card but doesn't have any managers
	Given there is a player
	And the player has a Laboratory card in their hand
	And the player has 0 managers
	And the player is in the Action state
	And the player has 3 Laboratory cards in their hand
	And the player has 2 Laboratory cards in their draw pile
	When the player plays the Laboratory card
	Then the player has 0 managers
	And the player has 3 cards in their hand
	And the Laboratory card was not played

@bottomup @laboratory @player
Scenario: A player plays a Laboratory card but is not in the Action state
	Given there is a player
	And the player has a Laboratory card in their hand
	And the player has 1 managers
	And the player is in the Buy state
	And the player has 3 Laboratory cards in their hand
	And the player has 2 Laboratory cards in their draw pile
	When the player plays the Laboratory card
	Then the player has 1 managers
	And the player has 3 cards in their hand
	And the Laboratory card was not played

@bottomup @laboratory @game
Scenario: Try to add 1 player to a new game
	Given there is a game
	When I make the number of players in the game 1
	Then an ArgumentOutOfRangeException is thrown

@bottomup @laboratory @game
Scenario: Try to add five players to a new game
	Given there is a game
	When I make the number of players in the game 5
	Then an ArgumentOutOfRangeException is thrown