﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SwlMarket.Models
{
    public class Price
    {
        public int Id { get; set; }
        public int ItemID { get; set; }
        public DateTime Time { get; set; }
        public int? ExpiresIn { get; set; }
        public int Marks { get; set; }
        public int? ApiKeyId { get; set; }

        public Item Item { get; set; }
        /// <summary>
        /// API Key used to upload this price
        /// </summary>
        public ApiKey ApiKey { get; set; }

        [DisplayName("Expires At")]
        [DisplayFormat(NullDisplayText = "Expired")]
        [NotMapped]
        public DateTime? ExpiresAt
        {
            get
            {
                var expirationDate = Time.AddSeconds(ExpiresIn ?? 0);
                return expirationDate > DateTime.Now ? (DateTime?)expirationDate : null;
            }
        }
    }
}
