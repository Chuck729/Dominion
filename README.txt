Milesstone 6:

Chuck Alexa:


Eric Haug:
Added additional tests to any class that required it in order to 
achieve 100% code coverage.
Wrote tests for and implemented the playCard methods for multiple
cards while using mocks.  

Christian Nunnally:


Lucas Weier:






Milestone 5:

Chuck Alexa:
Added comments to all the cards playCard method to describe how they need to 
interact with the players and cards.  Found an error by looking and decision tables
with the drawCard and endTurn methods in player.  Made more tests to cover full 
functionality of the code and changed the methods to make the tests pass.

Eric Haug:
Created a start up screen for the GUI.  Players now have the ability to 
choose the number of players that they want to participate in the game
as well as choose the players' names.  The game begins after the start 
button is pressed.  

Christian Nunnally:
Used intellitest on all methods in Deck and Game and Player and used TDD
to implement unit tests and features that patch up failing intellitests.
Added some more tests to ensure game rules are followed by playCard and BuyCard.
Adjusted coloring and design for the ButtonUi class.  Made the MapUi show a
visual card bounce when it's played.  Added next turn button.  Refactored and
cleaned up code.  Used factory pattern to refactor GenerateCards() to fit our 
<25 lines of code metric.  Fixed bug where dragging the map would play a card.

Added stubs for the rest of the card types.  Unit tested a few PlayCard() methods
(Still lots more to do)

Added a trash card feature so the the Ui and the Game can handle cards that allow
you to trash card.

Lucas Weier:
Added ApartmentTest that tests the Apartment class. Use of BVA -> checking whether
card exists or not, mainly. Parameterized test format in the form of IntelliTest.
Ran IntelliTest on Apartment and it passed. Had to handle possibility of null in
Apartment PlayCard() method. Same exact process for multiple other cards, including
SmallBusiness, Company, and 5 others. Also added decision table testing for DrawCard
and BuyCard in PlayerTests.



Milestone 4:

Chuck Alexa:
Refactored GUI code to not violate metrics.
Fixed some of the visuals in BuyCardViewer to fix alignments.
Fixed tile location of the buyDeckUI to be inside the cirlces.
Created the button UI and starded making the PlayAllTreasuresButton.



Eric Haug:
Wrote tests for Game using mocks and BVA and implemented methods.  
Added documentation to the Game class wherever needed.
Began testing and writing of PlayCard methods in the treasure cards. This required
a new method for Player called AddGold so I could mock Player and expect a 
method call.  


Christian Nunnally:
Used BVA, TDD, and mocking to write the game class.  Added a lot of documentation to
old code that didn't have it.  Code reviewed game class and implemented suggestions.
Pair programming with Test Engineer to help them learn my part of the code base.

Lucas Weier:
Wrote integration tests for Deck and Game with error handling and BVA in mind. Refactored
throughout the project including GetFirstCard and TestSubDeck, as well as editing
documentation. The unit tests in DeckTest now use mocks and stubs where needed rather
than fake sub classes.




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