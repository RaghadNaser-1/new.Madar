using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MEP.Madar.Helper.Dto
{
    public  class UserRegisterDto
    {
        [StringLength(100)]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
