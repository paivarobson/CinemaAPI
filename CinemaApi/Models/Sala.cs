using System.Text.Json.Serialization;

namespace CinemaApi.Models
{
    public class Sala
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Descricao { get; set; } = string.Empty;
        [JsonIgnore]
        public List<SalaFilme> SalaFilmes { get; set; } = new List<SalaFilme>();
    }
}