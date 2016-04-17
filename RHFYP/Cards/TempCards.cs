namespace RHFYP.Cards
{
    public class TempCard1 : Card
    {
        public TempCard1() : base(3, "Temp1", "+2 actions and +1 card", "action", 0, "")
        {
            
        }

        public override void PlayCard(Player player)
        {

        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new TempCard1();
        }
    }

    public class TempCard2 : Card
    {
        public TempCard2() : base(3, "Temp2", "+2 actions and +1 card", "action", 0, "")
        {

        }

        public override void PlayCard(Player player)
        {

        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new TempCard2();
        }
    }

    public class TempCard3 : Card
    {
        public TempCard3() : base(3, "Temp3", "+2 actions and +1 card", "action", 0, "")
        {

        }

        public override void PlayCard(Player player)
        {

        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new TempCard3();
        }
    }
}
