using System;

namespace RHFYP.Cards
{
    public class Company : Card
    {

        public Company() : base(3, "Company", "This building gives 2 coins when activated", "treasure", 0, "company")
        {

        }

        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }

    }
}
