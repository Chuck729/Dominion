using System;

namespace RHFYP.Cards
{
    public class HippieCamp : Card
    {
        public HippieCamp() : base(0, "Hippie Camp", "-1 Victory Point at the end of the game", "victory", -1, "")
        {

        }

        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
