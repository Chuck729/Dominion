﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{
    public class SmallBusiness: Card
    {
        public SmallBusiness()
        {
            CardCost = 1;
            Name = "Small Business";
            Description = "This card gives 1 coin when played";
            Type = "treasure";
            VictoryPoints = 0;
        }

        public override void playCard()
        {
            throw new NotImplementedException();
        }
    }
}