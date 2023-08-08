using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanQuyenWedAPI.Data;
using PhanQuyenWedAPI.Models;

namespace PhanQuyenWedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public NguoiDungsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/NguoiDungs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NguoiDung>>> GetNguoiDungs()
        {
          if (_context.NguoiDungs == null)
          {
              return NotFound();
          }
            return await _context.NguoiDungs.ToListAsync();
        }

        // GET: api/NguoiDungs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NguoiDung>> GetNguoiDung(string id)
        {
          if (_context.NguoiDungs == null)
          {
              return NotFound();
          }
            var nguoiDung = await _context.NguoiDungs.FindAsync(id);

            if (nguoiDung == null)
            {
                return NotFound();
            }

            return nguoiDung;
        }

        // PUT: api/NguoiDungs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNguoiDung(string id, NguoiDung nguoiDung)
        {
            if (id != nguoiDung.UserId)
            {
                return BadRequest();
            }

            _context.Entry(nguoiDung).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NguoiDungExists(id))
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

        // POST: api/NguoiDungs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NguoiDung>> PostNguoiDung(NguoiDung nguoiDung)
        {
          if (_context.NguoiDungs == null)
          {
              return Problem("Entity set 'ApiDbContext.NguoiDungs'  is null.");
          }
            _context.NguoiDungs.Add(nguoiDung);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NguoiDungExists(nguoiDung.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNguoiDung", new { id = nguoiDung.UserId }, nguoiDung);
        }

        // DELETE: api/NguoiDungs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNguoiDung(string id)
        {
            if (_context.NguoiDungs == null)
            {
                return NotFound();
            }
            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            _context.NguoiDungs.Remove(nguoiDung);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NguoiDungExists(string id)
        {
            return (_context.NguoiDungs?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
