using System;

namespace RHFYP.Cards
{
    public class TestCard : Card
    {
        //Create test card with certain values
        //this is just a test with random assigned values
        //each other card implemented will have meaningful values
        public TestCard() : base(3, "TestCard", "This card is used for testing purposes", "action", 1, "TestCard")
        {

        }

        public override void PlayCard(Player player)
        {
            //TODO dummy action for now; used for testing purposes. 
            player.Gold++;
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new TestCard();
        }
    }
}
