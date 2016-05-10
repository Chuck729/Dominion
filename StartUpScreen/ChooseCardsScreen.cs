using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.VictoryCards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace StartUpScreen
{
    public partial class ChooseCardsScreen : Form
    {
        public ChooseCardsScreen()
        {

            InitializeComponent();
            cardSelectorLB.Items.Add(new Apartment()); // 23 total cards added
            cardSelectorLB.Items.Add(new Area51());
            cardSelectorLB.Items.Add(new Army());
            cardSelectorLB.Items.Add(new Bank());
            cardSelectorLB.Items.Add(new CeosHouse());
            cardSelectorLB.Items.Add(new Committee());
            cardSelectorLB.Items.Add(new ConstructionSite());
            cardSelectorLB.Items.Add(new Gardens());
            cardSelectorLB.Items.Add(new HomelessGuy());
            cardSelectorLB.Items.Add(new Laboratory());
            cardSelectorLB.Items.Add(new LawFirm());
            cardSelectorLB.Items.Add(new Library());
            cardSelectorLB.Items.Add(new MilitaryBase());
            cardSelectorLB.Items.Add(new Mine());
            cardSelectorLB.Items.Add(new Museum());
            cardSelectorLB.Items.Add(new Plug());
            cardSelectorLB.Items.Add(new Prison());
            cardSelectorLB.Items.Add(new Scholarship());
            cardSelectorLB.Items.Add(new SpeedyLoans());
            cardSelectorLB.Items.Add(new StartUp());
            cardSelectorLB.Items.Add(new Storeroom());
            cardSelectorLB.Items.Add(new Subdivision());
            cardSelectorLB.Items.Add(new WallStreet());
            cardSelectorLB.CheckOnClick = true;
        }

        private void ChooseCardsScreen_Load(object sender, EventArgs e)
        {
            
            
        }

        public List<ICard> GetCardList()
        {
            var list = new List<ICard>();
            foreach(var item in cardSelectorLB.CheckedItems)
            {
                var card = item as ICard;
                if (card != null)
                {
                    list.Add(card);
                }
            }
            if (list.Count == 0)
                return null;
            return list;
        }
        private void cardSelectorLB_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public void setCheckedItems(List<ICard> cardList)
        {
            var indexes = new List<int>();
            var items = cardSelectorLB.Items;
            var itemList = new List<Object>();
            var index = 0;
            
            foreach(var item in items)
            {
                var card = item as ICard;
                foreach (var c in cardList)
                {
                    if (card.GetType() == c.GetType())
                    {
                        itemList.Add(item);
                        indexes.Add(index);
                    }
                }
                index++;
            }
            foreach(var i in indexes)
            {
                cardSelectorLB.SetItemChecked(i, true);
            }
        }

        private void SelectAllCards_Click(object sender, EventArgs e)
        {
            for(int x = 0; x < cardSelectorLB.Items.Count; x++)
            {
                cardSelectorLB.SetItemChecked(x, true);
            }
        }

        private void DeselectAllCards_Click(object sender, EventArgs e)
        {
            for(int x = 0; x < cardSelectorLB.Items.Count; x++)
            {
                cardSelectorLB.SetItemChecked(x, false);
            }
        }
    }
}
