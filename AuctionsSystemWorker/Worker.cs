using AuctionsSystem.Infrastructure;
using AuctionsSystem.Infrastructure.Contexts;
using AuctionsSystem.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AuctionsSystemWorker
{
    public class Worker
    {
        public Context Context { get; set; }
        public Worker()
        {
            Context = new Context();
        }

        public void Run()
        {
            var auctionsFinisheds = GetAuctionsFinisheds();
            auctionsFinisheds.ForEach(a => DesactivateAuction(a));

            foreach (var auctionFinished in auctionsFinisheds)
            {
                var transactions = GetTransactionsByAuctionId(auctionFinished.Id);

                if (transactions.Any())
                {
                    var contemplatedTransaction = SetTransactionsFinished(transactions);

                    var buyerWallet = GetWallet(contemplatedTransaction.BuyerId);
                    var sellerWallet = GetWallet(contemplatedTransaction.SellerId);

                    DiscountBuyerBalance(buyerWallet, contemplatedTransaction.Bid);
                    CreditSellerBalance(sellerWallet, contemplatedTransaction.Bid);
                    CreateWalletHistory(buyerWallet, sellerWallet, contemplatedTransaction.Id, contemplatedTransaction.Bid, auctionFinished.Name);
                }
            }
        }

        private void DesactivateAuction(Auction auctionFinished)
        {
            auctionFinished.Active = false;

            Context.Entry(auctionFinished).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public List<Auction> GetAuctionsFinisheds()
        {
            return Context.Auctions.ToList().Where(a => a.FinalDate < DateTime.Now && a.Active).ToList();
        }

        public List<Transaction> GetTransactionsByAuctionId(int auctionId)
        {
            return Context.Transactions.ToList().Where(a => a.AuctionId == auctionId).ToList();
        }

        public Wallet GetWallet(string id)
        {
            return Context.Wallets.ToList().Where(w => w.UserId == id).FirstOrDefault();
        }

        public Transaction SetTransactionsFinished(List<Transaction> transactions)
        {
            var transactionBetterBid = transactions.OrderByDescending(t => t.Bid).FirstOrDefault();
            transactions.ForEach(t =>
            {
                if (t.Id == transactionBetterBid.Id)
                    t.TransactionContemplated = true;

                t.TransactionEndDate = DateTime.Now;
                Context.Entry(t).State = EntityState.Modified;
            });

            Context.SaveChanges();

            return transactionBetterBid;
        }

        public void DiscountBuyerBalance(Wallet buyerWallet, decimal discountValue)
        {
            buyerWallet.Balance = buyerWallet.Balance - discountValue;

            Context.Entry(buyerWallet).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void CreditSellerBalance(Wallet sellerWallet, decimal discountValue)
        {
            sellerWallet.Balance = sellerWallet.Balance + discountValue;
            Context.Entry(sellerWallet).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void CreateWalletHistory(Wallet buyerWallet, Wallet sellerWallet, int transactionId, decimal bid, string auctionName)
        {
            Context.WalletsHistory.Add(new WalletHistory
            {
                BuyerWalletId = buyerWallet.Id,
                SellerWalletId = sellerWallet.Id,
                TransactionId = transactionId,
                TransactionDate = DateTime.Now,
                TransactionValue = bid,
                Description = $"Subasta de {auctionName}"
            });
            Context.SaveChanges();
        }
    }
}
