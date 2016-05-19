Feature: CIAPlayCard
	As a player, I want to be able to play a CIA card

@playcard @CIA
Scenario: Play a CIA card with two players, player's top card is discarded
	Given I have a game
	And the game has 2 players
	And player 0 has no cards
	And player 1 has no cards
	And player 0 is in Action mode
	And player 0 has 1 managers
	And player 1 has 5 non-action cards in their draw pile
	And player 0 has 5 non-action cards in their draw pile
	And player 0 has a CIA card in their hand

	When player 0 plays the CIA card and chooses to discard it

	Then player 0 has 4 non-action cards in their draw pile
	And player 1 has 4 non-action cards in their draw pile
	And player 0 has 1 non-action cards in their discard pile
	And player 1 has 1 non-action cards in their discard pile
	And player 0 has 1 non-action cards in their hand
	And player 0 does not have a CIA card in their hand
	And player 0 has a CIA card in their discard pile
	And player 0 has 1 managers


@playcard @error handling
Scenario: Play CIA card no player given
	Given I have a game
	And there is a CIA card in the game
	When the CIA card is played without a player
	Then An ArgumentNullException is thrown

@playcard @error handling
Scenario: Play CIA card no game given
	Given I have a game
	And there is a CIA card in the game
	When the CIA card is played without a game
	Then An ArgumentNullException is thrown