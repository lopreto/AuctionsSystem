using AuctionsSystem.Infrastructure.Entities;
using AuctionsSystem.Infrastructure.Enums;
using AuctionsSystem.Infrastructure.Helpers;
using Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AuctionsSystem.Models
{
    public class AuctionModel : BaseModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nombre del producto")]
        public string Name { get; set; }
        [Display(Name = "Comentarios")]
        public string Comments { get; set; }
        [Display(Name = "Imagen del Producto")]
        public string ImagePath { get; set; }
        [NotMapped]
        [Display(Name = "Imagen del Producto")]
        public HttpPostedFileBase ImageFile { get; set; }
        [Display(Name = "Precio")]
        public decimal Price { get; set; }
        [Display(Name = "Fecha de Inicio")]
        [DataType(DataType.DateTime)]
        public DateTime InitialDate { get; set; }
        [Display(Name = "Fecha de Fin")]
        [DataType(DataType.DateTime)]
        public DateTime FinalDate { get; set; }
        [Display(Name = "Activo")]
        public bool Active { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Mejor Puja")]
        public decimal BestBid { get; set; }
        [Display(Name = "Mi Puja")]
        public decimal Bid { get; set; }
        public AuctionParticipantType AuctionParticipantType { get; set; }

        public List<AuctionModel> Auctions { get; set; }
        public TransactionModel _transactionModel;

        public AuctionModel()
        {
            Auctions = new List<AuctionModel>();
            _transactionModel = new TransactionModel();
        }

        private List<AuctionModel> Select()
        {
            var auctions = _context.Auctions.ToList().OrderByDescending(a => a.FinalDate);

            foreach (var auction in auctions)
            {
                Auctions.Add(new AuctionModel
                {
                    Id = auction.Id,
                    Name = auction.Name,
                    Comments = auction.Comments,
                    Price = auction.Price,
                    ImagePath = auction.ImagePath,
                    UserId = auction.UserId,
                    BestBid = auction.BestBid,
                    Active = auction.Active,
                    InitialDate = auction.InitialDate,
                    FinalDate = auction.FinalDate,
                });
            }
            return Auctions;
        }
        public List<AuctionModel> SelectAuctionActive()
        {
            return Select().Where(a => a.Active).ToList();
        }

        public List<AuctionModel> SelectParticipanteAuctions(string currentUserId)
        {
            var transactions = _transactionModel.Select();
            var auctions = Select();

            var myAuctions = auctions.Where(a => a.UserId == currentUserId).ToList();

            myAuctions.ForEach(m =>
            {
                m.AuctionParticipantType = AuctionParticipantType.Vendedor;
            });

            //var buyerTransactions = transactions.Where(t => t.BuyerId == currentUserId && t.SellerId != currentUserId).OrderByDescending(t => t.Bid).ToList();

            myAuctions = FillAuctionBid(myAuctions, transactions);

            var auctionsFilled = FillParticipantType(auctions.Where(a => a.UserId != currentUserId).ToList(), transactions, currentUserId);

            auctionsFilled.ForEach(a => myAuctions.Add(a));

            return myAuctions;
        }

        public AuctionModel SelectById(int? auctionId, string currentUserId)
        {
            var auctions = Select();
            var transactions = _transactionModel.Select();
            auctions = FillAuctionBid(auctions, transactions);
            var auctionsFilled = FillParticipantType(auctions, transactions, currentUserId);
            auctionsFilled.ForEach(a => auctions.Add(a));
            return auctions.Where(a => a.Id == auctionId).FirstOrDefault();
        }

        public List<AuctionModel> FillParticipantType(List<AuctionModel> auctions, List<TransactionModel> transactions, string currentUserId)
        {
            var listTemp = new List<AuctionModel>();

            auctions.ForEach(a =>
            {
                var transaction = transactions.Where(t => t.AuctionId == a.Id && t.BuyerId == currentUserId && t.SellerId != currentUserId).OrderByDescending(o => o.Bid).FirstOrDefault();

                if (transaction != null)
                {
                    if (transaction.TransactionContemplated)
                    {
                        a.AuctionParticipantType = AuctionParticipantType.Comprador;
                        listTemp.Add(a);
                    }
                    else
                    {
                        a.AuctionParticipantType = AuctionParticipantType.Participante;
                        listTemp.Add(a);
                    }
                }
            });

            return listTemp;
        }

        public List<AuctionModel> FillAuctionBid(List<AuctionModel> auctions, List<TransactionModel> transactions)
        {
            auctions.ForEach(a =>
            {
                var b = transactions.OrderByDescending(t => t.Bid).ToList().Where(t => t.AuctionId == a.Id).FirstOrDefault();
                a.Bid = b != null ? b.Bid : 0;
            });

            return auctions.ToList();
        }

        public void Create(AuctionModel auctionModel, HttpServerUtilityBase server, string currentUserId)
        {
            _context.Auctions.Add(new Auction
            {
                Id = auctionModel.Id,
                Name = auctionModel.Name,
                Comments = auctionModel.Comments,
                Price = auctionModel.Price,
                ImagePath = ImagemHelper.Save(ImageType.Productos, server, auctionModel.ImageFile),
                UserId = currentUserId,
                BestBid = auctionModel.BestBid,
                Active = true,
                InitialDate = auctionModel.InitialDate,
                FinalDate = auctionModel.FinalDate
            });
            _context.SaveChanges();
        }

        public void UpdateBestBid(AuctionModel auctionModel)
        {
            var auction = _context.Auctions.ToList().Where(a => a.Id == auctionModel.Id).FirstOrDefault();
            auction.BestBid = auctionModel.Bid;
            _context.Entry(auction).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Update(AuctionModel auctionModel, HttpServerUtilityBase server, string currentUserId)
        {
            var auction = _context.Auctions.ToList().Where(a => a.Id == auctionModel.Id).FirstOrDefault();

            auction.Name = auctionModel.Name;
            auction.Price = auctionModel.Price;
            auction.UserId = currentUserId;
            auction.InitialDate = auctionModel.InitialDate;
            auction.FinalDate = auctionModel.FinalDate;
            auction.Comments = auctionModel.Comments;
            auction.BestBid = auctionModel.BestBid;
            auction.Active = auctionModel.Active;
            auction.ImagePath = ImagemHelper.Save(ImageType.Productos, server, auctionModel.ImageFile);

            _context.Entry(auction).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool ValidateBit(decimal walletBalance, out MessageType message)
        {
            var actualAuctionModel = _context.Auctions.Find(Id);
            if (actualAuctionModel.BestBid > Bid && actualAuctionModel.Price > Bid)
            {
                message = MessageType.BestBid;
                return false;
            }
            else if (walletBalance < Bid)
            {
                message = MessageType.WithoutBalance;
                return false;
            }
            else
            {
                message = MessageType.Success;
                return true;
            }

        }
    }
}