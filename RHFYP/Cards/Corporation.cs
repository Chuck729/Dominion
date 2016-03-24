using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RHFYP.Cards;

namespace RHFYP
{
    public class Corporation: Card
    {
        public Corporation()
        {
            CardCost = 6;
            Name = "Corporation";
            Description = "This card gives 3 coins when played";
            Type = "treasure";
            VictoryPoints = 0;
        }

        public override void PlayCard()
        {
            throw new NotImplementedException();
        }
    }
}
