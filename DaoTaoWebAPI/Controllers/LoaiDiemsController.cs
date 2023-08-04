
namespace DaoTaoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiDiemsController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IExistName _existTenLD;

        public LoaiDiemsController(ApiDbContext context, IExistName existTenLD)
        {
            _context = context;
            _existTenLD = existTenLD;
        }

        // GET: api/LoaiDiems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoaiDiem>>> GetLoaiDiems()
        {
          if (_context.LoaiDiems == null)
          {
              return NotFound();
          }
            return await _context.LoaiDiems.ToListAsync();
        }

        // GET: api/LoaiDiems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoaiDiem>> GetLoaiDiem(string id)
        {
          if (_context.LoaiDiems == null)
          {
              return NotFound();
          }
            var loaiDiem = await _context.LoaiDiems.FindAsync(id);

            if (loaiDiem == null)
            {
                return NotFound();
            }

            return loaiDiem;
        }

        // PUT: api/LoaiDiems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoaiDiem(string id, LoaiDiem loaiDiem)
        {
            //if (id != loaiDiem.MaLDiem)
            //{
            //    return BadRequest();
            //}
            var existingTenLD = _context.LoaiDiems.FirstOrDefault(x => x.MaLDiem == loaiDiem.MaLDiem);

            if (existingTenLD == null)
            {
                return BadRequest(); // Không tìm thấy chức vụ để cập nhật
            }

            if (existingTenLD.TenLDiem != loaiDiem.TenLDiem && _context.LoaiDiems.Any(x => x.TenLDiem == loaiDiem.TenLDiem))
            {
                return BadRequest("Tên loại điểm này đã tồn tại! Vui lòng nhập lại!");
            }
            _context.LoaiDiems.Remove(existingTenLD);
            _context.Entry(loaiDiem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoaiDiemExists(id))
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

        // POST: api/LoaiDiems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoaiDiem>> PostLoaiDiem(LoaiDiem loaiDiem)
        {
          if (_context.LoaiDiems == null)
          {
              return Problem("Entity set 'ApiDbContext.LoaiDiems'  is null.");
          }
            _context.LoaiDiems.Add(loaiDiem);
            try
            {
                if (_existTenLD.IsTenLDiemUnique(loaiDiem.TenLDiem))
                {
                    return BadRequest("Tên loại điểm này đã tồn tại! Vui lòng nhập lại!");
                }
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LoaiDiemExists(loaiDiem.MaLDiem))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLoaiDiem", new { id = loaiDiem.MaLDiem }, loaiDiem);
        }

        // DELETE: api/LoaiDiems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoaiDiem(string id)
        {
            if (_context.LoaiDiems == null)
            {
                return NotFound();
            }
            var loaiDiem = await _context.LoaiDiems.FindAsync(id);
            if (loaiDiem == null)
            {
                return NotFound();
            }

            _context.LoaiDiems.Remove(loaiDiem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoaiDiemExists(string id)
        {
            return (_context.LoaiDiems?.Any(e => e.MaLDiem == id)).GetValueOrDefault();
        }
    }
}
