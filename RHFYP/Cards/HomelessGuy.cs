using System;

namespace RHFYP.Cards
{
    public class HomelessGuy : Card // Cellar
    {
        public HomelessGuy() : base(2, "Homeless Guy", "Activates one of your tiles and allows you to randomly relolate anyof your existing civilians", "action", 0, "")
        {
        }

        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
