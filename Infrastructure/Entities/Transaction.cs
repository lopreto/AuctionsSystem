using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AuctionsSystem.Infrastructure.Entities
{
    [Table("Transaction")]
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public int AuctionId { get; set; }
        public string BuyerId { get; set; }
        public string SellerId { get; set; }
        public decimal Bid { get; set; }
        public DateTime? TransactionEndDate { get; set; }
        public bool TransactionContemplated { get; set; }

    }
}