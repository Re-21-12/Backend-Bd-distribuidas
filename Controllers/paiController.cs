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
    public class paiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public paiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/pai
        [HttpGet]
        public async Task<ActionResult<IEnumerable<pai>>> Getpais()
        {
            return await _context.pais.ToListAsync();
        }

        // GET: api/pai/5
        [HttpGet("{id}")]
        public async Task<ActionResult<pai>> Getpai(string id)
        {
            var pai = await _context.pais.FindAsync(id);

            if (pai == null)
            {
                return NotFound();
            }

            return pai;
        }

        // PUT: api/pai/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putpai(string id, pai pai)
        {
            if (id != pai.codigo_pais)
            {
                return BadRequest();
            }

            _context.Entry(pai).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!paiExists(id))
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

        // POST: api/pai
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<pai>> Postpai(pai pai)
        {
            _context.pais.Add(pai);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (paiExists(pai.codigo_pais))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getpai", new { id = pai.codigo_pais }, pai);
        }

        // DELETE: api/pai/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletepai(string id)
        {
            var pai = await _context.pais.FindAsync(id);
            if (pai == null)
            {
                return NotFound();
            }

            _context.pais.Remove(pai);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool paiExists(string id)
        {
            return _context.pais.Any(e => e.codigo_pais == id);
        }
    }
}
