using System;

namespace RHFYP.Cards
{
    public class LawFirm : Card // Woodcutter
    {
        public LawFirm() : base(3, "Law Firm", "+1 investment +2 coins", "action", 0, "")
        {
        }

        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
