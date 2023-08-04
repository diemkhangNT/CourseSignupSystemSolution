

namespace DaoTaoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NienKhoasController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IExistName _existTGNK;

        public NienKhoasController(ApiDbContext context, IExistName existTGNK)
        {
            _context = context;
            _existTGNK = existTGNK;
        }

        // GET: api/NienKhoas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NienKhoa>>> GetNienKhoas()
        {
          if (_context.NienKhoas == null)
          {
              return NotFound();
          }
            return await _context.NienKhoas.ToListAsync();
        }

        // GET: api/NienKhoas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NienKhoa>> GetNienKhoa(string id)
        {
          if (_context.NienKhoas == null)
          {
              return NotFound();
          }
            var nienKhoa = await _context.NienKhoas.FindAsync(id);

            if (nienKhoa == null)
            {
                return NotFound();
            }

            return nienKhoa;
        }

        // PUT: api/NienKhoas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNienKhoa(string id, NienKhoa nienKhoa)
        {
            //if (id != nienKhoa.MaNK)
            //{
            //    return BadRequest();
            //}
            var existing = _context.NienKhoas.FirstOrDefault(x => x.MaNK == nienKhoa.MaNK);

            if (existing == null)
            {
                return BadRequest();
            }

            if (existing.MaNK != nienKhoa.MaNK && _context.NienKhoas.Any(x => x.ThoiGian == nienKhoa.ThoiGian))
            {
                return BadRequest("Thời gian này đã tồn tại. Vui lòng nhập vào khoảng thời gian khác!");
            }
            _context.NienKhoas.Remove(existing);
            _context.Entry(nienKhoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NienKhoaExists(id))
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

        // POST: api/NienKhoas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NienKhoa>> PostNienKhoa(NienKhoa nienKhoa)
        {
          if (_context.NienKhoas == null)
          {
              return Problem("Entity set 'ApiDbContext.NienKhoas'  is null.");
          }
            _context.NienKhoas.Add(nienKhoa);
            try
            {
                if (_existTGNK.IsThoiGiannkUnique(nienKhoa.ThoiGian))
                {
                    return BadRequest("Niên khóa với thời gian này đã tồn tại!");
                }
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NienKhoaExists(nienKhoa.MaNK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNienKhoa", new { id = nienKhoa.MaNK }, nienKhoa);
        }

        // DELETE: api/NienKhoas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNienKhoa(string id)
        {
            if (_context.NienKhoas == null)
            {
                return NotFound();
            }
            var nienKhoa = await _context.NienKhoas.FindAsync(id);
            if (nienKhoa == null)
            {
                return NotFound();
            }

            _context.NienKhoas.Remove(nienKhoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NienKhoaExists(string id)
        {
            return (_context.NienKhoas?.Any(e => e.MaNK == id)).GetValueOrDefault();
        }
    }
}
