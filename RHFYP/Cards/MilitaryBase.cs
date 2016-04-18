using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP.Cards
{
    public class MilitaryBase : Card // Moat
    {
        public MilitaryBase() : base(2, "Military Base", "+2 Civilians", CardType.Action, 0, "")
        {
        }

        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Factory pattern for card objects.
        /// </summary>
        /// <returns>A new card object.</returns>
        public override ICard CreateCard()
        {
            return new MilitaryBase();
        }
    }
}
