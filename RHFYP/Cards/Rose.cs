﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{
    public class Rose: Card
    {
        public Rose()
        {
            CardCost = 8;
            Name = "Rose";
            Description = "This card is worth 6 victory points at the end of the game";
            Type = "victory";
            VictoryPoints = 6;
        }

        public override void playCard()
        {
            throw new NotImplementedException();
        }
    }
}
