using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{
    class MIT: Card
    {
        public MIT()
        {
            CardCost = 5;
            Name = "MIT";
            Description = "This card is worth 3 victory points at the end of the game";
            Type = "victory";
            VictoryPoints = 3;
        }

        public override void playCard()
        {
            throw new NotImplementedException();
        }
    }
}
