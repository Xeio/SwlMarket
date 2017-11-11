using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwlMarket.Models
{
    public class Price
    {
        public int Id { get; set; }
        public int ItemID { get; set; }
        [DisplayName("Expires At")]
        [DisplayFormat]
        public DateTime ExpiresAt { get; set; }
        public int Marks { get; set; }

        public Item Item { get; set; }
    }
}
