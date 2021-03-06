//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
namespace OnlineNewspaperDistribution.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Subscribed
    {
        [Display(Name = "Subscription Id")]
        public int SubscriptionId { get; set; }

        [Display(Name = "User Id")]
        public int UserId { get; set; }

        [Display(Name = "Newspaper Id")]
        public int NewspaperId { get; set; }

        [Display(Name = "Newspaper Name")]
        public string NewspaperName { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public decimal Price { get; set; }

        [Display(Name = "Vendor Id")]
        public int VendorId { get; set; }

        [Display(Name = "Monthly Price")]
        public decimal MonthlyPrice { get; set; }

        [Display(Name = "Street Id")]
        public Nullable<int> StreetId { get; set; }

        [Display(Name = "DeleveryBoy Id")]
        public Nullable<int> DeleveryBoyId { get; set; }
    
        public virtual Subscribed Subscribed1 { get; set; }
        public virtual Subscribed Subscribed2 { get; set; }
    }
}
