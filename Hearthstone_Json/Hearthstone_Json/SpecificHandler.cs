using System;
using System.Collections.Generic;
using System.Text;

namespace Hearthstone_Json
{
    public class SpecificHandler
    {
        public HandlerType HandlerType { get; set; }
        public string Description { get; set; }
        public int Damage { get; set; }
    }

    public enum HandlerType
    {
        None,
        BattleCry,
        DeathRattle
    }
}
