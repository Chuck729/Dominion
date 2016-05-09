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
        }

        private void ChooseCardsScreen_Load(object sender, EventArgs e)
        {
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
            return list;
        }
        private void cardSelectorLB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
