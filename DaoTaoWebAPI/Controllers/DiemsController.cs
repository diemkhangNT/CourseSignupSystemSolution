

namespace DaoTaoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiemsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public DiemsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Diems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diem>>> GetDiems()
        {
          if (_context.Diems == null)
          {
              return NotFound();
          }
            return await _context.Diems.ToListAsync();
        }

        // GET: api/Diems/5
        [HttpGet("{lophocid}/{hocvienid}")]
        public async Task<ActionResult<Diem>> GetDiem(string lophocid, string hocvienid)
        {
          if (_context.Diems == null)
          {
              return NotFound();
          }
            var diem = await _context.Diems.FindAsync(hocvienid, lophocid);

            if (diem == null)
            {
                return NotFound();
            }

            return diem;
        }

        // PUT: api/Diems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{lophocid}/{hocvienid}")]
        public async Task<IActionResult> PutDiem(string lophocid, string hocvienid, Diem diem)
        {
            if (lophocid != diem.MaLopHoc || hocvienid != diem.MaHV)
            {
                return BadRequest();
            }

            _context.Entry(diem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiemExists(lophocid, hocvienid))
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

        // POST: api/Diems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Diem>> PostDiem(Diem diem)
        {
          if (_context.Diems == null)
          {
              return Problem("Entity set 'ApiDbContext.Diems'  is null.");
          }
            _context.Diems.Add(diem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DiemExists(diem.MaLopHoc, diem.MaHV))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDiem", new { LHid = diem.MaLopHoc, HVid = diem.MaHV  }, diem);
        }

        // DELETE: api/Diems/5
        [HttpDelete("{lophocid}/{hocvienid}")]
        public async Task<IActionResult> DeleteDiem(string lophocid, string hocvienid)
        {
            if (_context.Diems == null)
            {
                return NotFound();
            }
            var diem = await _context.Diems.FindAsync(hocvienid, lophocid);
            if (diem == null)
            {
                return NotFound();
            }

            _context.Diems.Remove(diem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiemExists(string lophocid, string hocvienid)
        {
            return (_context.Diems?.Any(e => e.MaHV == hocvienid && e.MaLopHoc == lophocid)).GetValueOrDefault();
        }
    }
}
