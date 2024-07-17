public class DataSeeder
{
    public static void Seed(CinemaContext context)
    {
        if (!context.Filmes.Any())
        {
            var filmes = new List<Filme>
            {
                new Filme { Titulo = "A Origem", Genero = "Ficção Científica", Diretor = "Christopher Nolan", Duracao = 148, AnoLancamento = 2010 },
                new Filme { Titulo = "O Poderoso Chefão", Genero = "Crime", Diretor = "Francis Ford Coppola", Duracao = 175, AnoLancamento = 1972 },
                new Filme { Titulo = "Batman: O Cavaleiro das Trevas", Genero = "Ação", Diretor = "Christopher Nolan", Duracao = 152, AnoLancamento = 2008 },
                new Filme { Titulo = "Pulp Fiction: Tempo de Violência", Genero = "Drama", Diretor = "Quentin Tarantino", Duracao = 154, AnoLancamento = 1994 },
                new Filme { Titulo = "Interstellar", Genero = "Ficção Científica", Diretor = "Christopher Nolan", Duracao = 169, AnoLancamento = 2014 }
            };

            context.Filmes.AddRange(filmes);
            context.SaveChanges();
        }

        if (!context.Salas.Any())
        {
            var salas = new List<Sala>
            {
                new Sala { Id = 1, NomeSala = "Sala 01", Capacidade = 100 },
                new Sala { Id = 2, NomeSala = "Sala 02", Capacidade = 200 },
                new Sala { Id = 3, NomeSala = "Sala 03", Capacidade = 300 },
                new Sala { Id = 4, NomeSala = "Sala 04", Capacidade = 400 },
                new Sala { Id = 5, NomeSala = "Sala 05", Capacidade = 500 }
            };

            context.Salas.AddRange(salas);
            context.SaveChanges();
        }
    }
}