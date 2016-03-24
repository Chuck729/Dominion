using System;

namespace RHFYP.Cards
{
    public class Mit : Card
    {
        public Mit()
        {
            CardCost = 5;
            Name = "MIT";
            Description = "This card is worth 3 victory points at the end of the game";
            Type = "victory";
            VictoryPoints = 3;
        }

        public override void PlayCard()
        {
            throw new NotImplementedException();
        }
    }
}
