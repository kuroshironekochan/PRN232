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
                    ThoiGianBatDau = (DateTime)t.ThoiGianBatDau,
                    ThoiGianKetThuc = (DateTime)t.ThoiGianKetThuc
                });

            return Ok(list);
        }

        // GET: api/ThueXeTheoGio/5
        [HttpGet("{id}")]
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
                ThoiGianBatDau = (DateTime)theoGio.ThoiGianBatDau,
                ThoiGianKetThuc = (DateTime)theoGio.ThoiGianKetThuc
            };

            return Ok(item);
        }

        // POST: api/ThueXeTheoGio
        [HttpPost]
        public async Task<ActionResult<ThueXeTheoGio>> Create(ThueXeTheoGio model)
        {
            if (model == null)
                return BadRequest();

            _context.ThueXeTheoGios.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = model.MaThue }, model);
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
