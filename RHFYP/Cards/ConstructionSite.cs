using System;

namespace RHFYP.Cards
{
    public class ConstructionSite : Card // Workshop
    {
        public ConstructionSite() : base(3, "Construction Site", "Transform into a tile costing up to 4", "action", 0, "")
        {
        }

        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
