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
    
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Ad> Ad { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Cupon> Cupon { get; set; }
        public virtual DbSet<Inst_Group> Inst_Group { get; set; }
        public virtual DbSet<Institution> Institution { get; set; }
        public virtual DbSet<Manager> Manager { get; set; }
        public virtual DbSet<Manager_Institution_maps> Manager_Institution_maps { get; set; }
        public virtual DbSet<Subscribable> Subscribable { get; set; }
        public virtual DbSet<Subscription> Subscription { get; set; }
        public virtual DbSet<Ad_Photo_maps> Ad_Photo_maps { get; set; }
        public virtual DbSet<Institution_Photo_maps> Institution_Photo_maps { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<searchable_ad> searchable_ad { get; set; }
    }
}
