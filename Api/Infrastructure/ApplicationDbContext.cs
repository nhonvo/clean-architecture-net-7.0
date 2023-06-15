using Api.Core.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Api.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context)
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}