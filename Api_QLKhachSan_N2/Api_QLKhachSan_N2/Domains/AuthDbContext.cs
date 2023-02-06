using Microsoft.EntityFrameworkCore;

namespace Api_QLKhachSan_N2.Domains
{
    public class AuthDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình khóa ngoại bảng user và freshtoken
            modelBuilder.Entity<RefreshToken>().HasOne(t => t.User).WithMany(t => t.RefreshTokens).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.ClientCascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}

