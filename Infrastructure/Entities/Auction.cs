using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AuctionsSystem.Infrastructure.Entities
{
    [Table("Auctions")]
    public class Auction
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comments { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime InitialDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime FinalDate { get; set; }
        public bool Active { get; set; }
        public string UserId { get; set; }
        public decimal BestBid { get; set; }
    }
}