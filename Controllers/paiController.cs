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
    public class paiController : ControllerBase
    {
        private readonly AppDbContextFactory _dbContextFactory;

        public paiController(AppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        // GET: api/pai
        [HttpGet]
        public async Task<ActionResult<IEnumerable<pai>>> Getpais()
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            return await context.pais.ToListAsync();
        }

        // GET: api/pai/5
        [HttpGet("{id}")]
        public async Task<ActionResult<pai>> Getpai(string id)
        {
            using var context = _dbContextFactory.CreateReadOnlyContext();
            var pai = await context.pais.FindAsync(id);

            if (pai == null)
            {
                return NotFound();
            }

            return pai;
        }

        // PUT: api/pai/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putpai(string id, pai pai)
        {
            if (id != pai.codigo_pais)
            {
                return BadRequest();
            }

            using var context = _dbContextFactory.CreateWriteContext();
            context.Entry(pai).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.pais.Any(e => e.codigo_pais == id))
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
        [HttpPost]
        public async Task<ActionResult<pai>> Postpai(pai pai)
        {
            using var context = _dbContextFactory.CreateWriteContext();
            context.pais.Add(pai);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (context.pais.Any(e => e.codigo_pais == pai.codigo_pais))
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
            using var context = _dbContextFactory.CreateWriteContext();
            var pai = await context.pais.FindAsync(id);
            if (pai == null)
            {
                return NotFound();
            }

            context.pais.Remove(pai);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
