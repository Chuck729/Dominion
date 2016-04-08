using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP.Cards
{
    public class Village : Card
    {
        public Village() : base(3, "Village", "+1 civilian +2 managers", "action", 0, "village")
        {
        }

        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
