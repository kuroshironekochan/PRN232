using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectBackend.DTO;
using projectBackend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {

        private readonly ThuexemayContext _dbContext;
        public KhachHangController(ThuexemayContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadKhachHangDTO>>> GetKhachHang()
        {
            var khachhang = await _dbContext.KhachHangs
                .Select(a => new ReadKhachHangDTO()
                {
                    MaKhachHang = a.MaKhachHang,
                    DiaChi = a.DiaChi,
                    HoTen = a.HoTen,
                    SoDienThoai = a.SoDienThoai,
                })
                .ToListAsync();

            if (khachhang == null || khachhang.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(khachhang);
            }
        }

        //get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadKhachHangDTO>> GetKhachHangById(int id)
        {
            var khachhang = await _dbContext.KhachHangs
                .Select(a => new ReadKhachHangDTO()
                {
                    MaKhachHang = a.MaKhachHang,
                    DiaChi = a.DiaChi,
                    HoTen = a.HoTen,
                    SoDienThoai = a.SoDienThoai,
                })
                .FirstOrDefaultAsync(a => a.MaKhachHang == id);

            if (khachhang == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(khachhang);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReadKhachHangDTO>> CreateKhachHang([FromBody] ReadKhachHangDTO khachhang)
        {
            var nvExist = _dbContext.KhachHangs.Any(a => a.MaKhachHang == khachhang.MaKhachHang);
            if (nvExist) { return Conflict(); }
            else
            {
                try
                {
                    var kh = new KhachHang()
                    {
                        MaKhachHang = khachhang.MaKhachHang,
                        DiaChi = khachhang.DiaChi,
                        HoTen = khachhang.HoTen,
                        SoDienThoai = khachhang.SoDienThoai,
                    };
                    _dbContext.KhachHangs.Add(kh);
                    await _dbContext.SaveChangesAsync();
                    var url = Url.Link("GetAccountById", new { id = khachhang.MaKhachHang });

                    var readAccount = new ReadKhachHangDTO()
                    {
                        MaKhachHang = khachhang.MaKhachHang,
                        DiaChi = khachhang.DiaChi,
                        HoTen = khachhang.HoTen,
                        SoDienThoai = khachhang.SoDienThoai,
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
        public async Task<ActionResult<ReadKhachHangDTO>> UpdateKhachHang(int id, [FromBody] ReadKhachHangDTO khachhang)
        {
            var kh = _dbContext.KhachHangs.FirstOrDefault(a => a.MaKhachHang == id);
            if (kh == null) { return NotFound(); }
            else
            {
                try
                {
                    kh.DiaChi = khachhang.DiaChi;
                    kh.HoTen = khachhang.HoTen;
                    kh.SoDienThoai = khachhang.SoDienThoai;

                    _dbContext.KhachHangs.Update(kh);
                    await _dbContext.SaveChangesAsync();

                    var readNhanVien = new ReadKhachHangDTO()
                    {
                        MaKhachHang = khachhang.MaKhachHang,
                        DiaChi = khachhang.DiaChi,
                        HoTen = khachhang.HoTen,
                        SoDienThoai = khachhang.SoDienThoai,
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
