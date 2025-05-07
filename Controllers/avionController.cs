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
    public class avionController : ControllerBase
    {
        private readonly AppDbContextFactory _dbContextFactory;

        public avionController(AppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        // GET: api/avion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<avion>>> Getavions()
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            return await context.avions.ToListAsync();
        }

        // GET: api/avion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<avion>> Getavion(int id)
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            var avion = await context.avions.FindAsync(id);

            if (avion == null)
            {
                return NotFound();
            }

            return avion;
        }

        // PUT: api/avion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putavion(int id, avion avion)
        {
            if (id != avion.id_avion)
            {
                return BadRequest();
            }

            using var context = _dbContextFactory.CreateWriteContext();
            context.Entry(avion).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.avions.Any(e => e.id_avion == id))
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
        [HttpPost]
        public async Task<ActionResult<avion>> Postavion(avion avion)
        {
            using var context = _dbContextFactory.CreateWriteContext();
            context.avions.Add(avion);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (context.avions.Any(e => e.id_avion == avion.id_avion))
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
            using var context = _dbContextFactory.CreateWriteContext();
            var avion = await context.avions.FindAsync(id);
            if (avion == null)
            {
                return NotFound();
            }

            context.avions.Remove(avion);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
