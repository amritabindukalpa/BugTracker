using DefectTracker.Models;
using System.Data.Entity;

namespace DefectTracker.DataAccessLayer
{
    public class DefectTrackerContext : DbContext
    {
        public DefectTrackerContext() : base("DefectTrackerContext")
        { }

        public DbSet<DefectStatus> DefectStatus { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Defect> Defects { get; set; }
    }
}