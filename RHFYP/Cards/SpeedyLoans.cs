using System;

namespace RHFYP.Cards
{
    public class SpeedyLoans : Card // Money lender
    {
        public SpeedyLoans() : base(4, "Speedy Loans", "Destory a small business and +3 coins.", "action", 0, "speedyloans")
        {
        }

        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
