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
    public class vueloController : ControllerBase
    {
        private readonly AppDbContext _context;

        public vueloController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/vuelo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<vuelo>>> Getvuelos()
        {
            return await _context.vuelos.ToListAsync();
        }

        // GET: api/vuelo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<vuelo>> Getvuelo(string id)
        {
            var vuelo = await _context.vuelos.FindAsync(id);

            if (vuelo == null)
            {
                return NotFound();
            }

            return vuelo;
        }

        // PUT: api/vuelo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putvuelo(string id, vuelo vuelo)
        {
            if (id != vuelo.numero_vuelo)
            {
                return BadRequest();
            }

            _context.Entry(vuelo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vueloExists(id))
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

        // POST: api/vuelo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<vuelo>> Postvuelo(vuelo vuelo)
        {
            _context.vuelos.Add(vuelo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (vueloExists(vuelo.numero_vuelo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getvuelo", new { id = vuelo.numero_vuelo }, vuelo);
        }

        // DELETE: api/vuelo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletevuelo(string id)
        {
            var vuelo = await _context.vuelos.FindAsync(id);
            if (vuelo == null)
            {
                return NotFound();
            }

            _context.vuelos.Remove(vuelo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool vueloExists(string id)
        {
            return _context.vuelos.Any(e => e.numero_vuelo == id);
        }
    }
}
