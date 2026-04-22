using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectBackend.DTOs;
using projectBackend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThueXeTheoGioController : ControllerBase
    {
        private readonly ThuexemayContext _context;

        public ThueXeTheoGioController(ThuexemayContext context)
        {
            _context = context;
        }

        // GET: api/ThueXeTheoGio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThueXeTheoGioDto>>> GetAll()
        {
            var theoGio = await _context.ThueXeTheoGios
                .ToListAsync();
            var list = theoGio
                .Select(t => new ThueXeTheoGioDto
                {
                    MaThue = t.MaThue,
                    MaKhachHang = t.MaKhachHang ?? 0,
                    MaXe = t.MaXe ?? 0,
                    MaNhanVien = t.MaNhanVien ?? 0,
                    ThoiGianBatDau = (System.DateTime)t.ThoiGianBatDau,
                    ThoiGianKetThuc = (System.DateTime)t.ThoiGianKetThuc
                });

            return Ok(list);
        }

        // GET: api/ThueXeTheoGio/5
        [HttpGet("{id}", Name = "GetThueXeTheoGioById")]
        public async Task<ActionResult<ThueXeTheoGioDto>> GetById(int id)
        {
            var theoGio = await _context.ThueXeTheoGios
                .Where(t => t.MaThue == id)
                .FirstOrDefaultAsync();
            if (theoGio == null)
                return NotFound();
            var item = new ThueXeTheoGioDto
            {
                MaThue = theoGio.MaThue,
                MaKhachHang = theoGio.MaKhachHang ?? 0,
                MaXe = theoGio.MaXe ?? 0,
                MaNhanVien = theoGio.MaNhanVien ?? 0,
                ThoiGianBatDau = (System.DateTime)theoGio.ThoiGianBatDau,
                ThoiGianKetThuc = (System.DateTime)theoGio.ThoiGianKetThuc
            };

            return Ok(item);
        }

        // POST: api/ThueXeTheoGio
        [HttpPost]
        public async Task<ActionResult<ThueXeTheoGioDto>> Create([FromBody] ThueXeTheoGioDto model)
        {
            if (model == null)
                return BadRequest();

            var exists = await _context.ThueXeTheoGios.AnyAsync(a => a.MaThue == model.MaThue);
            if (exists)
                return Conflict();

            var entity = new ThueXeTheoGio
            {
                MaThue = model.MaThue,
                MaKhachHang = model.MaKhachHang,
                MaXe = model.MaXe,
                MaNhanVien = model.MaNhanVien,
                ThoiGianBatDau = model.ThoiGianBatDau,
                ThoiGianKetThuc = model.ThoiGianKetThuc
            };

            _context.ThueXeTheoGios.Add(entity);
            await _context.SaveChangesAsync();

            var url = Url.Link("GetThueXeTheoGioById", new { id = entity.MaThue });
            var read = new ThueXeTheoGioDto
            {
                MaThue = entity.MaThue,
                MaKhachHang = entity.MaKhachHang ?? 0,
                MaXe = entity.MaXe ?? 0,
                MaNhanVien = entity.MaNhanVien ?? 0,
                ThoiGianBatDau = (System.DateTime)entity.ThoiGianBatDau,
                ThoiGianKetThuc = (System.DateTime)entity.ThoiGianKetThuc
            };

            return Created(url, read);
        }

        // PUT: api/ThueXeTheoGio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ThueXeTheoGio model)
        {
            if (model == null || id != model.MaThue)
                return BadRequest();

            var exists = await _context.ThueXeTheoGios.AnyAsync(t => t.MaThue == id);
            if (!exists)
                return NotFound();

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.ThueXeTheoGios.AnyAsync(e => e.MaThue == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/ThueXeTheoGio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.ThueXeTheoGios.FindAsync(id);
            if (entity == null)
                return NotFound();

            _context.ThueXeTheoGios.Remove(entity);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xóa thành công" });
        }

    }
}
