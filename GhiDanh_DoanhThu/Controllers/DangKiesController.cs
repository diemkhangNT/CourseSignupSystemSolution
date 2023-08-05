using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseSignupSystemServer.Models;
using GhiDanh_DoanhThu.Data;

namespace GhiDanh_DoanhThu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DangKiesController : ControllerBase
    {
        private readonly APIDBContext _context;

        public DangKiesController(APIDBContext context)
        {
            _context = context;
        }

        // GET: api/DangKies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DangKy>>> GetDangKies()
        {
          if (_context.DangKies == null)
          {
              return NotFound();
          }
            return await _context.DangKies.ToListAsync();
        }

        // GET: api/DangKies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DangKy>> GetDangKy(string id)
        {
          if (_context.DangKies == null)
          {
              return NotFound();
          }
            var dangKy = await _context.DangKies.FindAsync(id);

            if (dangKy == null)
            {
                return NotFound();
            }

            return dangKy;
        }

        // PUT: api/DangKies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDangKy(string id, DangKy dangKy)
        {
            if (id != dangKy.MaDK)
            {
                return BadRequest();
            }

            _context.Entry(dangKy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DangKyExists(id))
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

        // POST: api/DangKies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DangKy>> PostDangKy(DangKy dangKy)
        {
          if (_context.DangKies == null)
          {
              return Problem("Entity set 'APIDBContext.DangKies'  is null.");
          }
            _context.DangKies.Add(dangKy);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DangKyExists(dangKy.MaDK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDangKy", new { id = dangKy.MaDK }, dangKy);
        }

        // DELETE: api/DangKies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDangKy(string id)
        {
            if (_context.DangKies == null)
            {
                return NotFound();
            }
            var dangKy = await _context.DangKies.FindAsync(id);
            if (dangKy == null)
            {
                return NotFound();
            }

            _context.DangKies.Remove(dangKy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DangKyExists(string id)
        {
            return (_context.DangKies?.Any(e => e.MaDK == id)).GetValueOrDefault();
        }
    }
}
