using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{
    public class TestCard :Card
    {
        //Create test card with certain values
        //this is just a test with random assigned values
        //each other card implemented will have meaningful values
        public TestCard()
        {
            CardCost = 3;
            Name = "TestCard";
            Description = "This card is used for testing purposes";
            Type = "action";
            VictoryPoints = 1;
        }

        public override void playCard()
        {
            throw new NotImplementedException();
        }
    }
}
