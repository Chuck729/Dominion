using System;

namespace RHFYP.Cards
{
    public class WallStreet : Card // Chancellor
    {
        public WallStreet() : base(3, "Wall Street", "+2 coins.  All civilians visit the same places on your next turn", "action", 0, "")
        {
        }

        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
