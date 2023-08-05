using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LichNghiWebAPI.Data;
using LichNghiWebAPI.Models;

namespace LichNghiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichNghisController : ControllerBase
    {
        private readonly APIDbContext _context;

        public LichNghisController(APIDbContext context)
        {
            _context = context;
        }

        // GET: api/LichNghis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LichNghi>>> GetLichNghis()
        {
          if (_context.LichNghis == null)
          {
              return NotFound();
          }
            return await _context.LichNghis.ToListAsync();
        }

        // GET: api/LichNghis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LichNghi>> GetLichNghi(string id)
        {
          if (_context.LichNghis == null)
          {
              return NotFound();
          }
            var lichNghi = await _context.LichNghis.FindAsync(id);

            if (lichNghi == null)
            {
                return NotFound();
            }

            return lichNghi;
        }

        // PUT: api/LichNghis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLichNghi(string id, LichNghi lichNghi)
        {
            if (id != lichNghi.MaLN)
            {
                return BadRequest();
            }

            _context.Entry(lichNghi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LichNghiExists(id))
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

        // POST: api/LichNghis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LichNghi>> PostLichNghi(LichNghi lichNghi)
        {
          if (_context.LichNghis == null)
          {
              return Problem("Entity set 'APIDbContext.LichNghis'  is null.");
          }
            _context.LichNghis.Add(lichNghi);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LichNghiExists(lichNghi.MaLN))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLichNghi", new { id = lichNghi.MaLN }, lichNghi);
        }

        // DELETE: api/LichNghis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLichNghi(string id)
        {
            if (_context.LichNghis == null)
            {
                return NotFound();
            }
            var lichNghi = await _context.LichNghis.FindAsync(id);
            if (lichNghi == null)
            {
                return NotFound();
            }

            _context.LichNghis.Remove(lichNghi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LichNghiExists(string id)
        {
            return (_context.LichNghis?.Any(e => e.MaLN == id)).GetValueOrDefault();
        }
    }
}
