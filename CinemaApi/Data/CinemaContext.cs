using Microsoft.EntityFrameworkCore;

public class CinemaContext : DbContext
{
    public CinemaContext(DbContextOptions<CinemaContext> options) : base(options) {}

    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Sala> Salas { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
}