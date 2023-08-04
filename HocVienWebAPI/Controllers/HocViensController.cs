using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HocVienWebAPI.Data;
using HocVienWebAPI.Interfaces;
using HocVienWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HocVienWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocViensController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IIndexValid _indexValid;

        public HocViensController(ApiDbContext context, IIndexValid indexValid)
        {
            _context = context;
            _indexValid = indexValid;
        }

        // GET: api/HocViens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HocVien>>> GetHocViens()
        {
          if (_context.HocViens == null)
          {
              return NotFound();
          }
            return await _context.HocViens.ToListAsync();
        }

        // GET: api/HocViens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HocVien>> GetHocVien(string id)
        {
          if (_context.HocViens == null)
          {
              return NotFound();
          }
            var hocVien = await _context.HocViens.FindAsync(id);

            if (hocVien == null)
            {
                return NotFound();
            }

            return hocVien;
        }

        // PUT: api/HocViens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHocVien(string id, HocVien hocVien)
        {
            //if (id != hocVien.MaHV)
            //{
            //    return BadRequest();
            //}
            var existingHocVien = _context.HocViens.FirstOrDefault(x => x.MaHV == hocVien.MaHV);

            if (existingHocVien == null)
            {
                return BadRequest(); // Không tìm thấy chức vụ để cập nhật
            }

            if (existingHocVien.MaHV != hocVien.MaHV && _context.HocViens.Any(x => x.Email == hocVien.Email))
            {
                return BadRequest("Email này đã được sử dụng. Vui lòng nhập email khác!"); // TenCV mới trùng với các TenCV khác
            }
            _context.HocViens.Remove(existingHocVien);

            _context.Entry(hocVien).State = EntityState.Modified;

            try
            {
                if (!_indexValid.IsNumber(hocVien.SDTLienLac))
                {
                    return BadRequest("Số điện thoại không hợp lệ!");
                }
                if(!_indexValid.IsPassword(hocVien.Password))
                {
                    return BadRequest("Độ dài mật khẩu (ít nhất 8 ký tự).");
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HocVienExists(id))
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

        // POST: api/HocViens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HocVien>> PostHocVien(HocVien hocVien)
        {
          if (_context.HocViens == null)
          {
              return Problem("Entity set 'ApiDbContext.HocViens'  is null.");
          }
            _context.HocViens.Add(hocVien);
            try
            {
                if (_indexValid.IsEmailUnique(hocVien.Email))
                {
                    return BadRequest("Email này đã tồn tại!");
                }
                if (!_indexValid.IsNumber(hocVien.SDTLienLac))
                {
                    return BadRequest("Số điện thoại không hợp lệ!");
                }
                if (!_indexValid.IsPassword(hocVien.Password))
                {
                    return BadRequest("Độ dài mật khẩu (ít nhất 8 ký tự).");
                }
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HocVienExists(hocVien.MaHV))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHocVien", new { id = hocVien.MaHV }, hocVien);
        }

        // DELETE: api/HocViens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHocVien(string id)
        {
            if (_context.HocViens == null)
            {
                return NotFound();
            }
            var hocVien = await _context.HocViens.FindAsync(id);
            if (hocVien == null)
            {
                return NotFound();
            }

            _context.HocViens.Remove(hocVien);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HocVienExists(string id)
        {
            return (_context.HocViens?.Any(e => e.MaHV == id)).GetValueOrDefault();
        }
    }
}
