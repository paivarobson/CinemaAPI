public class Sala
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public List<Filme> Filmes { get; set; } = new List<Filme>();
}