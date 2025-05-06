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
    public class telefonoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public telefonoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/telefono
        [HttpGet]
        public async Task<ActionResult<IEnumerable<telefono>>> Gettelefonos()
        {
            return await _context.telefonos.ToListAsync();
        }

        // GET: api/telefono/5
        [HttpGet("{id}")]
        public async Task<ActionResult<telefono>> Gettelefono(string id)
        {
            var telefono = await _context.telefonos.FindAsync(id);

            if (telefono == null)
            {
                return NotFound();
            }

            return telefono;
        }

        // PUT: api/telefono/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Puttelefono(string id, telefono telefono)
        {
            if (id != telefono.numero_telefono)
            {
                return BadRequest();
            }

            _context.Entry(telefono).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!telefonoExists(id))
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

        // POST: api/telefono
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<telefono>> Posttelefono(telefono telefono)
        {
            _context.telefonos.Add(telefono);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (telefonoExists(telefono.numero_telefono))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Gettelefono", new { id = telefono.numero_telefono }, telefono);
        }

        // DELETE: api/telefono/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletetelefono(string id)
        {
            var telefono = await _context.telefonos.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }

            _context.telefonos.Remove(telefono);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool telefonoExists(string id)
        {
            return _context.telefonos.Any(e => e.numero_telefono == id);
        }
    }
}
