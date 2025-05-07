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
    public class aeropuertoController : ControllerBase
    {
        private readonly AppDbContextFactory _dbContextFactory;

        public aeropuertoController(AppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        // GET: api/aeropuerto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<aeropuerto>>> Getaeropuertos()
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            return await context.aeropuertos.ToListAsync();
        }

        // GET: api/aeropuerto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<aeropuerto>> Getaeropuerto(int id)
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            var aeropuerto = await context.aeropuertos.FindAsync(id);

            if (aeropuerto == null)
            {
                return NotFound();
            }

            return aeropuerto;
        }

        // PUT: api/aeropuerto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putaeropuerto(int id, aeropuerto aeropuerto)
        {
            if (id != aeropuerto.id_aeropuerto)
            {
                return BadRequest();
            }

            using var context = _dbContextFactory.CreateWriteContext();
            context.Entry(aeropuerto).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.aeropuertos.Any(e => e.id_aeropuerto == id))
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

        // POST: api/aeropuerto
        [HttpPost]
        public async Task<ActionResult<aeropuerto>> Postaeropuerto(aeropuerto aeropuerto)
        {
            using var context = _dbContextFactory.CreateWriteContext();
            context.aeropuertos.Add(aeropuerto);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (context.aeropuertos.Any(e => e.id_aeropuerto == aeropuerto.id_aeropuerto))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getaeropuerto", new { id = aeropuerto.id_aeropuerto }, aeropuerto);
        }

        // DELETE: api/aeropuerto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteaeropuerto(int id)
        {
            using var context = _dbContextFactory.CreateWriteContext();
            var aeropuerto = await context.aeropuertos.FindAsync(id);
            if (aeropuerto == null)
            {
                return NotFound();
            }

            context.aeropuertos.Remove(aeropuerto);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
