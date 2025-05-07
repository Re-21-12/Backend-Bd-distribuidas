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
    public class reservaController : ControllerBase
    {
        private readonly AppDbContextFactory _dbContextFactory;

        public reservaController(AppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        // GET: api/reserva
        [HttpGet]
        public async Task<ActionResult<IEnumerable<reserva>>> Getreservas()
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            return await context.reservas.ToListAsync();
        }

        // GET: api/reserva/5
        [HttpGet("{id}")]
        public async Task<ActionResult<reserva>> Getreserva(uint id)
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            var reserva = await context.reservas.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return reserva;
        }

        // PUT: api/reserva/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putreserva(uint id, reserva reserva)
        {
            if (id != reserva.id_reserva)
            {
                return BadRequest();
            }

            using var context = _dbContextFactory.CreateWriteContext();
            context.Entry(reserva).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.reservas.Any(e => e.id_reserva == id))
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

        // POST: api/reserva
        [HttpPost]
        public async Task<ActionResult<reserva>> Postreserva(reserva reserva)
        {
            using var context = _dbContextFactory.CreateWriteContext();
            context.reservas.Add(reserva);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (context.reservas.Any(e => e.id_reserva == reserva.id_reserva))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getreserva", new { id = reserva.id_reserva }, reserva);
        }

        // DELETE: api/reserva/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletereserva(uint id)
        {
            using var context = _dbContextFactory.CreateWriteContext();
            var reserva = await context.reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            context.reservas.Remove(reserva);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
