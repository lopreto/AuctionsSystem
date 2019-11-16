﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Web;

namespace AuctionsSystem.Models.Identity
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Alias")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recordarme?")]
        public bool RememberMe { get; set; }
        public string ImagePath { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surnames { get; set; }
        [Required]
        public string UserName { get; set; }
        [DisplayName("Imagem")]
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
