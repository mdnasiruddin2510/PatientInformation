using Microsoft.EntityFrameworkCore;

namespace PatientInformation.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<NCD> NCD { get; set; }
    }
}
