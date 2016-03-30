using System;

namespace RHFYP.Cards
{
    public class Purdue : Card
    {
        public Purdue() : base(3, "Purdue", "This card is worth 1 victory point at the end of the game", "victory", 1)
        {

        }

        public override void PlayCard()
        {
            throw new NotImplementedException();
        }
    }
}
