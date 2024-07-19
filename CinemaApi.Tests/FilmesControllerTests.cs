using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaApi.Controllers;
using CinemaApi.Data;
using CinemaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CinemaApi.Tests
{
    [TestClass]
    public class FilmesControllerBasicTest
    {
        private FilmesController _controller = null!;
        private CinemaContext _context = null!;

        [TestInitialize]
        public void Inicialize()
        {
            var opcoes = new DbContextOptionsBuilder<CinemaContext>()
                .UseInMemoryDatabase(databaseName: "cinema_db_Test")
                .Options;

            _context = new CinemaContext(opcoes);
            _controller = new FilmesController(_context);
        }

        [TestMethod]
        public async Task AdicionarEConsultarFilme()
        {
            var filme = new Filme { Nome = "Filme Teste", Diretor = "Diretor Teste", Duracao = 120 };

            var resultadoPost = await _controller.PostFilme(filme);
            var resultadoCriado = resultadoPost.Result as CreatedAtActionResult;
            var idFilme = (resultadoCriado?.Value as Filme)?.Id;

            Assert.IsNotNull(idFilme, "O ID do filme não deve ser nulo.");

            var resultadoGet = await _controller.GetFilme(idFilme.Value);
            var resultadoOk = resultadoGet.Result as OkObjectResult;
            var filmeRecuperado = resultadoOk?.Value as Filme;

            Assert.IsNotNull(filmeRecuperado, "O filme recuperado não deve ser nulo.");
            Assert.AreEqual("Filme Teste", filmeRecuperado?.Nome, "O nome do filme não corresponde.");
            Assert.AreEqual("Diretor Teste", filmeRecuperado?.Diretor, "O diretor do filme não corresponde.");
            Assert.AreEqual(120, filmeRecuperado?.Duracao, "A duração do filme não corresponde.");
        }

        [TestMethod]
        public async Task ListarFilmes_RetornaResultadoOk_ComListaDeFilmes()
        {
            var filme1 = new Filme { Nome = "Filme Teste 1", Diretor = "Diretor Teste 1", Duracao = 120 };
            var filme2 = new Filme { Nome = "Filme Teste 2", Diretor = "Diretor Teste 2", Duracao = 90 };
            _context.Filmes.AddRange(filme1, filme2);
            await _context.SaveChangesAsync();

            var resultado = await _controller.GetFilmes(pagina: 1, itensPagina: 10);
            var resultadoOk = resultado.Result as OkObjectResult;

            Assert.IsNotNull(resultadoOk, "Resultado não deve ser nulo.");
            var dados = resultadoOk.Value as IDictionary<string, object>;
            var filmes = dados?["Filmes"] as IEnumerable<Filme>;
            Assert.AreEqual(2, filmes.Count(), "Número de filmes não corresponde.");
        }
    }
}
