using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaApi.Data;
using CinemaApi.Models;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalasController : ControllerBase
    {
        private readonly CinemaContext _context;

        public SalasController(CinemaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sala>>> GetSalas(int pagina = 1, int itensPagina = 3)
        {
            if (pagina <= 0 || itensPagina <= 0)
                return BadRequest("Página e itens da página devem ser maiores que 0");

            var totalSalas = await _context.Salas.CountAsync();

            if (totalSalas == 0)
                return NotFound("Nenhum sala encontrado.");

            var totalPaginas = (int)Math.Ceiling(totalSalas / (double)itensPagina);

            if (pagina > totalPaginas)
                return BadRequest($"O número de página(s) excede o total de página(s). Existem apena(s) {totalPaginas} página(s).");

            var salas = await _context.Salas
                .Skip((pagina - 1) * itensPagina)
                .Take(itensPagina)
                .ToListAsync();

            var dados = new
            {
                Pagina = pagina,
                ItensPagina = itensPagina,
                TotalSalas = totalSalas,
                TotalPaginas = totalPaginas,
                Salas = salas
            };

            return Ok(dados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sala>> GetSala(int id)
        {
            var sala = await _context.Salas.FindAsync(id);

            if (sala == null)
                return NotFound();

            return Ok(sala);
        }

        [HttpPost]
        public async Task<ActionResult<Sala>> PostSala(Sala sala)
        {
            _context.Salas.Add(sala);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSala), new { id = sala.Id }, sala);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSala(int id, Sala sala)
        {
            if (id != sala.Id)
            {
                return BadRequest();
            }

            _context.Entry(sala).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (!SalaExiste(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSala(int id)
        {
            var sala = await _context.Salas.FindAsync(id);

            if (sala == null)
                return NotFound();

            _context.Salas.Remove(sala);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalaExiste(int id)
        {
            return _context.Salas.Any(e => e.Id == id);
        }
    }
}