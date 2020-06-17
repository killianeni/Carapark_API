using KMAP_API.Models;
using Microsoft.EntityFrameworkCore;

namespace KMAP_API.Data
{
    public class KmapContext : DbContext
    {
        public KmapContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}
