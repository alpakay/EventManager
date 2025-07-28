using EventManager.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class RepositoryContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Participant> Participants { get; set; }

    public RepositoryContext(DbContextOptions<RepositoryContext> options)
    : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .HasData(
                new Category { CategoryId = 1, CategoryName = "Spor" },
                new Category { CategoryId = 2, CategoryName = "Müzik" },
                new Category { CategoryId = 3, CategoryName = "Sinema" }
            );
        
        modelBuilder.Entity<Event>()
        .HasData(
            new Event
            {
                EventId = 1,
                Name = "Halısaha",
                Description = "Çfl Halısahası",
                CreatedAt = new DateTime(2025, 07, 28, 0, 0, 0),
                ImgUrl = "https://example.com/image.jpg",
                StartDate = new DateTime(2025, 07, 29, 0, 0, 0),
                EndDate = new DateTime(2025, 07, 30, 0, 0, 0),
                ParticipantCount = 0,
                MaxParticipants = 22,
                CategoryId = 1
            },
            new Event
            {
                EventId = 2,
                Name = "Müzik Festivali",
                Description = "Yaz Festivali",
                CreatedAt = new DateTime(2025, 07, 28, 0, 0, 0),
                ImgUrl = "https://example.com/image.jpg",
                StartDate = new DateTime(2025, 07, 29, 0, 0, 0),
                EndDate = new DateTime(2025, 07, 30, 0, 0, 0),
                ParticipantCount = 0,
                MaxParticipants = 200,
                CategoryId = 2
            },
            new Event
            {
                EventId = 3,
                Name = "F1 Filmi",
                Description = "F1 Filmi Açılışı",
                CreatedAt = new DateTime(2025, 07, 28, 0, 0, 0),
                ImgUrl = "https://example.com/image.jpg",
                StartDate = new DateTime(2025, 07, 29, 0, 0, 0),
                EndDate = new DateTime(2025, 07, 30, 0, 0, 0),
                ParticipantCount = 0,
                MaxParticipants = 120,
                CategoryId = 3
            }
        );

        // modelBuilder.ApplyConfiguration(new ProductConfig());
        // modelBuilder.ApplyConfiguration(new CategoryConfig());

        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
