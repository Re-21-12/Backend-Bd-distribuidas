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
    public class aerolineaController : ControllerBase
    {
        private readonly AppDbContextFactory _dbContextFactory;

        public aerolineaController(AppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        // GET: api/aerolinea
        [HttpGet]
        public async Task<ActionResult<IEnumerable<aerolinea>>> Getaerolineas()
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            return await context.aerolineas.ToListAsync();
        }

        // GET: api/aerolinea/5
        [HttpGet("{id}")]
        public async Task<ActionResult<aerolinea>> Getaerolinea(string id)
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            var aerolinea = await context.aerolineas.FindAsync(id);

            if (aerolinea == null)
            {
                return NotFound();
            }

            return aerolinea;
        }

        // PUT: api/aerolinea/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putaerolinea(string id, aerolinea aerolinea)
        {
            if (id != aerolinea.id_aerolinea)
            {
                return BadRequest();
            }

            using var context = _dbContextFactory.CreateWriteContext();
            context.Entry(aerolinea).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.aerolineas.Any(e => e.id_aerolinea == id))
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
        [HttpPost]
        public async Task<ActionResult<aerolinea>> Postaerolinea(aerolinea aerolinea)
        {
            using var context = _dbContextFactory.CreateWriteContext();
            context.aerolineas.Add(aerolinea);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (context.aerolineas.Any(e => e.id_aerolinea == aerolinea.id_aerolinea))
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
            using var context = _dbContextFactory.CreateWriteContext();
            var aerolinea = await context.aerolineas.FindAsync(id);
            if (aerolinea == null)
            {
                return NotFound();
            }

            context.aerolineas.Remove(aerolinea);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
