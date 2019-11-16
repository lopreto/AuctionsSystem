using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AuctionsSystem.Infrastructure.Entities
{
    [Table("Wallet")]
    public class Wallet
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal Balance { get; set; }
    }
}