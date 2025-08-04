using System.Security.Cryptography;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class RepositoryContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Key> Keys { get; set; }

    public RepositoryContext(DbContextOptions<RepositoryContext> options)
    : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Key>().HasData(
            new Key { KeyId = 1, KeyValue = "z4sbUQxWv/fbiQv4OnQYzjTwcbNTe9I9KR2DZhBPUrQ=" }
        );
        
        // modelBuilder.Entity<Event>()
        // .HasData(
        //     new Event
        //     {
        //         EventId = 1,
        //         Name = "Halısaha",
        //         Description = "Çfl Halısahası",
        //         CreatedAt = new DateTime(2025, 07, 28, 0, 0, 0),
        //         ImgUrl = "https://example.com/image.jpg",
        //         StartDate = new DateTime(2025, 07, 29, 0, 0, 0),
        //         EndDate = new DateTime(2025, 07, 30, 0, 0, 0),
        //         CategoryId = 1
        //     },
        //     new Event
        //     {
        //         EventId = 2,
        //         Name = "Müzik Festivali",
        //         Description = "Yaz Festivali",
        //         CreatedAt = new DateTime(2025, 07, 28, 0, 0, 0),
        //         ImgUrl = "https://example.com/image.jpg",
        //         StartDate = new DateTime(2025, 07, 29, 0, 0, 0),
        //         EndDate = new DateTime(2025, 07, 30, 0, 0, 0),
        //         CategoryId = 2
        //     },
        //     new Event
        //     {
        //         EventId = 3,
        //         Name = "F1 Filmi",
        //         Description = "F1 Filmi Açılışı",
        //         CreatedAt = new DateTime(2025, 07, 28, 0, 0, 0),
        //         ImgUrl = "https://example.com/image.jpg",
        //         StartDate = new DateTime(2025, 07, 29, 0, 0, 0),
        //         EndDate = new DateTime(2025, 07, 30, 0, 0, 0),
        //         CategoryId = 3
        //     }
        // );
    }
}
