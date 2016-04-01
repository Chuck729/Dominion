using System;

namespace RHFYP.Cards
{
    public class Rose : Card
    {
        public Rose() : base(8, "rosehulman", "This card is worth 6 victory points at the end of the game", "victory", 6)
        {

        }

        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
