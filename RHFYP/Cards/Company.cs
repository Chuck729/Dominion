using System;

namespace RHFYP.Cards
{
    public class Company : Card
    {
        public Company() : base(3, "company", "This card gives 2 coins when played", "treasure", 0)
        {

        }

        public override void PlayCard()
        {
            throw new NotImplementedException();
        }

    }
}
