using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab28.Models
{

    public class Deck
    {
        public bool Success { get; set; }
        public string Deck_id { get; set; }
        public bool Shuffled { get; set; }
        public int Remaining { get; set; }
    }
}
