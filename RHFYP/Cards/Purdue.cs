using System;

namespace RHFYP.Cards
{
    public class Purdue : Card
    {
        public Purdue() : base(3, "Purdue", "This card is worth 1 victory point at the end of the Game", "victory", 1, "purdue")
        {

        }

        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
