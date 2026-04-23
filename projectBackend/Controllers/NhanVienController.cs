using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectBackend.DTO;

using projectBackend.Models;
using static projectBackend.DTO.NhanVienDAO;

namespace projectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly ThuexemayContext _dbContext;
        public NhanVienController(ThuexemayContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadNhanVienDTO>>> GetNhanVien()
        {
            var nhanvien = await _dbContext.NhanViens
                .Select(a => new ReadNhanVienDTO()
                {
                    MaNhanVien = a.MaNhanVien,
                    DiaChi = a.DiaChi,
                    HoTen = a.HoTen,
                    SoDienThoai = a.SoDienThoai,
                })
                .ToListAsync();

            if (nhanvien == null || nhanvien.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(nhanvien);
            }
        }

        //get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadNhanVienDTO>> GetNhanVienById(int id)
        {
            double total = 0;
            var doanhthu = await _dbContext.DoanhThus.Where(a => a.MaNhanVien == id).ToListAsync();
            foreach (var item in doanhthu)
            {
                total += (double)item.SoTien;
            }
            var nhanvien = await _dbContext.NhanViens
                .Select(a => new ReadNhanVienDTO()
                {
                    MaNhanVien = a.MaNhanVien,
                    DiaChi = a.DiaChi,
                    HoTen = a.HoTen,
                    SoDienThoai = a.SoDienThoai,
                    DoanhThu = total,
                })
                .FirstOrDefaultAsync(a => a.MaNhanVien == id);

            if (nhanvien == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(nhanvien);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReadNhanVienDTO>> CreateNhanVien([FromBody] ReadNhanVienDTO nhanvien)
        {
            var nvExist = _dbContext.NhanViens.Any(a => a.MaNhanVien == nhanvien.MaNhanVien);
            if (nvExist) { return Conflict(); }
            else
            {
                try
                {
                    var nv = new NhanVien()
                    {
                        MaNhanVien = nhanvien.MaNhanVien,
                        DiaChi = nhanvien.DiaChi,
                        HoTen = nhanvien.HoTen,
                        SoDienThoai = nhanvien.SoDienThoai,
                    };
                    _dbContext.NhanViens.Add(nv);
                    await _dbContext.SaveChangesAsync();
                    var url = Url.Link("GetAccountById", new { id = nhanvien.MaNhanVien });

                    var readAccount = new ReadNhanVienDTO()
                    {
                        MaNhanVien = nhanvien.MaNhanVien,
                        DiaChi = nhanvien.DiaChi,
                        HoTen = nhanvien.HoTen,
                        SoDienThoai = nhanvien.SoDienThoai,
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
        public async Task<ActionResult<ReadNhanVienDTO>> UpdateNhanVien(int id, [FromBody] ReadNhanVienDTO nhanvien)
        {
            var nv = _dbContext.NhanViens.FirstOrDefault(a => a.MaNhanVien == id);

            if (nv == null) { return NotFound(); }
            else
            {
                try
                {
                    nv.DiaChi = nhanvien.DiaChi;
                    nv.HoTen = nhanvien.HoTen;
                    nv.SoDienThoai = nhanvien.SoDienThoai;

                    _dbContext.NhanViens.Update(nv);
                    await _dbContext.SaveChangesAsync();

                    var readNhanVien = new ReadNhanVienDTO()
                    {
                        MaNhanVien = nhanvien.MaNhanVien,
                        DiaChi = nhanvien.DiaChi,
                        HoTen = nhanvien.HoTen,
                        SoDienThoai = nhanvien.SoDienThoai,
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
        public async Task<ActionResult<ReadNhanVienDTO>> UpdateDelete(int id)
        {
            var nv = await _dbContext.NhanViens.FirstOrDefaultAsync(a => a.MaNhanVien == id);
            if (nv == null) { return NotFound(); }
            else
            {
                try
                {
                    _dbContext.NhanViens.Remove(nv);
                    await _dbContext.SaveChangesAsync();

                    var readNhanVien = new ReadNhanVienDTO()
                    {
                        MaNhanVien = nv.MaNhanVien,
                        DiaChi = nv.DiaChi,
                        HoTen = nv.HoTen,
                        SoDienThoai = nv.SoDienThoai,
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
