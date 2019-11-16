using System.Linq;

namespace AuctionsSystem.Models
{
    public class UserModel : BaseModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surnames { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }

        public UserModel()
        {
        }

        public UserModel(string currentUserId):base(currentUserId)
        {
            var user = _context.Users.Where(u => u.UserName == currentUserId).FirstOrDefault();
            Id = user.Id;
            Name = user.Name;
            Surnames = user.Surnames;
            Email = user.Email;
            UserName = user.UserName;
            ImagePath = user.ImagePath;
        }
    }
}
