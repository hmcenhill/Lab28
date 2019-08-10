using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab28.Models
{
    public class Draw
    {
        public bool Success { get; set; }
        public Card[] Cards { get; set; }
        public string Deck_id { get; set; }
        public int Remaining { get; set; }
    }
}
