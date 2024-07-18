public class Cinema
{
    public int Id { get; set; }
    public int SalaId { get; set; }
    public Sala? Sala { get; set; }
    public int FilmeId { get; set; }
    public Filme? Filme { get; set; }
}