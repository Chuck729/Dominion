Chuck Alexa:
Added tests and implementations to methods in card and some methods in deck.
Some changes of test and implementations were made when error in logic were discovered.
Some comments added to help describe code.


Eric Haug:
Added error handling to both the Card class and the Deck class.  
The Card class does not allow for data to be altered once set, and
if someone attempts to do this, then an exception is thrown.  Also, 
an instance of a Card can only exist in one deck at a time, and if 
any piece of code or user input attempts to break this constraint, 
an exception is thrown.
The only refactoring of code I had to do was making a change to the 
shuffle() method in Deck. I refactored in order to remove an 
unneaded method call.  
Some integration testing between Card and Deck and between methods in Deck. 



Christian Nunnally:
Made an isometric map tile system for the game.  Also began work on the GUI elements like the player name
and player info displayed in the top left.  The 'buy' decks will be able to be displayed on the right within
the next week.  Refactered the entire GUI class system to adhear to good OOP standards.


Lucas Weier:
Wrote the majority of the tests for the Deck class. After these tests were written
and commited I wrote just enough code in the actual methods in Deck for the tests
to pass. Tried to stick to TDD as best I could.


Test Results: 
Currently all unit tests and integration tests pass except one, TestShuffleIn()
The ShuffleIn() method still needs further implementation to meet the current
test.  