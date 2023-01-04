using _0101.Models;
using Microsoft.EntityFrameworkCore;

namespace _0101.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
   
        public DbSet<Student> Students {get; set;}
        public DbSet<_0101.Models.Customer>? Customer { get; set; }
        public DbSet<_0101.Models.Employee>? Employee { get; set; }
        public DbSet<_0101.Models.Person>? Person { get; set; }
    }
}