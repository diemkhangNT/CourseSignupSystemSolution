

namespace DaoTaoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LopHocsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public LopHocsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/LopHocs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LopHoc>>> GetLopHocs()
        {
          if (_context.LopHocs == null)
          {
              return NotFound();
          }
            return await _context.LopHocs.ToListAsync();
        }

        // GET: api/LopHocs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LopHoc>> GetLopHoc(string id)
        {
          if (_context.LopHocs == null)
          {
              return NotFound();
          }
            var lopHoc = await _context.LopHocs.FindAsync(id);

            if (lopHoc == null)
            {
                return NotFound();
            }

            return lopHoc;
        }

        // PUT: api/LopHocs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLopHoc(string id, LopHoc lopHoc)
        {
            if (id != lopHoc.MaLop)
            {
                return BadRequest();
            }

            _context.Entry(lopHoc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LopHocExists(id))
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

        // POST: api/LopHocs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LopHoc>> PostLopHoc(LopHoc lopHoc)
        {
          if (_context.LopHocs == null)
          {
              return Problem("Entity set 'ApiDbContext.LopHocs'  is null.");
          }
            _context.LopHocs.Add(lopHoc);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LopHocExists(lopHoc.MaLop))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLopHoc", new { id = lopHoc.MaLop }, lopHoc);
        }

        // DELETE: api/LopHocs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLopHoc(string id)
        {
            if (_context.LopHocs == null)
            {
                return NotFound();
            }
            var lopHoc = await _context.LopHocs.FindAsync(id);
            if (lopHoc == null)
            {
                return NotFound();
            }

            _context.LopHocs.Remove(lopHoc);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LopHocExists(string id)
        {
            return (_context.LopHocs?.Any(e => e.MaLop == id)).GetValueOrDefault();
        }
    }
}
