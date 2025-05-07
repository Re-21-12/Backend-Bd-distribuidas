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
    public class plazaController : ControllerBase
    {
        private readonly AppDbContextFactory _dbContextFactory;

        public plazaController(AppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        // GET: api/plaza
        [HttpGet]
        public async Task<ActionResult<IEnumerable<plaza>>> Getplazas()
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            return await context.plazas.ToListAsync();
        }

        // GET: api/plaza/5
        [HttpGet("{id}")]
        public async Task<ActionResult<plaza>> Getplaza(string id)
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            var plaza = await context.plazas.FindAsync(id);

            if (plaza == null)
            {
                return NotFound();
            }

            return plaza;
        }

        // PUT: api/plaza/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putplaza(string id, plaza plaza)
        {
            if (id != plaza.letra_fila)
            {
                return BadRequest();
            }

            using var context = _dbContextFactory.CreateWriteContext();
            context.Entry(plaza).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.plazas.Any(e => e.letra_fila == id))
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
        [HttpPost]
        public async Task<ActionResult<plaza>> Postplaza(plaza plaza)
        {
            using var context = _dbContextFactory.CreateWriteContext();
            context.plazas.Add(plaza);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (context.plazas.Any(e => e.letra_fila == plaza.letra_fila))
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
            using var context = _dbContextFactory.CreateWriteContext();
            var plaza = await context.plazas.FindAsync(id);
            if (plaza == null)
            {
                return NotFound();
            }

            context.plazas.Remove(plaza);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
