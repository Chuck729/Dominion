using System.Runtime.InteropServices;
using RHFYP;

namespace GUI
{
    public class BuyDeckViewer
    {
        private readonly GameViewer _parentViewer;
        private readonly Deck _buyDeck;

        public BuyDeckViewer(GameViewer parentViewer)
        {
            _parentViewer = parentViewer;
        }
    }
}
