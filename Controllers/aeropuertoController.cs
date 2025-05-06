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
    public class aeropuertoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public aeropuertoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/aeropuerto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<aeropuerto>>> Getaeropuertos()
        {
            return await _context.aeropuertos.ToListAsync();
        }

        // GET: api/aeropuerto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<aeropuerto>> Getaeropuerto(int id)
        {
            var aeropuerto = await _context.aeropuertos.FindAsync(id);

            if (aeropuerto == null)
            {
                return NotFound();
            }

            return aeropuerto;
        }

        // PUT: api/aeropuerto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putaeropuerto(int id, aeropuerto aeropuerto)
        {
            if (id != aeropuerto.id_aeropuerto)
            {
                return BadRequest();
            }

            _context.Entry(aeropuerto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!aeropuertoExists(id))
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

        // POST: api/aeropuerto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<aeropuerto>> Postaeropuerto(aeropuerto aeropuerto)
        {
            _context.aeropuertos.Add(aeropuerto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (aeropuertoExists(aeropuerto.id_aeropuerto))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getaeropuerto", new { id = aeropuerto.id_aeropuerto }, aeropuerto);
        }

        // DELETE: api/aeropuerto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteaeropuerto(int id)
        {
            var aeropuerto = await _context.aeropuertos.FindAsync(id);
            if (aeropuerto == null)
            {
                return NotFound();
            }

            _context.aeropuertos.Remove(aeropuerto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool aeropuertoExists(int id)
        {
            return _context.aeropuertos.Any(e => e.id_aeropuerto == id);
        }
    }
}
