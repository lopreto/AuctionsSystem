using AuctionsSystem.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AuctionsSystem.Models
{
    public class WalletHistoryModel : BaseModel
    {
        public int Id { get; set; }
        public int BuyerWalletId { get; set; }
        public int SellerWalletId { get; set; }
        public int TransactionId { get; set; }
        [Display(Name = "Fecha de Movimentación")]
        public DateTime TransactionDate { get; set; }
        [Display(Name = "Valor de Movimentacíon")]
        public decimal TransactionValue { get; set; }
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        public int WalletId { get; set; }
        public int WalletIdUserLogged { get; set; }
        
        public List<WalletHistoryModel> WalletsHistory { get; set; }

        public WalletHistoryModel()
        {
            WalletsHistory = new List<WalletHistoryModel>();
        }

        public void CreateBalanceHistory(WalletModel walletModel)
        {
            _context.WalletsHistory.Add(new WalletHistory
            {
                TransactionValue = walletModel.HandlingValue,
                TransactionDate = DateTime.Now,
                Description = walletModel.HandlingValue > 0 ? "Entrada de saldo." : "Salida de saldo.",
                WalletId = walletModel.Id
            });

            _context.SaveChanges();
        }
    }
}