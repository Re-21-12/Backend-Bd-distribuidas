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
    public class correo_electronicoController : ControllerBase
    {
        private readonly AppDbContextFactory _dbContextFactory;

        public correo_electronicoController(AppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        // GET: api/correo_electronico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<correo_electronico>>> Getcorreo_electronicos()
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            return await context.correo_electronicos.ToListAsync();
        }

        // GET: api/correo_electronico/5
        [HttpGet("{id}")]
        public async Task<ActionResult<correo_electronico>> Getcorreo_electronico(string id)
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            var correo_electronico = await context.correo_electronicos.FindAsync(id);

            if (correo_electronico == null)
            {
                return NotFound();
            }

            return correo_electronico;
        }

        // PUT: api/correo_electronico/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcorreo_electronico(string id, correo_electronico correo_electronico)
        {
            if (id != correo_electronico.correo)
            {
                return BadRequest();
            }

            using var context = _dbContextFactory.CreateWriteContext();
            context.Entry(correo_electronico).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.correo_electronicos.Any(e => e.correo == id))
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
        [HttpPost]
        public async Task<ActionResult<correo_electronico>> Postcorreo_electronico(correo_electronico correo_electronico)
        {
            using var context = _dbContextFactory.CreateWriteContext();
            context.correo_electronicos.Add(correo_electronico);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (context.correo_electronicos.Any(e => e.correo == correo_electronico.correo))
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
            using var context = _dbContextFactory.CreateWriteContext();
            var correo_electronico = await context.correo_electronicos.FindAsync(id);
            if (correo_electronico == null)
            {
                return NotFound();
            }

            context.correo_electronicos.Remove(correo_electronico);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
