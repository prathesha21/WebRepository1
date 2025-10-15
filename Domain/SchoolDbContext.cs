using Microsoft.EntityFrameworkCore;
using WebRepository1.Models.Entities;

namespace WebRepository1.Domain
{
    public class SchoolDbContext:DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) 
        { 
        }
        public DbSet<SchoolDetailscs> StudentSchool { get; set; }
    }
    
}
