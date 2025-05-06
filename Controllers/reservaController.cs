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
    public class reservaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public reservaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/reserva
        [HttpGet]
        public async Task<ActionResult<IEnumerable<reserva>>> Getreservas()
        {
            return await _context.reservas.ToListAsync();
        }

        // GET: api/reserva/5
        [HttpGet("{id}")]
        public async Task<ActionResult<reserva>> Getreserva(uint id)
        {
            var reserva = await _context.reservas.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return reserva;
        }

        // PUT: api/reserva/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putreserva(uint id, reserva reserva)
        {
            if (id != reserva.id_reserva)
            {
                return BadRequest();
            }

            _context.Entry(reserva).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!reservaExists(id))
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<reserva>> Postreserva(reserva reserva)
        {
            _context.reservas.Add(reserva);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (reservaExists(reserva.id_reserva))
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
            var reserva = await _context.reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            _context.reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool reservaExists(uint id)
        {
            return _context.reservas.Any(e => e.id_reserva == id);
        }
    }
}
