using AuctionsSystem.Infrastructure.Contexts;
using AuctionsSystem.Infrastructure.Enums;
using System.Linq;

namespace AuctionsSystem.Models
{
    public class BaseModel
    {
        protected Context _context;
        public UserModel CurrentUser { get; }
        public BaseModel()
        {
            _context = new Context();
        }

        public BaseModel(string currentUserId)
        {
            _context = new Context();
            CurrentUser = new UserModel();

            var user = _context.Users.Where(u => u.UserName == currentUserId).FirstOrDefault();
            CurrentUser.Id = user.Id;
            CurrentUser.Name = user.Name;
            CurrentUser.Surnames = user.Surnames;
            CurrentUser.Email = user.Email;
            CurrentUser.UserName = user.UserName;
            CurrentUser.ImagePath = user.ImagePath;
        }

        public string CreateMessage(MessageType? message)
        {
            return message == MessageType.ChangePasswordSuccess ? "Su password ha sido cambiada."
                : message == MessageType.Error ? "An error has occurred."
                : message == MessageType.ChangeEmailSuccess ? "Su correo electrónico ha sido cambiado."
                : message == MessageType.ChangeImageSuccess ? "Su imagem ha sido cambiado."
                : "";
        }
    }
}