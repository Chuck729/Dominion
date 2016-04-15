Milestone 4:

Chuck Alexa:



Eric Haug:
Wrote tests for Game using mocks and BVA and implemented methods.  
Added documentation to the Game class wherever needed.
Began testing and writing of PlayCard methods in the treasure cards. This required
a new method for Player called AddGold so I could mock Player and expect a 
method call.  


Christian Nunnally:



Lucas Weier:





Milestone 3:

Chuck Alexa:
Wrote and edited some unit test for Player and Deck classes.  Also, implemented some of
player's methods to pass the unit tests.  A little refactoring was done in deck and player
to better suit the intention of the methods.



Eric Haug:
Refactored much of our previous unit tests as well as the Card class in order
to allow the use of a dummy TestCard class in the DeckTests.  
Included more integration tests between Player, Deck and Card.
The metric that was cheosen to drive our development was lines of code.  Each method
should have no more than 25 lines of code in order to increase readability and 
simplicity.


Christian Nunnally:
Did a ton of refactoring of the UI classes to make ISimpleUi and SimpleUi.
Now all the graphics items you see on the screen inherit the same base class.
Made an animated buy menu that expands when the user mouses over the buy menu icon.
The buy menu icon displays when item the user has currently selected.  The menu will
repond to the mouse's y coord when there is not enough vertical space to display them all.


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