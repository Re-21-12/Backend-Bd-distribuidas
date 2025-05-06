using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_db.Data;
using api_db.Models;

namespace api_db.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ciudadController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ciudadController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ciudad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ciudad>>> Getciudads()
        {
            return await _context.ciudads.ToListAsync();
        }

        // GET: api/ciudad/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ciudad>> Getciudad(string id)
        {
            var ciudad = await _context.ciudads.FindAsync(id);

            if (ciudad == null)
            {
                return NotFound();
            }

            return ciudad;
        }

        // PUT: api/ciudad/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putciudad(string id, ciudad ciudad)
        {
            if (id != ciudad.codigo_ciudad)
            {
                return BadRequest();
            }

            _context.Entry(ciudad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ciudadExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ciudad
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ciudad>> Postciudad(ciudad ciudad)
        {
            _context.ciudads.Add(ciudad);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ciudadExists(ciudad.codigo_ciudad))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getciudad", new { id = ciudad.codigo_ciudad }, ciudad);
        }

        // DELETE: api/ciudad/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteciudad(string id)
        {
            var ciudad = await _context.ciudads.FindAsync(id);
            if (ciudad == null)
            {
                return NotFound();
            }

            _context.ciudads.Remove(ciudad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ciudadExists(string id)
        {
            return _context.ciudads.Any(e => e.codigo_ciudad == id);
        }
    }
}
