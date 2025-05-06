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
    public class correo_electronicoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public correo_electronicoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/correo_electronico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<correo_electronico>>> Getcorreo_electronicos()
        {
            return await _context.correo_electronicos.ToListAsync();
        }

        // GET: api/correo_electronico/5
        [HttpGet("{id}")]
        public async Task<ActionResult<correo_electronico>> Getcorreo_electronico(string id)
        {
            var correo_electronico = await _context.correo_electronicos.FindAsync(id);

            if (correo_electronico == null)
            {
                return NotFound();
            }

            return correo_electronico;
        }

        // PUT: api/correo_electronico/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcorreo_electronico(string id, correo_electronico correo_electronico)
        {
            if (id != correo_electronico.correo)
            {
                return BadRequest();
            }

            _context.Entry(correo_electronico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!correo_electronicoExists(id))
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

        // POST: api/correo_electronico
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<correo_electronico>> Postcorreo_electronico(correo_electronico correo_electronico)
        {
            _context.correo_electronicos.Add(correo_electronico);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (correo_electronicoExists(correo_electronico.correo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getcorreo_electronico", new { id = correo_electronico.correo }, correo_electronico);
        }

        // DELETE: api/correo_electronico/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletecorreo_electronico(string id)
        {
            var correo_electronico = await _context.correo_electronicos.FindAsync(id);
            if (correo_electronico == null)
            {
                return NotFound();
            }

            _context.correo_electronicos.Remove(correo_electronico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool correo_electronicoExists(string id)
        {
            return _context.correo_electronicos.Any(e => e.correo == id);
        }
    }
}
