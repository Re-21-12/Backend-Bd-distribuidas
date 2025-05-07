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
    public class ciudadController : ControllerBase
    {
        private readonly AppDbContextFactory _dbContextFactory;

        public ciudadController(AppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        // GET: api/ciudad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ciudad>>> Getciudads()
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            return await context.ciudads.ToListAsync();
        }

        // GET: api/ciudad/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ciudad>> Getciudad(string id)
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            var ciudad = await context.ciudads.FindAsync(id);

            if (ciudad == null)
            {
                return NotFound();
            }

            return ciudad;
        }

        // PUT: api/ciudad/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putciudad(string id, ciudad ciudad)
        {
            if (id != ciudad.codigo_ciudad)
            {
                return BadRequest();
            }

            using var context = _dbContextFactory.CreateWriteContext();
            context.Entry(ciudad).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.ciudads.Any(e => e.codigo_ciudad == id))
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

        // POST: api/ciudad
        [HttpPost]
        public async Task<ActionResult<ciudad>> Postciudad(ciudad ciudad)
        {
            using var context = _dbContextFactory.CreateWriteContext();
            context.ciudads.Add(ciudad);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (context.ciudads.Any(e => e.codigo_ciudad == ciudad.codigo_ciudad))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getciudad", new { id = ciudad.codigo_ciudad }, ciudad);
        }

        // DELETE: api/ciudad/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteciudad(string id)
        {
            using var context = _dbContextFactory.CreateWriteContext();
            var ciudad = await context.ciudads.FindAsync(id);
            if (ciudad == null)
            {
                return NotFound();
            }

            context.ciudads.Remove(ciudad);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
