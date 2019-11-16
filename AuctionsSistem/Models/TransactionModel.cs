using AuctionsSystem.Infrastructure.Contexts;
using AuctionsSystem.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AuctionsSystem.Models
{
    public class TransactionModel : BaseModel
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public int AuctionId { get; set; }
        public string BuyerId { get; set; }
        public string SellerId { get; set; }
        public decimal Bid { get; set; }
        public DateTime? TransactionEndDate { get; set; }
        public bool TransactionContemplated { get; set; }
        public List<TransactionModel> Transactions { get; set; }

        public TransactionModel()
        {
            Transactions = new List<TransactionModel>();
        }

        public List<TransactionModel> Select()
        {
            var transactions = _context.Transactions.ToList();

            transactions.ForEach(t => {
                Transactions.Add(new TransactionModel
                {
                    Id = t.Id,
                    BuyerId = t.BuyerId,
                    SellerId = t.SellerId,
                    AuctionId = t.AuctionId,
                    Bid = t.Bid,
                    TransactionContemplated = t.TransactionContemplated,
                    TransactionDate = t.TransactionDate,
                    TransactionEndDate = t.TransactionEndDate,
                });
            });

            return Transactions;
        }

        internal void Create(AuctionModel auctionModel, string currentUserId)
        {
            _context.Transactions.Add(new Transaction
            {
                TransactionDate = DateTime.Now,
                AuctionId = auctionModel.Id,
                BuyerId = currentUserId,
                SellerId = auctionModel.UserId,
                Bid = auctionModel.Bid,
                TransactionContemplated = false
            });
            _context.SaveChanges();
        }
    }
}