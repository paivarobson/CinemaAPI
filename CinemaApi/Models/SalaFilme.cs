namespace CinemaApi.Models
{
    public class SalaFilme
    {
        public int Id { get; set; }
        public int SalaId { get; set; }
        public Sala Sala { get; set; } = null!;
        public int FilmeId { get; set; }
        public Filme Filme { get; set; } = null!;
    }
}