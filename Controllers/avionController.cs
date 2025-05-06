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
    public class avionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public avionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/avion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<avion>>> Getavions()
        {
            return await _context.avions.ToListAsync();
        }

        // GET: api/avion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<avion>> Getavion(int id)
        {
            var avion = await _context.avions.FindAsync(id);

            if (avion == null)
            {
                return NotFound();
            }

            return avion;
        }

        // PUT: api/avion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putavion(int id, avion avion)
        {
            if (id != avion.id_avion)
            {
                return BadRequest();
            }

            _context.Entry(avion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!avionExists(id))
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

        // POST: api/avion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<avion>> Postavion(avion avion)
        {
            _context.avions.Add(avion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (avionExists(avion.id_avion))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getavion", new { id = avion.id_avion }, avion);
        }

        // DELETE: api/avion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteavion(int id)
        {
            var avion = await _context.avions.FindAsync(id);
            if (avion == null)
            {
                return NotFound();
            }

            _context.avions.Remove(avion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool avionExists(int id)
        {
            return _context.avions.Any(e => e.id_avion == id);
        }
    }
}
