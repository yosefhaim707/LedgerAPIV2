using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LedgerAPIV2.Data;
using LedgerAPIV2.Models;
using NuGet.Versioning;

namespace LedgerAPIV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedgersController : ControllerBase
    {
        private readonly LedgerAPIV2Context _context;

        public LedgersController(LedgerAPIV2Context context)
        {
            _context = context;
        }

        // GET: api/Ledgers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ledger>>> GetLedger()
        {
            return await _context.Ledger.ToListAsync();
        }

        // GET: api/Ledgers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ledger>> GetLedger(int id)
        {
            var ledger = await _context.Ledger.FindAsync(id);

            if (ledger == null)
            {
                return NotFound();
            }

            return ledger;
        }

        // PUT: api/Ledgers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLedger(int id, Ledger ledger)
        {
            if (id != ledger.Id)
            {
                return BadRequest();
            }

            _context.Entry(ledger).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LedgerExists(id))
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

        // POST: api/Ledgers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ledger>> PostLedger(Ledger ledger)
        {
            _context.Ledger.Add(ledger);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLedger", new { id = ledger.Id }, ledger);
        }

        // DELETE: api/Ledgers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLedger(int id)
        {
            var ledger = await _context.Ledger.FindAsync(id);
            if (ledger == null)
            {
                return NotFound();
            }

            _context.Ledger.Remove(ledger);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Get the ledger users
        [HttpGet("dudu")]
        public List<User> GetUserLedgers(int id)
        {
            Ledger ledger = _context.Ledger.Include(l => l.Users).FirstOrDefault(l => l.Id == id);
            return ledger.Users;
        }

        // POST: Adds a user to a ledger
        [HttpPost("dudu2")]
        public async Task<ActionResult<Ledger>> UserToLedger(int ledgerId, int userId)
        {
            Ledger ledger = _context.Ledger.Include(l => l.Users).FirstOrDefault(l => l.Id == ledgerId);
            ledger.Users.Add(_context.User.Find(userId));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLedger", new { id = ledger.Id }, ledger);
        }



        private bool LedgerExists(int id)
        {
            return _context.Ledger.Any(e => e.Id == id);
        }
    }
}
