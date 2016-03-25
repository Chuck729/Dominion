Chuck Alexa:



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