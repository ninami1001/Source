using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Zadatak1.Models
{
    public partial class Model_Zadatak : DbContext
    {

       
        public Model_Zadatak()
            : base("name=Model_Zadatak")
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Geo> Geo { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Address)
                .HasForeignKey(e => e.Adress);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Company1)
                .HasForeignKey(e => e.Company);

            modelBuilder.Entity<Geo>()
                .HasMany(e => e.Address)
                .WithOptional(e => e.Geo1)
                .HasForeignKey(e => e.Geo);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
