using Microsoft.EntityFrameworkCore;
using WebAppNotes.Data.Models;

namespace WebAppNotes.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Note>()
                .HasIndex(i => i.Id);

            modelBuilder.Entity<Note>()
                .Property(n => n.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Tag>()
                .HasIndex(i => i.Id);

            modelBuilder.Entity<Tag>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
