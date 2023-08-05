using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiangVienWebAPI.Data;
using GiangVienWebAPI.Models;
using System.Security.Cryptography;

namespace GiangVienWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanCongGiangDaysController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public PhanCongGiangDaysController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/PhanCongGiangDays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhanCongGiangDay>>> GetPhanCongGiangDay()
        {
          if (_context.PhanCongGiangDay == null)
          {
              return NotFound();
          }
            return await _context.PhanCongGiangDay.ToListAsync();
        }

        // GET: api/PhanCongGiangDays/5
        [HttpGet("{gvid}/{lhid}")]
        public async Task<ActionResult<PhanCongGiangDay>> GetPhanCongGiangDay(string gvid, string lhid)
        {
          if (_context.PhanCongGiangDay == null)
          {
              return NotFound();
          }
            var phanCongGiangDay = await _context.PhanCongGiangDay.FindAsync(gvid, lhid);

            if (phanCongGiangDay == null)
            {
                return NotFound();
            }

            return phanCongGiangDay;
        }

        // PUT: api/PhanCongGiangDays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{gvid}/{lhid}")]
        public async Task<IActionResult> PutPhanCongGiangDay(string gvid, string lhid, PhanCongGiangDay phanCongGiangDay)
        {
            if (lhid != phanCongGiangDay.MaLop || gvid != phanCongGiangDay.MaGV)
            {
                return BadRequest();
            }

            _context.Entry(phanCongGiangDay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhanCongGiangDayExists(gvid, lhid))
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

        // POST: api/PhanCongGiangDays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhanCongGiangDay>> PostPhanCongGiangDay(PhanCongGiangDay phanCongGiangDay)
        {
          if (_context.PhanCongGiangDay == null)
          {
              return Problem("Entity set 'ApiDbContext.PhanCongGiangDay'  is null.");
          }
            _context.PhanCongGiangDay.Add(phanCongGiangDay);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PhanCongGiangDayExists(phanCongGiangDay.MaGV, phanCongGiangDay.MaLop))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPhanCongGiangDay", new { gvid = phanCongGiangDay.MaGV, lhid = phanCongGiangDay.MaLop }, phanCongGiangDay);
        }

        // DELETE: api/PhanCongGiangDays/5
        [HttpDelete("{gvid}/{lhid}")]
        public async Task<IActionResult> DeletePhanCongGiangDay(string gvid, string lhid)
        {
            if (_context.PhanCongGiangDay == null)
            {
                return NotFound();
            }
            var phanCongGiangDay = await _context.PhanCongGiangDay.FindAsync(gvid, lhid);
            if (phanCongGiangDay == null)
            {
                return NotFound();
            }

            _context.PhanCongGiangDay.Remove(phanCongGiangDay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhanCongGiangDayExists(string gvid, string lhid)
        {
            return (_context.PhanCongGiangDay?.Any(e => e.MaLop == lhid && e.MaGV == gvid)).GetValueOrDefault();
        }
    }
}
