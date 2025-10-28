using quest5.Models;
using Microsoft.EntityFrameworkCore;

namespace quest5.DataFolder
{  
    public class InMemoryData : DbContext
    {
        public InMemoryData(DbContextOptions<InMemoryData> options)
            : base(options)
        {
        }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne()
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Agata Kristy", DateOfBirth = new DateTime(1890, 9, 15) },
                new Author { Id = 2, Name = "Fyodor Dostoevsky", DateOfBirth = new DateTime(1821, 11, 11) }
            );

            modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Secret Adversary", PublishedYear = 1922, AuthorId = 1 },
            new Book { Id = 2, Title = "Postern of Fate", PublishedYear = 1973, AuthorId = 1 },
            new Book { Id = 3, Title = "Crime and Punishment", PublishedYear = 1866, AuthorId = 2 }
            );
        }
    }
}
