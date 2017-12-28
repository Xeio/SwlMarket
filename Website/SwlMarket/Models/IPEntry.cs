using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwlMarket.Models
{
    public class IPEntry
    {
        public int Id { get; set; }
        public string IP { get; set; }
        public bool Blocked { get; set; }
        public string Notes { get; set; }
    }
}
