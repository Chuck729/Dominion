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
        private int _numRandomCards = 0;
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
            Size = new Size(800, 600);
            CenterToScreen();
            ChooseCardsScreen_SizeChanged(sender, e);
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

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SubmitNumRandCards_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < cardSelectorLB.Items.Count; x++)
            {
                cardSelectorLB.SetItemChecked(x, false);
            }
            var list = cardSelectorLB.Items;
            var count = _numRandomCards;
            var rand = new Random();
            while (count > 0)
            {
                var randNum = rand.Next(0, list.Count);
                var item = list[randNum];
                if (!cardSelectorLB.CheckedIndices.Contains(randNum))
                {
                    cardSelectorLB.SetItemChecked(randNum, true);
                    cardSelectorLB.SelectedItems.Add(item);
                    count--;
                }
                if (cardSelectorLB.Items.Count == cardSelectorLB.CheckedIndices.Count)
                    break;
            }
        }
        
        private void numUpDown_ValueChanged(object sender, EventArgs e)
        {
            _numRandomCards = (int)numUpDown.Value;
        }

        private void ChooseCardsScreen_SizeChanged(object sender, EventArgs e)
        {
            textBox1.Location = new Point((ClientSize.Width - textBox1.Width) / 2,
                (ClientSize.Height)/ 10 - textBox1.Height);
            cardSelectorLB.Location = new Point((ClientSize.Width - cardSelectorLB.Width) / 2,
                (ClientSize.Height)/ 10 + textBox1.Height/2);
            numUpDown.Location = new Point((ClientSize.Width ) / 2 - cardSelectorLB.Width/2, 
                ClientSize.Height / 10 + textBox1.Height + cardSelectorLB.Height);
            textBox2.Location = new Point((ClientSize.Width) / 2 - cardSelectorLB.Width / 2 + numUpDown.Width,
                ClientSize.Height / 10 + textBox1.Height + cardSelectorLB.Height);
            SubmitNumRandCards.Location = new Point((ClientSize.Width - SubmitNumRandCards.Width) / 2, 
                textBox2.Location.Y + textBox2. Height + 10);
            SelectAllCards.Location = new Point((ClientSize.Width - SelectAllCards.Width) / 2 - SelectAllCards.Width*2/3,
                SubmitNumRandCards.Location.Y + SubmitNumRandCards.Height + 10);
            DeselectAllCards.Location = new Point((ClientSize.Width - DeselectAllCards.Width) / 2 + DeselectAllCards.Width*2/3, 
                SubmitNumRandCards.Location.Y + SubmitNumRandCards.Height + 10);
            SubmitButton.Location = new Point((ClientSize.Width - SubmitButton.Width) / 2,
                SelectAllCards.Location.Y + SelectAllCards.Height + 10);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
