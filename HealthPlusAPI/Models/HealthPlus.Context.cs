﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HealthPlusAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class healthplusEntities : DbContext
    {
        public healthplusEntities()
            : base("name=healthplusEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Acccount> Acccounts { get; set; }
        public virtual DbSet<Ad> Ads { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Cupon> Cupons { get; set; }
        public virtual DbSet<Inst_Group> Inst_Group { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Manager_Institution_maps> Manager_Institution_maps { get; set; }
        public virtual DbSet<Subscribable> Subscribables { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
    }
}