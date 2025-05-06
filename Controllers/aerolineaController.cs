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
    public class aerolineaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public aerolineaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/aerolinea
        [HttpGet]
        public async Task<ActionResult<IEnumerable<aerolinea>>> Getaerolineas()
        {
            return await _context.aerolineas.ToListAsync();
        }

        // GET: api/aerolinea/5
        [HttpGet("{id}")]
        public async Task<ActionResult<aerolinea>> Getaerolinea(string id)
        {
            var aerolinea = await _context.aerolineas.FindAsync(id);

            if (aerolinea == null)
            {
                return NotFound();
            }

            return aerolinea;
        }

        // PUT: api/aerolinea/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putaerolinea(string id, aerolinea aerolinea)
        {
            if (id != aerolinea.id_aerolinea)
            {
                return BadRequest();
            }

            _context.Entry(aerolinea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!aerolineaExists(id))
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

        // POST: api/aerolinea
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<aerolinea>> Postaerolinea(aerolinea aerolinea)
        {
            _context.aerolineas.Add(aerolinea);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (aerolineaExists(aerolinea.id_aerolinea))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getaerolinea", new { id = aerolinea.id_aerolinea }, aerolinea);
        }

        // DELETE: api/aerolinea/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteaerolinea(string id)
        {
            var aerolinea = await _context.aerolineas.FindAsync(id);
            if (aerolinea == null)
            {
                return NotFound();
            }

            _context.aerolineas.Remove(aerolinea);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool aerolineaExists(string id)
        {
            return _context.aerolineas.Any(e => e.id_aerolinea == id);
        }
    }
}
