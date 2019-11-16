using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionsSystem.Infrastructure.Entities
{
    [Table("WalletHistory")]
    public class WalletHistory
    {
        public int Id { get; set; }
        public int BuyerWalletId { get; set; }
        public int SellerWalletId { get; set; }
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TransactionValue { get; set; }
        public string Description { get; set; }
        public int WalletId { get; set; }
    }
}