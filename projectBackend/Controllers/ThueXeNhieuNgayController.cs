using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectBackend.Models;
using projectBackend.DTOs;

namespace projectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThueXeNhieuNgayController : ControllerBase
    {
        private readonly ThuexemayContext _context;

        public ThueXeNhieuNgayController(ThuexemayContext context)
        {
            _context = context;
        }

        // GET: api/ThueXeNhieuNgay
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThueXeTheoNgayDto>>> GetAll()
        {
            var theoNgay = await _context.ThueXeNhieuNgays
                .ToListAsync();
            var list = theoNgay
                .Select(t => new ThueXeTheoNgayDto
                {
                    MaThue = t.MaThue,
                    MaKhachHang = t.MaKhachHang ?? 0,
                    MaXe = t.MaXe ?? 0,
                    MaNhanVien = t.MaNhanVien ?? 0,
                    NgayBatDau = (DateOnly)t.NgayBatDau,
                    NgayKetThuc = (DateOnly)t.NgayKetThuc
                });

            return Ok(list);
        }

        // GET: api/ThueXeNhieuNgay/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ThueXeTheoNgayDto>> GetById(int id)
        {
            var theoNgay = await _context.ThueXeNhieuNgays
                .Where(t => t.MaThue == id)
                .FirstOrDefaultAsync();
            if (theoNgay == null)
                return NotFound();
            var item = new ThueXeTheoNgayDto
                {
                    MaThue = theoNgay.MaThue,
                    MaKhachHang = theoNgay.MaKhachHang ?? 0,
                    MaXe = theoNgay.MaXe ?? 0,
                    MaNhanVien = theoNgay.MaNhanVien ?? 0,
                    NgayBatDau = (DateOnly)theoNgay.NgayBatDau,
                    NgayKetThuc = (DateOnly)theoNgay.NgayKetThuc
            };

            return Ok(item);
        }

        // POST: api/ThueXeNhieuNgay
        [HttpPost]
        public async Task<ActionResult<ThueXeNhieuNgay>> Create(ThueXeNhieuNgay model)
        {
            if (model == null)
                return BadRequest();

            _context.ThueXeNhieuNgays.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = model.MaThue }, model);
        }

        // PUT: api/ThueXeNhieuNgay/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ThueXeNhieuNgay model)
        {
            if (model == null || id != model.MaThue)
                return BadRequest();

            var exists = await _context.ThueXeNhieuNgays.AnyAsync(t => t.MaThue == id);
            if (!exists)
                return NotFound();

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.ThueXeNhieuNgays.AnyAsync(e => e.MaThue == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/ThueXeNhieuNgay/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.ThueXeNhieuNgays.FindAsync(id);
            if (entity == null)
                return NotFound();

            _context.ThueXeNhieuNgays.Remove(entity);
            await _context.SaveChangesAsync();

            // return a success notification
            return Ok(new { message = "Xóa thành công" });
        }
    }
}
