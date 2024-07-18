using Microsoft.EntityFrameworkCore;

public class CinemaContext : DbContext
{
    public CinemaContext(DbContextOptions<CinemaContext> options) : base(options) {}

    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Sala> Salas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sala>()
            .HasMany(s => s.Filmes)
            .WithOne(f => f.Sala)
            .HasForeignKey(f => f.SalaId)
            .OnDelete(DeleteBehavior.SetNull);

        base.OnModelCreating(modelBuilder);
    }
}