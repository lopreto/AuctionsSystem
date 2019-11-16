using System.Net;
using System.Web.Mvc;
using AuctionsSystem.Infrastructure.Contexts;
using AuctionsSystem.Infrastructure.Enums;
using AuctionsSystem.Models;

namespace AuctionsSystem.Controllers
{
    public class AuctionsController : BaseController
    {
        private Context db = new Context();
        private AuctionModel _auctionModel;
        private TransactionModel _transactionModel;
        private WalletModel _walletModel;

        public AuctionsController()
        {
            _transactionModel = new TransactionModel();
            _auctionModel = new AuctionModel();
            _walletModel = new WalletModel();
        }

        public ActionResult Index()
        {
            return View(_auctionModel.SelectAuctionActive());
        }

        public ActionResult MyAuctions()
        {
            return View(_auctionModel.SelectParticipanteAuctions(CurrentUserId));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var auctionModel = _auctionModel.SelectById(id, CurrentUserId);

            if (auctionModel == null)
            {
                return HttpNotFound();
            }
            return View(auctionModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuctionModel auctionModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    auctionModel.Create(auctionModel, Server, CurrentUserId);

                    return RedirectToAction("MyAuctions");
                }
            }
            return View(auctionModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var auctionModel = _auctionModel.SelectById(id, CurrentUserId);
            if (auctionModel == null)
            {
                return HttpNotFound();
            }
            return View(auctionModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AuctionModel auctionModel)
        {
            if (ModelState.IsValid)
            {
                auctionModel.Update(auctionModel, Server, CurrentUserId);

                return RedirectToAction("MyAuctions");
            }
            return View(auctionModel);
        }

        [Authorize]
        public ActionResult AuctionsParticipate(int? id)
        {
            var auction = new AuctionModel();
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                auction = _auctionModel.SelectById(id, CurrentUserId);
                if (auction == null)
                {
                    return HttpNotFound();
                }
            }
            return View(auction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AuctionsParticipate(AuctionModel auctionModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    MessageType message;
                    var walletBalance = _walletModel.SelectByUserId(CurrentUserId).Balance;
                    if (auctionModel.ValidateBit(walletBalance, out message))
                    {
                        auctionModel.UpdateBestBid(auctionModel);
                        _transactionModel.Create(auctionModel, CurrentUserId);
                    }
                    else
                    {
                        CreateMessage(message);
                        return View(auctionModel);
                    }
                }
                return RedirectToAction("AuctionsParticipate");
            }

            return View(auctionModel);
        }
    }
}
