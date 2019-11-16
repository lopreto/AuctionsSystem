using AuctionsSystem.Infrastructure.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

namespace AuctionsSystem.Models
{
    public class WalletModel : BaseModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Saldo")]
        public decimal Balance { get; set; }
        [Display(Name = "Adicionar saldo:")]
        public decimal HandlingValue { get; set; }
        public List<WalletHistoryModel> WalletHistory { get; set; }

        public List<WalletModel> Wallets { get; set; }

        public WalletModel()
        {
            Wallets = new List<WalletModel>();
            WalletHistory = new List<WalletHistoryModel>();
        }

        public WalletModel SelectByUserId(string currentUserId)
        {
            var wallet = _context.Wallets.ToList().Where(s => s.UserId == currentUserId).FirstOrDefault();
            var walletModel = new WalletModel
            {
                Id = wallet.Id,
                UserId = wallet.UserId,
                Balance = wallet.Balance,
            };

            var walletHistories = _context.WalletsHistory.ToList().Where(h => h.BuyerWalletId == wallet.Id || h.SellerWalletId == wallet.Id || h.WalletId == wallet.Id).ToList();

            foreach (var walletHistory in walletHistories)
            {
                walletModel.WalletHistory.Add(new WalletHistoryModel
                {
                    Id = walletHistory.Id,
                    BuyerWalletId = walletHistory.BuyerWalletId,
                    SellerWalletId = walletHistory.SellerWalletId,
                    TransactionDate = walletHistory.TransactionDate,
                    TransactionValue = walletHistory.TransactionValue,
                    Description = walletHistory.Description,
                    WalletIdUserLogged = walletModel.Id
                });
            }

            walletModel.Balance = CalculateBalance(walletModel);

            return walletModel;
        }

        public decimal CalculateBalance(WalletModel walletModel)
        {
            var transactions = _context.Transactions.ToList().Where(t => t.BuyerId == walletModel.UserId && t.TransactionContemplated == false && t.TransactionEndDate == null).OrderByDescending(t => t.Bid).ToList();
            decimal bids = 0;
            int lastAuctionId = 0;

            transactions.ForEach(t =>
            {
                if (lastAuctionId != t.AuctionId)
                {
                    bids += t.Bid;
                    lastAuctionId = t.AuctionId;
                }
            });

            return walletModel.Balance - bids;
        }

        public void Create(string userId)
        {
            _context.Wallets.Add(new Wallet
            {
                UserId = userId,
                Balance = 0,
            });
            _context.SaveChanges();
        }

        public void AddBalance(string currentUserId)
        {
            var wallet = _context.Wallets.ToList().Where(w => w.UserId == currentUserId).FirstOrDefault();
            wallet.Balance = wallet.Balance + HandlingValue;

            _context.Entry(wallet).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}