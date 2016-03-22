using System.Data.Common;
using System.Drawing;

namespace RHFYP
{
    public class Card
    {
        /// <summary>
        /// The string that represents the title of the card.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The location of this card on the map (if it's part of a map)
        /// </summary>
        public Point Location { get; set; }

        public Card()
        {
            // NOTE: ALL TEMP
            Type = "company";
        }
    }
}