using System;

namespace RHFYP.Cards
{
    public class Corporation : Card
    {
        public Corporation() : base(6, "Corporation", "This card gves 3 coins when played", "treasure", 0)
        {
            
        }

        public override void PlayCard()
        {
            throw new NotImplementedException();
        }
    }
}
