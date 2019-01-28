using Microsoft.EntityFrameworkCore;

namespace WeatherStore.Models
{
    public class ReadingContext : DbContext
    {
        public ReadingContext(DbContextOptions<ReadingContext> options)
            : base(options)
        { }

        public DbSet<Reading> Readings { get; set; }
    }
}