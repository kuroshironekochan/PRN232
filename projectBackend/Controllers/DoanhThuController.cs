using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectBackend.DTO;
using projectBackend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoanhThuController : ControllerBase
    {
        private readonly ThuexemayContext _context;

        public DoanhThuController(ThuexemayContext context)
        {
            _context = context;
        }

        // GET: api/DoanhThu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoanhThuDto>>> GetAll()
        {
            var items = await _context.DoanhThus.ToListAsync();
            var list = items.Select(d => new DoanhThuDto
            {
                MaHoaDon = d.MaHoaDon,
                MaKhachHang = d.MaKhachHang ?? 0,
                MaXe = d.MaXe ?? 0,
                MaNhanVien = d.MaNhanVien ?? 0,
                NgayThanhToan = d.NgayThanhToan ?? System.DateOnly.MinValue,
                SoTien = d.SoTien ?? 0m
            });

            return Ok(list);
        }

        // GET: api/DoanhThu/5
        [HttpGet("{id}", Name = "GetDoanhThuById")]
        public async Task<ActionResult<DoanhThuDto>> GetById(int id)
        {
            var d = await _context.DoanhThus
                .Where(x => x.MaHoaDon == id)
                .FirstOrDefaultAsync();
            if (d == null)
                return NotFound();

            var dto = new DoanhThuDto
            {
                MaHoaDon = d.MaHoaDon,
                MaKhachHang = d.MaKhachHang ?? 0,
                MaXe = d.MaXe ?? 0,
                MaNhanVien = d.MaNhanVien ?? 0,
                NgayThanhToan = d.NgayThanhToan ?? System.DateOnly.MinValue,
                SoTien = d.SoTien ?? 0m
            };

            return Ok(dto);
        }

        // POST: api/DoanhThu
        [HttpPost]
        public async Task<ActionResult<DoanhThuDto>> Create([FromBody] DoanhThuDto model)
        {
            if (model == null)
                return BadRequest();

            var exists = await _context.DoanhThus.AnyAsync(a => a.MaHoaDon == model.MaHoaDon);
            if (exists)
                return Conflict();

            var entity = new DoanhThu
            {
                MaHoaDon = model.MaHoaDon,
                MaKhachHang = model.MaKhachHang,
                MaXe = model.MaXe,
                MaNhanVien = model.MaNhanVien,
                NgayThanhToan = model.NgayThanhToan,
                SoTien = model.SoTien
            };

            _context.DoanhThus.Add(entity);
            await _context.SaveChangesAsync();

            var url = Url.Link("GetDoanhThuById", new { id = entity.MaHoaDon });
            var read = new DoanhThuDto
            {
                MaHoaDon = entity.MaHoaDon,
                MaKhachHang = entity.MaKhachHang ?? 0,
                MaXe = entity.MaXe ?? 0,
                MaNhanVien = entity.MaNhanVien ?? 0,
                NgayThanhToan = entity.NgayThanhToan ?? System.DateOnly.MinValue,
                SoTien = entity.SoTien ?? 0m
            };

            return Created(url, read);
        }

        // PUT: api/DoanhThu/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DoanhThu model)
        {
            if (model == null || id != model.MaHoaDon)
                return BadRequest();

            var exists = await _context.DoanhThus.AnyAsync(d => d.MaHoaDon == id);
            if (!exists)
                return NotFound();

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.DoanhThus.AnyAsync(e => e.MaHoaDon == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/DoanhThu/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var entity = await _context.DoanhThus.FindAsync(id);
        //    if (entity == null)
        //        return NotFound();

        //    _context.DoanhThus.Remove(entity);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { message = "Deleted successfully" });
        //}
    }
}
