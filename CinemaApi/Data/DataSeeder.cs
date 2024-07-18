public class DataSeeder
{
    public static void Seed(CinemaContext context)
    {
        if (!context.Filmes.Any())
        {
            var filmes = new List<Filme>
            {
                new Filme { Nome = "A Origem", Diretor = "Christopher Nolan", Duracao = 148, SalaId = 1 },
                new Filme { Nome = "O Poderoso Chefão", Diretor = "Francis Ford Coppola", Duracao = 175, SalaId = 1 },
                new Filme { Nome = "Batman: O Cavaleiro das Trevas", Diretor = "Christopher Nolan" },
                new Filme { Nome = "Pulp Fiction: Tempo de Violência", Diretor = "Quentin Tarantino", SalaId = 4 },
                new Filme { Nome = "Interstellar", Diretor = "Christopher Nolan", Duracao = 169, SalaId = 5 }
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
    }
}