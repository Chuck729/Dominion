using System;

namespace RHFYP.Cards
{
    public class Area51 : Card // Chapel
    {
        public Area51() : base(2, "Area 51", "Nuke up to 4 tiles on the map", "action", 0, "area51")
        {
        }

        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
