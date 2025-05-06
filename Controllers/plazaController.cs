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
    public class plazaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public plazaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/plaza
        [HttpGet]
        public async Task<ActionResult<IEnumerable<plaza>>> Getplazas()
        {
            return await _context.plazas.ToListAsync();
        }

        // GET: api/plaza/5
        [HttpGet("{id}")]
        public async Task<ActionResult<plaza>> Getplaza(string id)
        {
            var plaza = await _context.plazas.FindAsync(id);

            if (plaza == null)
            {
                return NotFound();
            }

            return plaza;
        }

        // PUT: api/plaza/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putplaza(string id, plaza plaza)
        {
            if (id != plaza.letra_fila)
            {
                return BadRequest();
            }

            _context.Entry(plaza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!plazaExists(id))
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

        // POST: api/plaza
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<plaza>> Postplaza(plaza plaza)
        {
            _context.plazas.Add(plaza);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (plazaExists(plaza.letra_fila))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getplaza", new { id = plaza.letra_fila }, plaza);
        }

        // DELETE: api/plaza/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteplaza(string id)
        {
            var plaza = await _context.plazas.FindAsync(id);
            if (plaza == null)
            {
                return NotFound();
            }

            _context.plazas.Remove(plaza);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool plazaExists(string id)
        {
            return _context.plazas.Any(e => e.letra_fila == id);
        }
    }
}
