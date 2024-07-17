using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<IEnumerable<Sala>>> GetSalas()
    {
        return await _context.Salas.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Sala>> GetSala(int id)
    {
        var sala = await _context.Salas.FindAsync(id);

        if (sala == null)
            return NotFound();

        return sala;
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