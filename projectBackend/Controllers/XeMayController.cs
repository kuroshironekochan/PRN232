using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectBackend.DTO;
using projectBackend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XeMayController : ControllerBase
    {
        private readonly ThuexemayContext _dbContext;
        public XeMayController(ThuexemayContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadXeMayDTO>>> GetNhanVien()
        {
            var xemay = await _dbContext.XeMays
                .Select(a => new ReadXeMayDTO()
                {
                    MaXe = a.MaXe,
                    GiaThu = a.GiaThu,
                    HangXe = a.HangXe,
                    TenXe = a.TenXe,
                })
                .ToListAsync();

            if (xemay == null || xemay.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(xemay);
            }
        }

        //get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadXeMayDTO>> GetNhanVienById(int id)
        {
            var xemay = await _dbContext.XeMays
                .Select(a => new ReadXeMayDTO()
                {
                    MaXe = a.MaXe,
                    GiaThu = a.GiaThu,
                    HangXe = a.HangXe,
                    TenXe = a.TenXe,
                })
                .FirstOrDefaultAsync(a => a.MaXe == id);

            if (xemay == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(xemay);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReadXeMayDTO>> CreateNhanVien([FromBody] ReadXeMayDTO xemay)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nvExist = _dbContext.XeMays.Any(a => a.MaXe == xemay.MaXe);
            if (nvExist) { return Conflict(); }
            else
            {
                try
                {
                    var xe = new XeMay()
                    {
                        MaXe = xemay.MaXe,
                        GiaThu = xemay.GiaThu,
                        HangXe = xemay.HangXe,
                        TenXe = xemay.TenXe,
                    };
                    _dbContext.XeMays.Add(xe);
                    await _dbContext.SaveChangesAsync();
                    var url = Url.Link("GetAccountById", new { id = xemay.MaXe });

                    var readAccount = new ReadXeMayDTO()
                    {
                        MaXe = xe.MaXe,
                        GiaThu = xe.GiaThu,
                        HangXe = xe.HangXe,
                        TenXe = xe.TenXe,
                    };
                    return Created(url, readAccount);
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReadXeMayDTO>> UpdateNhanVien(int id, [FromBody] ReadXeMayDTO xemay)
        {
            var xe = _dbContext.XeMays.FirstOrDefault(a => a.MaXe == id);
            if (xe == null) { return NotFound(); }
            else
            {
                try
                {
                    xe.TenXe = xemay.TenXe;
                    xe.HangXe = xemay.HangXe;
                    xe.GiaThu = xemay.GiaThu;

                    _dbContext.XeMays.Update(xe);
                    await _dbContext.SaveChangesAsync();

                    var readNhanVien = new ReadXeMayDTO()
                    {
                        MaXe = xe.MaXe,
                        GiaThu = xe.GiaThu,
                        HangXe = xe.HangXe,
                        TenXe = xe.TenXe,
                    };
                    return Ok(readNhanVien);
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ReadXeMayDTO>> UpdateDelete(int id)
        {
            var xe = await _dbContext.XeMays.FirstOrDefaultAsync(a => a.MaXe == id);
            if (xe == null) { return NotFound(); }
            else
            {
                try
                {
                    _dbContext.XeMays.Remove(xe);
                    await _dbContext.SaveChangesAsync();

                    var readNhanVien = new XeMay()
                    {
                        MaXe = xe.MaXe,
                        GiaThu = xe.GiaThu,
                        HangXe = xe.HangXe,
                        TenXe = xe.TenXe,
                    };
                    return Ok(readNhanVien);
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }
    }
}
