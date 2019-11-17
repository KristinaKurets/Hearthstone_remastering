using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hearthstone_Json
{
    public class DeckService
    {
        public List<Card> CreateDeck (string path)
        {
            string json = File.ReadAllText(path);
            List<Card> deck = JsonConvert.DeserializeObject<List<Card>>(json);
            return(deck.OrderBy(c => Guid.NewGuid()).ToList());

        }
    }
}
