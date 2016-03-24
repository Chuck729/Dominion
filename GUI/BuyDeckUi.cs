using System;
using RHFYP;
using RHFYP.Cards;

namespace GUI
{
    public class BuyDeckUi
    {
        private readonly GameUi _parentUi;
        private readonly Deck _buyDeck;

        private bool _isCardItemMousedOver;
        private Card _cardItemMousedOver;
        private Card _cardItemSelected;

        public BuyDeckUi(GameUi parentUi)
        {
            if (parentUi == null) throw new ArgumentNullException(nameof(parentUi));
            _parentUi = parentUi;
        }
    }
}
