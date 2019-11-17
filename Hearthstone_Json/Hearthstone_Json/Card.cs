using System;
using System.Collections.Generic;
using System.Text;

namespace Hearthstone_Json
{
    public class Card
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public SpecificHandler Handler { get; set; }
    }
}
