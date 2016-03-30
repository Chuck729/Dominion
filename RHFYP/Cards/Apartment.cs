namespace RHFYP.Cards
{
    public class Apartment: Card
    {
        public Apartment()
        {
            CardCost = 3;
            Name = "Apartment";
            Description = "+2 actions and +1 card";
            Type = "action";
            VictoryPoints = 0;
        }

        public override void PlayCard()
        {

        }
    }
}
