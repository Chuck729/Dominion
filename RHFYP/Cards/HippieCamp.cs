using System;

namespace RHFYP.Cards
{
    public class HippieCamp: Card
    {
        public HippieCamp()
        {
            CardCost = 0;
            Name = "Hippie Camp";
            Description = "-1 Victory Point at the end of the game";
            Type = "victory";
            VictoryPoints = -1;
        }

        public override void PlayCard()
        {
            throw new NotImplementedException();
        }
    }
}
