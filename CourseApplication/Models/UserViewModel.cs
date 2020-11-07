using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseApplication.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="Confirm password not match password")]
        public string ConfirmPassword { get; set; }

        public string Message { get; set; }

    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public string Message { get; set; }
    }
}