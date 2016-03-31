using System;

namespace RHFYP.Cards
{
    public class Mit : Card
    {
        public Mit() : base(5, "mit", "This card is worth 3 victory points at the end of the game", "victory", 3)
        {

        }

        public override void PlayCard()
        {
            throw new NotImplementedException();
        }
    }
}
