using Microsoft.EntityFrameworkCore;
using CinemaApi.Models;

namespace CinemaApi.Data
{
    public class CinemaContext : DbContext
{
    public CinemaContext(DbContextOptions<CinemaContext> options) : base(options) {}

    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Sala> Salas { get; set; }
    public DbSet<SalaFilme> SalaFilmes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SalaFilme>()
            .HasKey(sf => sf.Id);

        modelBuilder.Entity<SalaFilme>()
            .HasOne(sf => sf.Sala)
            .WithMany(s => s.SalaFilmes)
            .HasForeignKey(sf => sf.SalaId);

        modelBuilder.Entity<SalaFilme>()
            .HasOne(sf => sf.Filme)
            .WithMany(f => f.SalaFilmes)
            .HasForeignKey(sf => sf.FilmeId);

        base.OnModelCreating(modelBuilder);
    }
}
}