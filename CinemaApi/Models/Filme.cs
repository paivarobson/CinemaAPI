using System.Text.Json.Serialization;

public class Filme
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Diretor { get; set; } = string.Empty;
    public int Duracao { get; set; }
    public List<SalaFilme> SalaFilmes { get; set; } = new List<SalaFilme>();
}