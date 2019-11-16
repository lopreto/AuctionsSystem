using AuctionsSystem.Infrastructure.Entities;
using System.Data.Entity;

namespace AuctionsSystem.Infrastructure.Contexts
{
    public class Context : DbContext
    {
        public Context() : base("DefaultConnection")
        {
            
        }

        public DbSet<Auction> Auctions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletHistory> WalletsHistory { get; set; }
    }
}
