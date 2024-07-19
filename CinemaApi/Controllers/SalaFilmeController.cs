using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaApi.Data;
using CinemaApi.Models;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaFilmeController : ControllerBase
    {
        private readonly CinemaContext _context;

        public SalaFilmeController(CinemaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaFilme>>> GetSalaFilmes(int pagina = 1, int itensPagina = 3)
        {
            if (pagina <= 0 || itensPagina <= 0)
                return BadRequest("Página e itens da página devem ser maiores que 0");

            var totalSalaFilme = await _context.SalaFilmes.CountAsync();

            if (totalSalaFilme == 0)
                return NotFound("Nenhuma sala com filmes encontrada.");

            var totalPaginas = (int)Math.Ceiling(totalSalaFilme / (double)itensPagina);

            if (pagina > totalPaginas)
                return BadRequest($"O número de página(s) excede o total de página(s). Existem apena(s) {totalPaginas} página(s).");

            var salaFilme = await _context.SalaFilmes
                .Skip((pagina - 1) * itensPagina)
                .Take(itensPagina)
                .ToListAsync();

            var dados = new
            {
                Pagina = pagina,
                ItensPagina = itensPagina,
                TotalSalaFilme = totalSalaFilme,
                TotalPaginas = totalPaginas,
                SalaFilme = salaFilme
            };

            return Ok(dados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalaFilme>> GetSalaFilme(int id)
        {
            var salaFilme = await _context.SalaFilmes
                .Include(sf => sf.Sala)
                .Include(sf => sf.Filme)
                .FirstOrDefaultAsync(sf => sf.SalaId == id);

            if (salaFilme == null)
                return NotFound();

            return salaFilme;
        }

        [HttpPost]
        public async Task<ActionResult<SalaFilme>> PostSalaFilme(SalaFilme salaFilme)
        {
            _context.SalaFilmes.Add(salaFilme);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSalaFilme), new { id = salaFilme.SalaId }, salaFilme);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalaFilme(int id, SalaFilme salaFilme)
        {
            if (id != salaFilme.FilmeId)
            {
                return BadRequest();
            }

            _context.Entry(salaFilme).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (!SalaFilmeExiste(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalaFilme(int id)
        {
            var salaFilme = await _context.SalaFilmes.FindAsync(id);

            if (salaFilme == null)
                return NotFound();

            _context.SalaFilmes.Remove(salaFilme);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalaFilmeExiste(int id)
        {
            return _context.SalaFilmes.Any(e => e.SalaId == id);
        }
    }
}