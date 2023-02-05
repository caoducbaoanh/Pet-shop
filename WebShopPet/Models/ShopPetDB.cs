namespace WebShopPet.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShopPetDB : DbContext
    {
        public ShopPetDB() : base("name=ShopPetDB2")
        {
        }

        public virtual DbSet<BRAND> BRANDs { get; set; }
        public virtual DbSet<CATEGORy> CATEGORIES { get; set; }
        public virtual DbSet<ORDER_DETAILS> ORDER_DETAILS { get; set; }
        public virtual DbSet<ORDER> ORDERS { get; set; }
        public virtual DbSet<PRODUCT> PRODUCTS { get; set; }
        public virtual DbSet<USER> USERS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BRAND>()
                .Property(e => e.PHONE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BRAND>()
                .HasMany(e => e.PRODUCTS)
                .WithRequired(e => e.BRAND)
                .HasForeignKey(e => e.BRAND_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CATEGORy>()
                .HasMany(e => e.PRODUCTS)
                .WithRequired(e => e.CATEGORy)
                .HasForeignKey(e => e.CATEGORY_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDER>()
                .Property(e => e.PHONE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ORDER>()
                .HasMany(e => e.ORDER_DETAILS)
                .WithRequired(e => e.ORDER)
                .HasForeignKey(e => e.ORDER_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRODUCT>()
                .HasMany(e => e.ORDER_DETAILS)
                .WithRequired(e => e.PRODUCT)
                .HasForeignKey(e => e.PRODUCT_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.PASSWORD)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.AVATAR)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.ORDERS)
                .WithRequired(e => e.USER)
                .HasForeignKey(e => e.USER_ID)
                .WillCascadeOnDelete(false);
        }
    }
}
