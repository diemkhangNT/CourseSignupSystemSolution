
namespace GiangVienWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiangViensController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IExistName _exist;

        public GiangViensController(ApiDbContext context, IExistName exist)
        {
            _context = context;
            _exist = exist;
        }

        // GET: api/GiangViens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiangVien>>> GetGiangViens()
        {
          if (_context.GiangViens == null)
          {
              return NotFound();
          }
            return await _context.GiangViens.ToListAsync();
        }

        // GET: api/GiangViens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GiangVien>> GetGiangVien(string id)
        {
          if (_context.GiangViens == null)
          {
              return NotFound();
          }
            var giangVien = await _context.GiangViens.FindAsync(id);

            if (giangVien == null)
            {
                return NotFound();
            }

            return giangVien;
        }

        // PUT: api/GiangViens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGiangVien(string id, GiangVien giangVien)
        {
            //if (id != giangVien.MaGV)
            //{
            //    return BadRequest();
            //}
            var existing = _context.GiangViens.FirstOrDefault(x => x.MaGV == giangVien.MaGV);

            if (existing == null)
            {
                return BadRequest();
            }

            if (existing.MaGV != giangVien.MaGV && _context.GiangViens.Any(x => x.Email == giangVien.Email))
            {
                return BadRequest("Email này đã được sử dụng. Vui lòng nhập email khác!");
            }
            if (!_exist.IsNumberCMND(giangVien.CMND))
            {
                return BadRequest("CCCD không hợp lệ");
            }
            else if (existing.MaGV != giangVien.MaGV && _context.GiangViens.Any(x => x.CMND == giangVien.CMND))
            {
                return BadRequest("CCCD này đã được sử dụng. Vui lòng nhập CCCD khác!");
            }
            if (!_exist.IsNumber(giangVien.SDTLienLac))
            {
                return BadRequest("Số điện thoại không hợp lệ!");
            }
            if (!_exist.IsPassword(giangVien.Password))
            {
                return BadRequest("Độ dài mật khẩu (ít nhất 8 ký tự).");
            }
            _context.GiangViens.Remove(existing);
            _context.Entry(giangVien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GiangVienExists(id))
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

        // POST: api/GiangViens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GiangVien>> PostGiangVien(GiangVien giangVien)
        {
              if (_context.GiangViens == null)
              {
                  return Problem("Entity set 'ApiDbContext.GiangViens'  is null.");
              }
            if (_exist.IsEmailGVUnique(giangVien.Email))
            {
                return BadRequest("Email này đã tồn tại! Vui lòng nhập email chưa đăng ký tài khoản!");
            }
            if (!_exist.IsNumberCMND(giangVien.CMND))
            {
                return BadRequest("CCCD không hợp lệ");
            }
            else if (_exist.IsCMNDgvUnique(giangVien.CMND))
            {
                return BadRequest("CMND này đã tồn tại!");
            }
            if (!_exist.IsNumber(giangVien.SDTLienLac))
            {
                return BadRequest("Số điện thoại không hợp lệ!");
            }
            if (!_exist.IsPassword(giangVien.Password))
            {
                return BadRequest("Độ dài mật khẩu (ít nhất 8 ký tự).");
            }
            _context.GiangViens.Add(giangVien);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GiangVienExists(giangVien.MaGV))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGiangVien", new { id = giangVien.MaGV }, giangVien);
        }

        // DELETE: api/GiangViens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGiangVien(string id)
        {
            if (_context.GiangViens == null)
            {
                return NotFound();
            }
            var giangVien = await _context.GiangViens.FindAsync(id);
            if (giangVien == null)
            {
                return NotFound();
            }

            _context.GiangViens.Remove(giangVien);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GiangVienExists(string id)
        {
            return (_context.GiangViens?.Any(e => e.MaGV == id)).GetValueOrDefault();
        }
    }
}
