using System;

namespace RHFYP.Cards
{
    public class Museum : Card // Bureaucrat
    {
        public Museum() : base(4, "Museum", "Place a company that will be visited next turn.  Opponents civilians will stay on victory tiles next turn", "action", 0, "museum")
        {
        }

        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
