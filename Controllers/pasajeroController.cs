using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly AppDbContextFactory _dbContextFactory;

        public pasajeroController(AppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        // GET: api/pasajero
        [HttpGet]
        public async Task<ActionResult<IEnumerable<pasajero>>> Getpasajeros()
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            return await context.pasajeros.ToListAsync();
        }

        // GET: api/pasajero/5
        [HttpGet("{id}")]
        public async Task<ActionResult<pasajero>> Getpasajero(uint id)
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            var pasajero = await context.pasajeros.FindAsync(id);

            if (pasajero == null)
            {
                return NotFound();
            }

            return pasajero;
        }

        // PUT: api/pasajero/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putpasajero(uint id, pasajero pasajero)
        {
            if (id != pasajero.id_pasajero)
            {
                return BadRequest();
            }

            using var context = _dbContextFactory.CreateWriteContext();
            context.Entry(pasajero).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.pasajeros.Any(e => e.id_pasajero == id))
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
        [HttpPost]
        public async Task<ActionResult<pasajero>> Postpasajero(pasajero pasajero)
        {
            using var context = _dbContextFactory.CreateWriteContext();
            context.pasajeros.Add(pasajero);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (context.pasajeros.Any(e => e.id_pasajero == pasajero.id_pasajero))
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
            using var context = _dbContextFactory.CreateWriteContext();
            var pasajero = await context.pasajeros.FindAsync(id);
            if (pasajero == null)
            {
                return NotFound();
            }

            context.pasajeros.Remove(pasajero);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
