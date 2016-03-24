using System;
using RHFYP;

namespace GUI
{
    public class BuyDeckUi
    {
        private readonly GameUI _parentUi;
        private readonly Deck _buyDeck;

        public BuyDeckUi(GameUI parentUi)
        {
            if (parentUi == null) throw new ArgumentNullException(nameof(parentUi));
            _parentUi = parentUi;
        }
    }
}
