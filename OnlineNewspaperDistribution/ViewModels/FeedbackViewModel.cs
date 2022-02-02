using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineNewspaperDistribution.ViewModels
{
    public class FeedbackViewModel
    {
        public int FeedbackId { get; set; }

        [Display(Name = "User Name : ")]
        [Required(ErrorMessage = "User Name is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter User Name.")]
        public string UserName { get; set; }

        [Display(Name = "EmailId : ")]
        [Required(ErrorMessage = "EmailId is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter EmailId address.")]
        public string EmailId { get; set; }

        [Display(Name = "Feedback : ")]
        //[Required(ErrorMessage = "Feedback1 is required.")]
        public string Feedback1 { get; set; }
    }
}