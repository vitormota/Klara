namespace WebClient.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HealthPlusDB : DbContext
    {
        public HealthPlusDB()
            : base("name=HealthPlusDB")
        {
        }

        public virtual DbSet<Acccount> Acccount { get; set; }
        public virtual DbSet<Ad> Ad { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Cupon> Cupon { get; set; }
        public virtual DbSet<Inst_Group> Inst_Group { get; set; }
        public virtual DbSet<Institution> Institution { get; set; }
        public virtual DbSet<Manager> Manager { get; set; }
        public virtual DbSet<Manager_Institution_maps> Manager_Institution_maps { get; set; }
        public virtual DbSet<Subscribable> Subscribable { get; set; }
        public virtual DbSet<Subscription> Subscription { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Acccount>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<Acccount>()
                .Property(e => e.currency)
                .IsUnicode(false);

            modelBuilder.Entity<Ad>()
                .Property(e => e.service)
                .IsUnicode(false);

            modelBuilder.Entity<Ad>()
                .Property(e => e.specialty)
                .IsUnicode(false);

            modelBuilder.Entity<Ad>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Ad>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.phone_number)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.nif)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Group>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Group>()
                .Property(e => e.website)
                .IsUnicode(false);

            modelBuilder.Entity<Institution>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Institution>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<Institution>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<Institution>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Institution>()
                .Property(e => e.website)
                .IsUnicode(false);

            modelBuilder.Entity<Institution>()
                .Property(e => e.phone_number)
                .IsUnicode(false);

            modelBuilder.Entity<Institution>()
                .Property(e => e.fax)
                .IsUnicode(false);

            modelBuilder.Entity<Manager>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<Manager>()
                .Property(e => e.password)
                .IsUnicode(false);
        }
    }
}
