using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanQuyenWedAPI.Data;
using PhanQuyenWedAPI.Interfaces;
using PhanQuyenWedAPI.Models;

namespace PhanQuyenWedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaiTroesController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IExistName _existTenVT;
        public VaiTroesController(ApiDbContext context, IExistName existTenVT)
        {
            _context = context;
            _existTenVT = existTenVT;
        }

        // GET: api/VaiTroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VaiTro>>> GetVaiTros()
        {
          if (_context.VaiTros == null)
          {
              return NotFound();
          }
            return await _context.VaiTros.ToListAsync();
        }

        // GET: api/VaiTroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VaiTro>> GetVaiTro(string id)
        {
          if (_context.VaiTros == null)
          {
              return NotFound();
          }
            var vaiTro = await _context.VaiTros.FindAsync(id);

            if (vaiTro == null)
            {
                return NotFound();
            }

            return vaiTro;
        }

        // PUT: api/VaiTroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVaiTro(string id, VaiTro vaiTro)
        {
            //if (id != vaiTro.MaVT)
            //{
            //    return BadRequest();
            //}
            var existingVT = _context.VaiTros.FirstOrDefault(x => x.MaVT == vaiTro.MaVT);

            if (existingVT == null)
            {
                return BadRequest(); // Không tìm thấy vai trò để cập nhật
            }

            if (existingVT.TenVT != vaiTro.TenVT && _context.VaiTros.Any(x => x.TenVT == vaiTro.TenVT))
            {
                return BadRequest("Ten vai trò mới trùng với các tên bộ môn khác");
            }
            _context.VaiTros.Remove(existingVT);
            _context.Entry(vaiTro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaiTroExists(id))
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

        // POST: api/VaiTroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VaiTro>> PostVaiTro(VaiTro vaiTro)
        {
          if (_context.VaiTros == null)
          {
              return Problem("Entity set 'ApiDbContext.VaiTros'  is null.");
          }
            _context.VaiTros.Add(vaiTro);
            try
            {
                if (_existTenVT.IsTenVT(vaiTro.TenVT))
                {
                    return BadRequest("Tên vai trò này đã tồn tại!");
                }
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VaiTroExists(vaiTro.MaVT))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVaiTro", new { id = vaiTro.MaVT }, vaiTro);
        }

        // DELETE: api/VaiTroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaiTro(string id)
        {
            if (_context.VaiTros == null)
            {
                return NotFound();
            }
            var vaiTro = await _context.VaiTros.FindAsync(id);
            if (vaiTro == null)
            {
                return NotFound();
            }

            _context.VaiTros.Remove(vaiTro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VaiTroExists(string id)
        {
            return (_context.VaiTros?.Any(e => e.MaVT == id)).GetValueOrDefault();
        }
    }
}
