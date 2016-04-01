Milestone 3:

Chuck Alexa:



Eric Haug:
Refactored much of our previous unit tests as well as the Card class in order
to allow the use of a dummy TestCard class in the DeckTests.  
Included more integration tests between Player, Deck and Card.
The metric that was cheosen to drive our development was lines of code.  Each method
should have no more than 25 lines of code in order to increase readability and 
simplicity.


Christian Nunnally:



Lucas Weier:
Wrote IPlayer and IPlayer's comments. Wrote unit tests for PlayerTests. Tried for a good
while to make mocking work, but gave up for now. Implemented some of the methods in
Player.





Milestone 2:

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



Lucas Weier:
Wrote the majority of the tests for the Deck class. After these tests were written
and commited I wrote just enough code in the actual methods in Deck for the tests
to pass. Tried to stick to TDD as best I could.


Test Results: 
Currently all unit tests and integration tests pass except one, TestShuffleIn()
The ShuffleIn() method still needs further implementation to meet the current
test.  