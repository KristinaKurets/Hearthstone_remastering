using System;
using System.Collections.Generic;
using System.Text;

namespace Hearthstone_Json
{
    public class Player
    {
        public int Hp { get; set; }
        public List<Card> Deck { get; set; }
        public List<Card> Hand { get; set; }
        public List<Card> Table { get; set; }
    }
}
