using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PvItemsAPI.Models;

namespace PvItemsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PvItemsController : ControllerBase
    {
        private readonly PvContext _context;

        public PvItemsController(PvContext context)
        {
            _context = context;
        }

        // GET: api/PvItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PvItem>>> GetPvItems()
        {
            return await _context.PvItems.ToListAsync();
        }

        // GET: api/PvItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PvItem>> GetPvItem(long id)
        {
            var pvItem = await _context.PvItems.FindAsync(id);

            if (pvItem == null)
            {
                return NotFound();
            }

            return pvItem;
        }

        // PUT: api/PvItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPvItem(long id, PvItem pvItem)
        {
            if (id != pvItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(pvItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PvItemExists(id))
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

        // POST: api/PvItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PvItem>> PostPvItem(PvItem pvItem)
        {
            _context.PvItems.Add(pvItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetPvItem", new { id = pvItem.Id }, pvItem);
            return CreatedAtAction(nameof(GetPvItem), new { id = pvItem.Id }, pvItem);
        }

        // DELETE: api/PvItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PvItem>> DeletePvItem(long id)
        {
            var pvItem = await _context.PvItems.FindAsync(id);
            if (pvItem == null)
            {
                return NotFound();
            }

            _context.PvItems.Remove(pvItem);
            await _context.SaveChangesAsync();

            return pvItem;
        }

        private bool PvItemExists(long id)
        {
            return _context.PvItems.Any(e => e.Id == id);
        }
    }
}
