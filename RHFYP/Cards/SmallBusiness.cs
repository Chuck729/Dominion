using System;

namespace RHFYP.Cards
{
    public class SmallBusiness : Card
    {
        public SmallBusiness() : base(1, "Small Business", "This card gives 1 coin when played", "treasure", 0)
        {

        }

        public override void PlayCard()
        {
            throw new NotImplementedException();
        }
    }
}
