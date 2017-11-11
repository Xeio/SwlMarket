using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwlMarket.Models
{
    public class ApiKey
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
