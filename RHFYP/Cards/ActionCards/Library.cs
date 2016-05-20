using System;

namespace RHFYP.Cards.ActionCards
{
    public class Library : Card
    {
        public Library() : base(5, "Library", "Civilians visit tiles until 7 are visited.  If they visit an action tile you can choose to set aside that tile. The set aside tiles are not visited.", CardType.Action, 0, "library")
        {
        }

        public override void PlayCard(Player player, Game game)
        {
            if (player == null) throw new ArgumentNullException();
            if (game == null) throw new ArgumentNullException();

            Deck asideDeck = new Deck();

            while (player.Hand.CardList.Count < 7)
            {
                ICard c = player.DrawPile.DrawCard();

                if (c.Type == CardType.Action)
                {
                    // TODO: Will need a method in the GUI that this calls
                    // that shows the player card c and asks if the player
                    // would like this card in their hand or if they would
                    // rather set it aside to be discarded.
                    // 
                    // bool keepingCard = player.PromptAction(c, yesNo);  Perhaps.
                    // 
                    // I'm imagining this method would take a card and the
                    // type of prompt it is. In this case, it displays to the
                    // player card c and has two buttons, "Keep" and "Discard"
                    // and the player can choose to keep or discard this action
                    // card. Of course, this is all up to however Christian
                    // chose to make the thing he was talking about.
                    //
                    // If the player wants to keep the card, keepingCard is true, and
                    // the card is simply added to the player's hand. If not,
                    // the card is placed into another deck called asideDeck
                    // that will be appended into the player's DiscardPile once
                    // this card's action is done.

                    // For testing purposes let's make the player not keep the 
                    // action card.
                    bool keepingCard = false;

                    if (keepingCard)
                    {
                        player.Hand.AddCard(c);
                    } else
                    {
                        asideDeck.AddCard(c);
                    }

                } else
                {
                    // If not an action card, simply add the card to the player's hand.
                    player.Hand.AddCard(c);
                }
            }
            player.DiscardPile.AppendDeck(asideDeck);
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new Library();
        }
    }
}
