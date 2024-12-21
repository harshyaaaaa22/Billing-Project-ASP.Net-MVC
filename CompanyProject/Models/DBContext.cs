using System.Data.Entity; // For Entity Framework

namespace CompanyProject.Models
{
    public class DBContext : DbContext
    {
        // Constructor with connection string name
        public DBContext() : base("name=MyConnection")
        {
        }

        // DbSets for your models
        public DbSet<CompanyDetails> CompanyDetails { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }

        // Fluent API configuration
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure CompanyDetails table
            modelBuilder.Entity<CompanyDetails>()
                .HasKey(c => c.CompanyID)
                .Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(100);

            // Configure ProductDetails table
            modelBuilder.Entity<ProductDetails>()
                .HasKey(p => p.ProductID)
                .Property(p => p.ProductCode)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
