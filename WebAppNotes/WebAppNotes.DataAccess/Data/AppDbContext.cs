using Microsoft.EntityFrameworkCore;
using WebAppNotes.Data.Models;

namespace WebAppNotes.DataAccess.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        DbSet<Note> Notes {  get; set; }
        DbSet<Tag> Tags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Note>()
                .HasIndex(i => i.Id);

            modelBuilder.Entity<Tag>()
                .HasIndex(i => i.Id);
        }
    }
}
