using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaApi.Controllers;
using CinemaApi.Data;
using CinemaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CinemaApi.Tests
{
    [TestClass]
    public class FilmesControllerTests
    {
        private FilmesController _controller;
        private Mock<CinemaContext> _mockContext;
        private List<Filme> _filmes;

        [TestInitialize]
        public void Initialize()
        {
            // Setup in-memory database or mock context
            _mockContext = new Mock<CinemaContext>();
            _filmes = new List<Filme>
            {
                new Filme { Id = 1, Nome = "Filme 1", Diretor = "Diretor 1", Duracao = 120 },
                new Filme { Id = 2, Nome = "Filme 2", Diretor = "Diretor 2", Duracao = 90 }
            };

            var dbSet = MockDbSet(_filmes);
            _mockContext.Setup(m => m.Filmes).Returns(dbSet.Object);

            _controller = new FilmesController(_mockContext.Object);
        }

        [TestMethod]
        public async Task GetFilmes_ReturnsOkResult_WithListOfFilmes()
        {
            // Act
            var result = await _controller.GetFilmes();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var filmes = okResult.Value as IEnumerable<Filme>;
            Assert.AreEqual(2, filmes.Count());
        }

        [TestMethod]
        public async Task GetFilme_ReturnsNotFound_WhenFilmeDoesNotExist()
        {
            // Act
            var result = await _controller.GetFilme(999);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PostFilme_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var filme = new Filme { Id = 3, Nome = "Filme 3", Diretor = "Diretor 3", Duracao = 100 };

            // Act
            var result = await _controller.PostFilme(filme);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("GetFilme", createdResult.ActionName);
        }

        [TestMethod]
        public async Task PutFilme_ReturnsNoContent_WhenUpdatedSuccessfully()
        {
            // Arrange
            var filme = new Filme { Id = 1, Nome = "Updated Filme", Diretor = "Updated Diretor", Duracao = 110 };

            // Act
            var result = await _controller.PutFilme(1, filme);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteFilme_ReturnsNotFound_WhenFilmeDoesNotExist()
        {
            // Act
            var result = await _controller.DeleteFilme(999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private Mock<DbSet<T>> MockDbSet<T>(List<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());
            return dbSet;
        }
    }
}
