﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace web_levent.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LeventEntities : DbContext
    {
        public LeventEntities()
            : base("name=LeventEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdminUser> AdminUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Color_De> Color_De { get; set; }
        public virtual DbSet<Detail> Details { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Size_De> Size_De { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}