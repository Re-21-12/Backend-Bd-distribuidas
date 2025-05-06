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
    public class pasajeroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public pasajeroController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/pasajero
        [HttpGet]
        public async Task<ActionResult<IEnumerable<pasajero>>> Getpasajeros()
        {
            return await _context.pasajeros.ToListAsync();
        }

        // GET: api/pasajero/5
        [HttpGet("{id}")]
        public async Task<ActionResult<pasajero>> Getpasajero(uint id)
        {
            var pasajero = await _context.pasajeros.FindAsync(id);

            if (pasajero == null)
            {
                return NotFound();
            }

            return pasajero;
        }

        // PUT: api/pasajero/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putpasajero(uint id, pasajero pasajero)
        {
            if (id != pasajero.id_pasajero)
            {
                return BadRequest();
            }

            _context.Entry(pasajero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!pasajeroExists(id))
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

        // POST: api/pasajero
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<pasajero>> Postpasajero(pasajero pasajero)
        {
            _context.pasajeros.Add(pasajero);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (pasajeroExists(pasajero.id_pasajero))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getpasajero", new { id = pasajero.id_pasajero }, pasajero);
        }

        // DELETE: api/pasajero/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletepasajero(uint id)
        {
            var pasajero = await _context.pasajeros.FindAsync(id);
            if (pasajero == null)
            {
                return NotFound();
            }

            _context.pasajeros.Remove(pasajero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool pasajeroExists(uint id)
        {
            return _context.pasajeros.Any(e => e.id_pasajero == id);
        }
    }
}
