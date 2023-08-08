using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhanQuyenWedAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanQuyenWedAPI.Data;
using PhanQuyenWedAPI.Models;

namespace PhanQuyenWedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanViensController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IExistName _exist;

        public NhanViensController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/NhanViens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NhanVien>>> GetNhanViens()
        {
          if (_context.NhanViens == null)
          {
              return NotFound();
          }
            return await _context.NhanViens.ToListAsync();
        }

        // GET: api/NhanViens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NhanVien>> GetNhanVien(string id)
        {
          if (_context.NhanViens == null)
          {
              return NotFound();
          }
            var nhanVien = await _context.NhanViens.FindAsync(id);

            if (nhanVien == null)
            {
                return NotFound();
            }

            return nhanVien;
        }

        // PUT: api/NhanViens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNhanVien(string id, NhanVien nhanVien)
        {
            //if (id != nhanVien.MaNV)
            //{
            //    return BadRequest();
            //}
            var existing = _context.NhanViens.FirstOrDefault(x => x.MaNV == nhanVien.MaNV);

            if (existing == null)
            {
                return BadRequest();
            }

            if (existing.MaNV != nhanVien.MaNV && _context.NhanViens.Any(x => x.Email == nhanVien.Email))
            {
                return BadRequest("Email này đã được sử dụng. Vui lòng nhập email khác!");
            }
            if (!_exist.IsNumberCMND(nhanVien.CMND))
            {
                return BadRequest("CCCD không hợp lệ");
            }
            else if (existing.MaNV != nhanVien.MaNV && _context.NhanViens.Any(x => x.CMND == nhanVien.CMND))
            {
                return BadRequest("CCCD này đã được sử dụng. Vui lòng nhập CCCD khác!");
            }
            if (!_exist.IsNumber(nhanVien.SDT))
            {
                return BadRequest("Số điện thoại không hợp lệ!");
            }
            if (!_exist.IsPassword(nhanVien.Password))
            {
                return BadRequest("Độ dài mật khẩu (ít nhất 8 ký tự).");
            }
            _context.NhanViens.Remove(existing);
            _context.Entry(nhanVien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhanVienExists(id))
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

        // POST: api/NhanViens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NhanVien>> PostNhanVien(NhanVien nhanVien)
        {
          if (_context.NhanViens == null)
          {
              return Problem("Entity set 'ApiDbContext.NhanViens'  is null.");
          }
            if (_exist.IsEmailUnique(nhanVien.Email))
            {
                return BadRequest("Email này đã tồn tại! Vui lòng nhập email chưa đăng ký tài khoản!");
            }
            if (!_exist.IsNumberCMND(nhanVien.CMND))
            {
                return BadRequest("CCCD không hợp lệ");
            }
            else if (_exist.IsCMNDUnique(nhanVien.CMND))
            {
                return BadRequest("CMND này đã tồn tại!");
            }
            if (!_exist.IsNumber(nhanVien.SDT))
            {
                return BadRequest("Số điện thoại không hợp lệ!");
            }
            if (!_exist.IsPassword(nhanVien.Password))
            {
                return BadRequest("Độ dài mật khẩu (ít nhất 8 ký tự).");
            }
            _context.NhanViens.Add(nhanVien);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NhanVienExists(nhanVien.MaNV))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNhanVien", new { id = nhanVien.MaNV }, nhanVien);
        }

        // DELETE: api/NhanViens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNhanVien(string id)
        {
            if (_context.NhanViens == null)
            {
                return NotFound();
            }
            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            _context.NhanViens.Remove(nhanVien);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NhanVienExists(string id)
        {
            return (_context.NhanViens?.Any(e => e.MaNV == id)).GetValueOrDefault();
        }
    }
}
