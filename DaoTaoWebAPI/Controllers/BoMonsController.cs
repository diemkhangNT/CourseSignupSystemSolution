

namespace DaoTaoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoMonsController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IExistName _existTenBM;

        public BoMonsController(ApiDbContext context, IExistName existTenBM)
        {
            _context = context;
            _existTenBM = existTenBM;
        }

        // GET: api/BoMons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoMon>>> GetBoMons()
        {
          if (_context.BoMons == null)
          {
              return NotFound();
          }
            return await _context.BoMons.ToListAsync();
        }

        // GET: api/BoMons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoMon>> GetBoMon(string id)
        {
          if (_context.BoMons == null)
          {
              return NotFound();
          }
            var boMon = await _context.BoMons.FindAsync(id);

            if (boMon == null)
            {
                return NotFound();
            }

            return boMon;
        }

        // PUT: api/BoMons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoMon(string id, BoMon boMon)
        {
            var existingBoMon = _context.BoMons.FirstOrDefault(x => x.MaBM == boMon.MaBM);

            if (existingBoMon == null)
            {
                return BadRequest(); // Không tìm thấy chức vụ để cập nhật
            }

            if (existingBoMon.TenBM != boMon.TenBM && _context.BoMons.Any(x => x.TenBM == boMon.TenBM))
            {
                return BadRequest("Ten bộ môn mới trùng với các tên bộ môn khác");
            }
            _context.BoMons.Remove(existingBoMon);

            _context.Entry(boMon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoMonExists(id))
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

        // POST: api/BoMons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BoMon>> PostBoMon(BoMon boMon)
        {
          if (_context.BoMons == null)
          {
              return Problem("Entity set 'ApiDbContext.BoMons'  is null.");
          }
            _context.BoMons.Add(boMon);
            try
            {
                if (_existTenBM.IsTenBMUnique(boMon.TenBM))
                {
                    return BadRequest("Tên bộ môn này đã tồn tại!");
                }
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BoMonExists(boMon.MaBM))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBoMon", new { id = boMon.MaBM }, boMon);
        }

        // DELETE: api/BoMons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoMon(string id)
        {
            if (_context.BoMons == null)
            {
                return NotFound();
            }
            var boMon = await _context.BoMons.FindAsync(id);
            if (boMon == null)
            {
                return NotFound();
            }

            _context.BoMons.Remove(boMon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoMonExists(string id)
        {
            return (_context.BoMons?.Any(e => e.MaBM == id)).GetValueOrDefault();
        }
    }
}
