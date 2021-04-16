using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Data.DB
{
    public partial class TranDungShopEntities : DbContext
    {
        public TranDungShopEntities()
            : base("name=TranDungShopEntities")
        {
        }

        public virtual DbSet<cartDetail> cartDetails { get; set; }
        public virtual DbSet<cart> carts { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<color> colors { get; set; }
        public virtual DbSet<customerType> customerTypes { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<news> news { get; set; }
        public virtual DbSet<orderDetail> orderDetails { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<product_categories> product_categories { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<rating> ratings { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<sale> sales { get; set; }
        public virtual DbSet<slide> slides { get; set; }
        public virtual DbSet<userRole> userRoles { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cartDetail>()
                .Property(e => e.totalPrices)
                .HasPrecision(18, 0);

            modelBuilder.Entity<cart>()
                .Property(e => e.totalPrices)
                .HasPrecision(18, 0);

            modelBuilder.Entity<cart>()
                .HasMany(e => e.cartDetails)
                .WithRequired(e => e.cart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<category>()
                .HasMany(e => e.product_categories)
                .WithOptional(e => e.category)
                .HasForeignKey(e => e.category_id);

            modelBuilder.Entity<color>()
                .Property(e => e.image_url)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.pass)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.avatar_url)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.carts)
                .WithRequired(e => e.customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.customer)
                .HasForeignKey(e => e.customer_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.ratings)
                .WithRequired(e => e.customer)
                .HasForeignKey(e => e.customer_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.userRoles)
                .WithRequired(e => e.customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<news>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<news>()
                .Property(e => e.imageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<orderDetail>()
                .Property(e => e.totalPrices)
                .HasPrecision(18, 0);

            modelBuilder.Entity<order>()
                .Property(e => e.total_price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<order>()
                .HasMany(e => e.orderDetails)
                .WithRequired(e => e.order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product_categories>()
                .HasMany(e => e.products)
                .WithRequired(e => e.product_categories)
                .HasForeignKey(e => e.product_category_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.price_start)
                .HasPrecision(18, 0);

            modelBuilder.Entity<product>()
                .Property(e => e.price_sale)
                .HasPrecision(18, 0);

            modelBuilder.Entity<product>()
                .Property(e => e.detail)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.slug)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.size)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.image_url)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.more_image)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.cartDetails)
                .WithRequired(e => e.product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.colors)
                .WithRequired(e => e.product)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.orderDetails)
                .WithRequired(e => e.product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.sales)
                .WithOptional(e => e.product)
                .HasForeignKey(e => e.product_id);

            modelBuilder.Entity<product>()
                .HasMany(e => e.ratings)
                .WithRequired(e => e.product)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<role>()
                .HasMany(e => e.userRoles)
                .WithRequired(e => e.role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<slide>()
                .Property(e => e.image_url)
                .IsUnicode(false);

            modelBuilder.Entity<slide>()
                .Property(e => e.slug)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.pass)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.news)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);
        }
    }
}
