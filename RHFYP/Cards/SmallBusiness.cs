using System;

namespace RHFYP.Cards
{
    public class SmallBusiness : Card
    {
        public SmallBusiness() : base(1, "Small Business", "Provides 1 coin when activated", "treasure", 0, "familybusiness")
        {

        }

        public override void PlayCard(Player player)
        {
            player.AddGold(1);
        }
    }
}
