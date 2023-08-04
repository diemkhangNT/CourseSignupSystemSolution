using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HocVienWebAPI.Data;
using HocVienWebAPI.Models;

namespace HocVienWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LienHesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public LienHesController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/LienHes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LienHe>>> GetLienHes()
        {
          if (_context.LienHes == null)
          {
              return NotFound();
          }
            return await _context.LienHes.ToListAsync();
        }

        // GET: api/LienHes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LienHe>> GetLienHe(string id)
        {
          if (_context.LienHes == null)
          {
              return NotFound();
          }
            var lienHe = await _context.LienHes.FindAsync(id);

            if (lienHe == null)
            {
                return NotFound();
            }

            return lienHe;
        }

        // PUT: api/LienHes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLienHe(string id, LienHe lienHe)
        {
            if (id != lienHe.MaLH)
            {
                return BadRequest();
            }

            _context.Entry(lienHe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LienHeExists(id))
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

        // POST: api/LienHes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LienHe>> PostLienHe(LienHe lienHe)
        {
          if (_context.LienHes == null)
          {
              return Problem("Entity set 'ApiDbContext.LienHes'  is null.");
          }
            _context.LienHes.Add(lienHe);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LienHeExists(lienHe.MaLH))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLienHe", new { id = lienHe.MaLH }, lienHe);
        }

        // DELETE: api/LienHes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLienHe(string id)
        {
            if (_context.LienHes == null)
            {
                return NotFound();
            }
            var lienHe = await _context.LienHes.FindAsync(id);
            if (lienHe == null)
            {
                return NotFound();
            }

            _context.LienHes.Remove(lienHe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LienHeExists(string id)
        {
            return (_context.LienHes?.Any(e => e.MaLH == id)).GetValueOrDefault();
        }
    }
}
