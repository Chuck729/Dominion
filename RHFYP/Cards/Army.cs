﻿using System;

namespace RHFYP.Cards
{
    public class Army : Card // Militia
    {
        public Army() : base(4, "Army", "Scares all but 3 civilians away from all other players.  +2 coins", "action", 0, "army")
        {
        }

        public override void PlayCard(Player player)
        {
            throw new NotImplementedException();
        }
    }
}