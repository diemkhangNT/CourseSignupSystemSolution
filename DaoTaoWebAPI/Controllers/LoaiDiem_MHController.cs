using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaoTaoWebAPI.Data;
using DaoTaoWebAPI.Models;

namespace DaoTaoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiDiem_MHController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public LoaiDiem_MHController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/LoaiDiem_MH
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoaiDiem_MH>>> GetLoaiDiem_MHs()
        {
          if (_context.LoaiDiem_MHs == null)
          {
              return NotFound();
          }
            return await _context.LoaiDiem_MHs.ToListAsync();
        }

        // GET: api/LoaiDiem_MH/5
        [HttpGet("{monhocid}/{loaidiemid}")]
        public async Task<ActionResult<LoaiDiem_MH>> GetLoaiDiem_MH(string monhocid, string loaidiemid)
        {
          if (_context.LoaiDiem_MHs == null)
          {
              return NotFound();
          }
            var loaiDiem_MH = await _context.LoaiDiem_MHs.FindAsync(monhocid, loaidiemid);

            if (loaiDiem_MH == null)
            {
                return NotFound();
            }

            return loaiDiem_MH;
        }

        // PUT: api/LoaiDiem_MH/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{monhocid}/{loaidiemid}")]
        public async Task<IActionResult> PutLoaiDiem_MH(string monhocid, string loaidiemid, LoaiDiem_MH loaiDiem_MH)
        {
            if (monhocid != loaiDiem_MH.MaMH || loaidiemid != loaiDiem_MH.MaLDiem)
            {
                return BadRequest();
            }

            _context.Entry(loaiDiem_MH).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoaiDiem_MHExists(monhocid, loaidiemid))
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

        // POST: api/LoaiDiem_MH
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoaiDiem_MH>> PostLoaiDiem_MH(LoaiDiem_MH loaiDiem_MH)
        {
          if (_context.LoaiDiem_MHs == null)
          {
              return Problem("Entity set 'ApiDbContext.LoaiDiem_MHs'  is null.");
          }
            _context.LoaiDiem_MHs.Add(loaiDiem_MH);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LoaiDiem_MHExists(loaiDiem_MH.MaMH, loaiDiem_MH.MaLDiem))
                {
                    return Conflict("đã lưu");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLoaiDiem_MH", new { id = loaiDiem_MH.MaMH }, loaiDiem_MH);
        }

        // DELETE: api/LoaiDiem_MH/5
        [HttpDelete("{monhocid}/{loaidiemid}")]
        public async Task<IActionResult> DeleteLoaiDiem_MH(string monhocid, string loaidiemid )
        {
            if (_context.LoaiDiem_MHs == null)
            {
                return NotFound();
            }
            var loaiDiem_MH = await _context.LoaiDiem_MHs.FindAsync(monhocid, loaidiemid);
            if (loaiDiem_MH == null)
            {
                return NotFound();
            }

            _context.LoaiDiem_MHs.Remove(loaiDiem_MH);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoaiDiem_MHExists(string monhocid, string loaidiemid)
        {
            return (_context.LoaiDiem_MHs?.Any(e => e.MaMH == monhocid && e.MaLDiem == loaidiemid)).GetValueOrDefault();
        }
    }
}
