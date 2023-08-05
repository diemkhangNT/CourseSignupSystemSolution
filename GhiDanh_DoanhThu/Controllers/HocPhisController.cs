using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GhiDanh_DoanhThu.Data;
using GhiDanh_DoanhThu.Models;

namespace GhiDanh_DoanhThu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocPhisController : ControllerBase
    {
        private readonly APIDBContext _context;

        public HocPhisController(APIDBContext context)
        {
            _context = context;
        }

        // GET: api/HocPhis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HocPhi>>> GetHocPhis()
        {
          if (_context.HocPhis == null)
          {
              return NotFound();
          }
            return await _context.HocPhis.ToListAsync();
        }

        // GET: api/HocPhis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HocPhi>> GetHocPhi(string id)
        {
          if (_context.HocPhis == null)
          {
              return NotFound();
          }
            var hocPhi = await _context.HocPhis.FindAsync(id);

            if (hocPhi == null)
            {
                return NotFound();
            }

            return hocPhi;
        }

        // PUT: api/HocPhis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHocPhi(string id, HocPhi hocPhi)
        {
            if (id != hocPhi.MaHP)
            {
                return BadRequest();
            }

            _context.Entry(hocPhi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HocPhiExists(id))
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

        // POST: api/HocPhis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HocPhi>> PostHocPhi(HocPhi hocPhi)
        {
          if (_context.HocPhis == null)
          {
              return Problem("Entity set 'APIDBContext.HocPhis'  is null.");
          }
            _context.HocPhis.Add(hocPhi);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HocPhiExists(hocPhi.MaHP))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHocPhi", new { id = hocPhi.MaHP }, hocPhi);
        }

        // DELETE: api/HocPhis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHocPhi(string id)
        {
            if (_context.HocPhis == null)
            {
                return NotFound();
            }
            var hocPhi = await _context.HocPhis.FindAsync(id);
            if (hocPhi == null)
            {
                return NotFound();
            }

            _context.HocPhis.Remove(hocPhi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HocPhiExists(string id)
        {
            return (_context.HocPhis?.Any(e => e.MaHP == id)).GetValueOrDefault();
        }
    }
}
