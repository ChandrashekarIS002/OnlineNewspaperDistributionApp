using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineNewspaperDistribution.ViewModels
{
    public class UserLoginViewModel
    {
        [Display(Name = "EmailId : ")]
        [Required(ErrorMessage = "EmailId is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter EmailId address.")]
        public string EmailId { get; set; }

        [Display(Name = "Password : ")]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}