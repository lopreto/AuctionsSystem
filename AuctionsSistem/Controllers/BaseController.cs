using AuctionsSystem.Infrastructure.Contexts;
using AuctionsSystem.Infrastructure.Enums;
using AuctionsSystem.Models;
using System.Web.Mvc;

namespace AuctionsSystem.Controllers
{
    public class BaseController : Controller
    {
        protected Context _context;
        public string CurrentUserId { get { return new UserModel(System.Web.HttpContext.Current.User.Identity.Name).Id; } }

        public BaseController()
        {
            _context = new Context();
        }
        public void CreateMessage(MessageType? message)
        {
            ViewBag.StatusMessage = message == MessageType.ChangePasswordSuccess ? "Su password ha sido cambiada."
                : message == MessageType.Error ? "Se ocorrió un error."
                : message == MessageType.BestBid ? "Su puja tiene que ser mas grande."
                : message == MessageType.LoginError ? "Inicio de sesión incorrecto."
                : message == MessageType.WithoutBalance ? "Sin saldo para la operación."
                : "";
        }
    }
}