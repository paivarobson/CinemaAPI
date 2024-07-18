using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class FilmesController : ControllerBase
{
    private readonly CinemaContext _context;

    public FilmesController(CinemaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Filme>>> GetFilmes(int pagina = 1, int itensPagina = 3)
    {
        if (pagina <= 0 || itensPagina <= 0)
            return BadRequest("Página e itens da página devem ser maiores que 0");

        var totalFilmes = await _context.Filmes.CountAsync();

        if (totalFilmes == 0)
            return NotFound("Nenhum filme encontrado.");

        var totalPaginas = (int)Math.Ceiling(totalFilmes / (double)itensPagina);

        if (pagina > totalPaginas)
            return BadRequest($"O número de página(s) excede o total de página(s). Existem apena(s) {totalPaginas} página(s).");

        var filmes = await _context.Filmes
            .Skip((pagina - 1) * itensPagina)
            .Take(itensPagina)
            .ToListAsync();

        var result = new
        {
            Pagina = pagina,
            ItensPagina = itensPagina,
            TotalFilmes = totalFilmes,
            TotalPaginas = totalPaginas,
            Filmes = filmes
        };

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Filme>> GetFilme(int id)
    {
        var filme = await _context.Filmes.FindAsync(id);

        if (filme == null)
            return NotFound();

        return filme;
    }

    [HttpPost]
    public async Task<ActionResult<Filme>> PostFilme(Filme filme)
    {
        _context.Filmes.Add(filme);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFilme), new { id = filme.Id }, filme);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutFilme(int id, Filme filme)
    {
        if (id != filme.Id)
            return BadRequest();

        _context.Entry(filme).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FilmeExiste(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFilme(int id)
    {
        var filme = await _context.Filmes.FindAsync(id);

        if (filme == null)
            return NotFound();

        _context.Filmes.Remove(filme);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool FilmeExiste(int id)
    {
        return _context.Filmes.Any(e => e.Id == id);
    }
}