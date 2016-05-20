Feature: LibraryPlayCard
	As a player, I want to be able to play a Library card

@playcard @Library
Scenario: Play Library card, draw 7 non-action cards
	Given I have a game
	And the game has 2 players
	And player 0 has no cards
	And player 0 has a Library card in their hand
	And player 0 is in Action mode
	And player 0 has 1 Manager
	And player 0 has 10 non-action cards in their draw pile

	When player 0 plays the Library card

	Then player 0 does not have a Library card in their hand
	And player 0 has a Library card in their discard pile
	And player 0 has 7 non-action cards in their hand
	And player 0 has 3 non-action cards in their draw pile
	And player 0 has 0 managers

@playcard @Library
Scenario: Play Library card, draw 7 non-action cards and 1 action card, but discards the action card
	Given I have a game
	And the game has 2 players
	And player 0 has no cards
	And player 0 has a Library card in their hand
	And player 0 is in Action mode
	And player 0 has 1 Manager
	And player 0 has 1 action cards in their draw pile
	And player 0 has 10 non-action cards in their draw pile
	
	When player 0 plays the Library card

	Then player 0 does not have a Library card in their hand
	And player 0 has a Library card in their discard pile
	And player 0 has 7 non-action cards in their hand
	And player 0 has 3 non-action cards in their draw pile
	And player 0 has 0 managers

@playcard @error handling
Scenario: Play Library card no player given
	Given I have a game
	And there is a Library card in the game
	When the Library card is played without a player
	Then An ArgumentNullException is thrown

@playcard @error handling
Scenario: Play Library card no game given
	Given I have a game
	And there is a Library card in the game
	When the Library card is played without a game
	Then An ArgumentNullException is thrown