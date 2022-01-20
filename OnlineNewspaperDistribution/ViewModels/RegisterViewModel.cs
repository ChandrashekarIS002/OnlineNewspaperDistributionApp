using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineNewspaperDistribution.ViewModels
{
    public class RegisterViewModel
    {

        ////public string UserId { get; set; }

        //[Display(Name = "User name : ")]
        //[Required(ErrorMessage = "User name is required.")]
        //public string UserName { get; set; }

        //[Display(Name = "EmailId : ")]
        //[Required(ErrorMessage = "EmailId is required.")]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Please enter EmailId address.")]
        //public string EmailId { get; set; }

        ////public string UserSalt { get; set; }
        ////public int UserTypeId { get; set; }

        //[Display(Name = "Password : ")]
        //[Required(ErrorMessage = "Password is required.")]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        //[Display(Name = "Confirm Password : ")]
        //[Required(ErrorMessage = "Confirm Password is required.")]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Password and ConfirmPassword should be same.")]
        //public string ConfirmPassword { get; set; }

        [Display(Name = "User Name : ")]
        [Required(ErrorMessage = "User Name is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter User Name.")]
        public string UserName { get; set; }

        [Display(Name = "EmailId : ")]
        [Required(ErrorMessage = "EmailId is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter EmailId address.")]
        public string EmailId { get; set; }

        [Display(Name = "Password : ")]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password : ")]
        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword should be same.")]
        public string ConfirmPassword { get; set; }
    }
}