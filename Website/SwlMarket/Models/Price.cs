using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwlMarket.Models
{
    public class Price
    {
        private DateTime _time;

        public int Id { get; set; }
        public int ItemID { get; set; }
        public DateTime Time
        {
            get
            {
                if (_time.Kind == DateTimeKind.Unspecified)
                {
                    _time = DateTime.SpecifyKind(_time, DateTimeKind.Utc);
                }
                return _time;
            }
            set
            {
                _time = value;
            }
        }
        public int? ExpiresIn { get; set; }
        public int Marks { get; set; }
        public int? IPId { get; set; }

        public Item Item { get; set; }

        public IPEntry IP { get; set; }
    }

    public class CurrentPrice : Price
    {

    }
    
    public class HistoricalPrice : Price
    {

    }
}
