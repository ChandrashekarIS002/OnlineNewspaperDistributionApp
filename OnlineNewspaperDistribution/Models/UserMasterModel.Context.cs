﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NewspaperEntities1 : DbContext
    {
        public NewspaperEntities1()
            : base("name=NewspaperEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<StreetMaster> StreetMasters { get; set; }
        public virtual DbSet<UserTypeMaster> UserTypeMasters { get; set; }
        public virtual DbSet<NewspaperMaster> NewspaperMasters { get; set; }
        public virtual DbSet<SubscriptionDetail> SubscriptionDetails { get; set; }
        public virtual DbSet<UserMaster> UserMasters { get; set; }
        public virtual DbSet<Subscribed> Subscribeds { get; set; }
        public virtual DbSet<UseDetail> UseDetails { get; set; }
        public virtual DbSet<BillMaster> BillMasters { get; set; }
        public virtual DbSet<BillDetail> BillDetails { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
    }
}
