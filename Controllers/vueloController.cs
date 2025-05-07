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
    public class vueloController : ControllerBase
    {
        private readonly AppDbContextFactory _dbContextFactory;

        public vueloController(AppDbContextFactory context)
        {
            _dbContextFactory = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<vuelo>>> Getvuelos()
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            return await context.vuelos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<vuelo>> Getvuelo(string id)
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            var vuelo = await context.vuelos.FindAsync(id);

            if (vuelo == null)
            {
                return NotFound();
            }

            return vuelo;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Putvuelo(string id, vuelo vuelo)
        {
            if (id != vuelo.numero_vuelo)
            {
                return BadRequest();
            }

            using var context = _dbContextFactory.CreateWriteContext();
            context.Entry(vuelo).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.vuelos.Any(e => e.numero_vuelo == id))
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
        [HttpPost]
        public async Task<ActionResult<vuelo>> Postvuelo(vuelo vuelo)
        {
            using var context = _dbContextFactory.CreateWriteContext();
            context.vuelos.Add(vuelo);
            await context.SaveChangesAsync();

            // Retorna 201 Created con la ruta del nuevo recurso creado
            return CreatedAtAction(nameof(Getvuelo), new { id = vuelo.numero_vuelo }, vuelo);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletevuelo(string id)
        {
            using var context = _dbContextFactory.CreateWriteContext();
            var vuelo = await context.vuelos.FindAsync(id);
            if (vuelo == null)
            {
                return NotFound();
            }

            context.vuelos.Remove(vuelo);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
