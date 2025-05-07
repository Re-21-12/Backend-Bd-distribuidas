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
    public class telefonoController : ControllerBase
    {
        private readonly AppDbContextFactory _dbContextFactory;

        public telefonoController(AppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        // GET: api/telefono
        [HttpGet]
        public async Task<ActionResult<IEnumerable<telefono>>> Gettelefonos()
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            return await context.telefonos.ToListAsync();
        }

        // GET: api/telefono/5
        [HttpGet("{id}")]
        public async Task<ActionResult<telefono>> Gettelefono(string id)
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            var telefono = await context.telefonos.FindAsync(id);

            if (telefono == null)
            {
                return NotFound();
            }

            return telefono;
        }

        // PUT: api/telefono/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Puttelefono(string id, telefono telefono)
        {
            if (id != telefono.numero_telefono)
            {
                return BadRequest();
            }

            using var context = _dbContextFactory.CreateWriteContext();
            context.Entry(telefono).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.telefonos.Any(e => e.numero_telefono == id))
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
        [HttpPost]
        public async Task<ActionResult<telefono>> Posttelefono(telefono telefono)
        {
            using var context = _dbContextFactory.CreateWriteContext();
            context.telefonos.Add(telefono);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (context.telefonos.Any(e => e.numero_telefono == telefono.numero_telefono))
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
            using var context = _dbContextFactory.CreateWriteContext();
            var telefono = await context.telefonos.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }

            context.telefonos.Remove(telefono);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
