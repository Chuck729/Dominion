using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP.Cards
{
    public class MilitaryBase : Card // Moat
    {
        public MilitaryBase() : base(2, "Military Base", "+2 Civilians", "action", 0, "")
        {
        }

        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
