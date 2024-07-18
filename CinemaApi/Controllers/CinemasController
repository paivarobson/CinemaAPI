using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CinemasController : ControllerBase
{
    private readonly CinemaContext _context;

    public CinemasController(CinemaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cinema>>> GetCinemas(int pagina = 1, int itensPagina = 3)
    {
        if (pagina <= 0 || itensPagina <= 0)
            return BadRequest("Página e itens da página devem ser maiores que 0");

        var totalCinemas = await _context.Cinemas.CountAsync();

        if (totalCinemas == 0)
            return NotFound("Nenhum Cinema encontrado.");

        var totalPaginas = (int)Math.Ceiling(totalCinemas / (double)itensPagina);

        if (pagina > totalPaginas)
            return BadRequest($"O número de página(s) excede o total de página(s). Existem apena(s) {totalPaginas} página(s).");

        var Cinemas = await _context.Cinemas
            .Skip((pagina - 1) * itensPagina)
            .Take(itensPagina)
            .ToListAsync();

        var result = new
        {
            Pagina = pagina,
            ItensPagina = itensPagina,
            TotalCinemas = totalCinemas,
            TotalPaginas = totalPaginas,
            Cinemas = Cinemas
        };

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cinema>> GetCinema(int id)
    {
        var Cinema = await _context.Cinemas.FindAsync(id);

        if (Cinema == null)
            return NotFound();

        return Cinema;
    }

    [HttpPost]
    public async Task<ActionResult<Cinema>> PostCinema(Cinema Cinema)
    {
        _context.Cinemas.Add(Cinema);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCinema), new { id = Cinema.Id }, Cinema);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCinema(int id, Cinema Cinema)
    {
        if (id != Cinema.Id)
        {
            return BadRequest();
        }

        _context.Entry(Cinema).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch
        {
            if (!CinemaExiste(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCinema(int id)
    {
        var Cinema = await _context.Cinemas.FindAsync(id);

        if (Cinema == null)
            return NotFound();

        _context.Cinemas.Remove(Cinema);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CinemaExiste(int id)
    {
        return _context.Cinemas.Any(e => e.Id == id);
    }
}