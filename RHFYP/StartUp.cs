using RHFYP.Cards;

namespace RHFYP
{
    public class StartUp : Card // Feast
    {
        public StartUp() : base(4, "StartUp", "Replace this tile with atile costing up to 5", "action", 0, "startup")
        {
        }

        public override void PlayCard(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
