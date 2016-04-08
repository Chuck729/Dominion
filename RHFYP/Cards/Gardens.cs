using System;

namespace RHFYP.Cards
{
    public class Gardens : Card
    {
        // TODO: This card has special victory point requirements. fix?
        public Gardens() : base(4, "Gardens", "+1 vp for evry 10 tiles on map (rounded down)", "victory", 0, "")
        {
        }

        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
