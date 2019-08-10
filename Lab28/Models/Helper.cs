using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab28.Models
{
    public class Helper
    {
        public bool IsDeck { get; set; }
        public int Remaining { get; set; }
        public bool AreDraws { get; set; }
        public List<Card> Cards { get; set; }
    }
}
