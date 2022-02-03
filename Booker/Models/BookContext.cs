using Microsoft.EntityFrameworkCore;

namespace Booker.Models
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Books> Books { get; set; }
    }
}
