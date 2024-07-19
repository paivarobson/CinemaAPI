using CinemaApi.Models;

namespace CinemaApi.Data
{
    public class DataSeeder
{
    public static void Seed(CinemaContext context)
    {
        if (!context.Filmes.Any())
        {
            var filmes = new List<Filme>
            {
                new Filme { Nome = "A Origem", Diretor = "Christopher Nolan", Duracao = 148 },
                new Filme { Nome = "O Poderoso Chefão", Diretor = "Francis Ford Coppola", Duracao = 175 },
                new Filme { Nome = "Batman: O Cavaleiro das Trevas", Diretor = "Christopher Nolan", Duracao = 190 },
                new Filme { Nome = "Pulp Fiction: Tempo de Violência", Diretor = "Quentin Tarantino", Duracao = 120 },
                new Filme { Nome = "Interstellar", Diretor = "Christopher Nolan", Duracao = 169 }
            };

            context.Filmes.AddRange(filmes);
            context.SaveChanges();
        }

        if (!context.Salas.Any())
        {
            var salas = new List<Sala>
            {
                new Sala { Numero = 1, Descricao = "Sala 01" },
                new Sala { Numero = 2, Descricao = "Sala 02" },
                new Sala { Numero = 3, Descricao = "Sala 03" },
                new Sala { Numero = 4, Descricao = "Sala 04" },
                new Sala { Numero = 5, Descricao = "Sala 05" }
            };

            context.Salas.AddRange(salas);
            context.SaveChanges();
        }

        if (!context.SalaFilmes.Any())
        {
            var salas = context.Salas.ToList();
            var filmes = context.Filmes.ToList();

            var salaFilmes = new List<SalaFilme>
            {
                new SalaFilme { SalaId = salas[0].Id, FilmeId = filmes[0].Id },
                new SalaFilme { SalaId = salas[0].Id, FilmeId = filmes[1].Id },
                new SalaFilme { SalaId = salas[1].Id, FilmeId = filmes[1].Id },
                new SalaFilme { SalaId = salas[1].Id, FilmeId = filmes[2].Id },
                new SalaFilme { SalaId = salas[1].Id, FilmeId = filmes[2].Id },
                new SalaFilme { SalaId = salas[2].Id, FilmeId = filmes[3].Id },
                new SalaFilme { SalaId = salas[2].Id, FilmeId = filmes[4].Id }
            };

            context.SalaFilmes.AddRange(salaFilmes);
            context.SaveChanges();
        }
    }
}

}