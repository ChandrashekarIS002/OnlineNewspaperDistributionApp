//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineNewspaperDistribution.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BillMaster
    {
        public int BillId { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public int LastEditedBy { get; set; }
        public Nullable<System.DateTime> LastEditedDateTime { get; set; }
    }
}