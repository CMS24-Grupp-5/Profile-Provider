using Microsoft.EntityFrameworkCore;

namespace Profile.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<UserProfileEntity> UserProfiles { get; set; } = null!;  
    }
}
