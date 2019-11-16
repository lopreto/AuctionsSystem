using System.Web.Mvc;
using AuctionsSystem.Models;

namespace AuctionsSystem.Controllers
{
    public class WalletsController : BaseController
    {
        private WalletModel _wallet;
        private WalletHistoryModel _walletHistory;

        public WalletsController()
        {
            _wallet = new WalletModel();
            _walletHistory = new WalletHistoryModel();
        }

        public ActionResult Index()
        {
            var wallet = _wallet.SelectByUserId(CurrentUserId);

            return View(wallet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBalance(WalletModel walletModel)
        {
            walletModel.AddBalance(CurrentUserId);
            _walletHistory.CreateBalanceHistory(walletModel);

            return RedirectToAction("Index"); 
        }
    }
}
