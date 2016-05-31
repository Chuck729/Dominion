using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using RHFYP.Cards;
using RHFYP.Cards.ActionCards;
using RHFYP.Cards.VictoryCards;

namespace StartUpScreen
{
    public partial class ChooseCardsScreen : Form
    {
        private int _numRandomCards;

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

            _numRandomCards = 10;
        }

        private void ChooseCardsScreen_Load(object sender, EventArgs e)
        {
            Size = new Size(530, 400);
            CenterToScreen();
            ChooseCardsScreen_SizeChanged(sender, e);
        }

        public List<ICard> GetCardList()
        {
            var list = new List<ICard>();
            foreach (var item in cardSelectorLB.CheckedItems)
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

        public void SetCheckedItems(List<ICard> cardList)
        {
            var indexes = new List<int>();
            var items = cardSelectorLB.Items;
            var index = 0;

            foreach (var item in items)
            {
                var card = item as ICard;
                foreach (var c in cardList)
                {
                    if (card != null && card.GetType() != c.GetType()) continue;
                    new List<object>().Add(item);
                    indexes.Add(index);
                }
                index++;
            }
            foreach (var i in indexes)
            {
                cardSelectorLB.SetItemChecked(i, true);
            }
        }

        private void SelectAllCards_Click(object sender, EventArgs e)
        {
            if (ToggleSelectAllButton.Text == "Select All")
            {
                for (var x = 0; x < cardSelectorLB.Items.Count; x++)
                {
                    cardSelectorLB.SetItemChecked(x, true);
                }
                ToggleSelectAllButton.Text = "Deselect All";
            }
            else
            {
                for (var x = 0; x < cardSelectorLB.Items.Count; x++)
                {
                    cardSelectorLB.SetItemChecked(x, false);
                }
                ToggleSelectAllButton.Text = "Select All";
            }
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SubmitNumRandCards_Click(object sender, EventArgs e)
        {
            for (var x = 0; x < cardSelectorLB.Items.Count; x++)
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

        private void ChooseCardsScreen_SizeChanged(object sender, EventArgs e)
        {
            textBox1.Location = new Point((ClientSize.Width - textBox1.Width)/2,
                ClientSize.Height/6 - textBox1.Height);
            cardSelectorLB.Location = new Point((ClientSize.Width - cardSelectorLB.Width)/2,
                ClientSize.Height/6 + textBox1.Height/2);
            var randomCardButtonsWidths = SubmitNumRandCards.Width + NumRandomCardsUpButton.Width + NumRandomCardsDownButton.Width + 10;
            SubmitNumRandCards.Location = new Point((ClientSize.Width - randomCardButtonsWidths) / 2, cardSelectorLB.Location.Y + cardSelectorLB.Height + 10);
            NumRandomCardsUpButton.Location = new Point((ClientSize.Width - randomCardButtonsWidths) / 2 + SubmitNumRandCards.Width + 5, cardSelectorLB.Location.Y + cardSelectorLB.Height + 10);
            NumRandomCardsDownButton.Location = new Point((ClientSize.Width - randomCardButtonsWidths) / 2 + SubmitNumRandCards.Width + NumRandomCardsUpButton.Width + 10, cardSelectorLB.Location.Y + cardSelectorLB.Height + 10);
            ToggleSelectAllButton.Location = new Point(SubmitNumRandCards.Location.X, SubmitNumRandCards.Location.Y + SubmitNumRandCards.Height + 10);
            ToggleSelectAllButton.Width = (randomCardButtonsWidths - 5) / 2;
            SubmitButton.Width = (randomCardButtonsWidths - 5) / 2;
            SubmitButton.Location = new Point(ToggleSelectAllButton.Location.X + ToggleSelectAllButton.Width + 5, ToggleSelectAllButton.Location.Y);
        }

        private void NumRandomCardsDownButton_Click(object sender, EventArgs e)
        {
            _numRandomCards = Math.Min(cardSelectorLB.Items.Count - 1, --_numRandomCards);
            _numRandomCards = Math.Max(0, _numRandomCards);
            SubmitNumRandCards.Text = $"Generate {_numRandomCards} Random Cards";
        }

        private void NumRandomCardsUpButton_Click(object sender, EventArgs e)
        {
            _numRandomCards = Math.Min(cardSelectorLB.Items.Count - 1, ++_numRandomCards);
            _numRandomCards = Math.Max(0, _numRandomCards);
            SubmitNumRandCards.Text = $"Generate {_numRandomCards} Random Cards";
        }
    }
}