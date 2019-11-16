using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace AuctionsSystem.Models.Identity
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public string Email { get; set; }
        public string Name { get; internal set; }
    }
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contrasenã actual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contrasenã")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nueva contrasenã")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class ChangeEmailViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Nuevo email")]
        public string NuevoEmail { get; set; }

        [EmailAddress]
        [Display(Name = "Confirme su nuevo email")]
        [Compare("NuevoEmail", ErrorMessage = "Los datos no estan correctos")]
        public string ConfirmeEmail { get; set; }
    }

    public class ChangeImageUserViewModel
    {
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}