using Microsoft.EntityFrameworkCore;

namespace PatientInformation.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<NCD> NCD { get; set; }
        public DbSet<Allergies> Allergies { get; set; }
        public DbSet<Disease> Disease { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<NCD_Details> NCD_Details { get; set; }
        public DbSet<Allergies_Details> Allergies_Details { get; set; }
    }
}
