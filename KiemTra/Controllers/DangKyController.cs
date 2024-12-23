using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KiemTra.Data;
using KiemTra.Models;
using Microsoft.AspNetCore.Authorization;

namespace KiemTra.Controllers
{
    [Authorize]
    public class DangKyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DangKyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DangKy
        public async Task<IActionResult> Index()
        {
            var hocPhans = await _context.HocPhan.ToListAsync();
            return View(hocPhans);
        }

        // POST: DangKy/DangKyHocPhan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangKyHocPhan(string maHP)
        {
            var hocPhan = await _context.HocPhan.FindAsync(maHP);
            if (hocPhan == null)
            {
                return NotFound();
            }

            var maSV = User.Identity?.Name;
            if (string.IsNullOrEmpty(maSV))
            {
                return Unauthorized();
            }

            var existingDangKy = await _context.DangKy
                .FirstOrDefaultAsync(d => d.MaSV == maSV);

            if (existingDangKy == null)
            {
                existingDangKy = new DangKy
                {
                    MaSV = maSV,
                    NgayDK = DateTime.Now
                };
                _context.DangKy.Add(existingDangKy);
                await _context.SaveChangesAsync();
            }

            var existingChiTiet = await _context.ChiTietDangKy
                .FirstOrDefaultAsync(c => c.MaDK == existingDangKy.MaDK && c.MaHP == maHP);

            if (existingChiTiet != null)
            {
                TempData["ErrorMessage"] = "Bạn đã đăng ký học phần này rồi!";
                return RedirectToAction(nameof(Index));
            }

            if (hocPhan.SoLuongDaDangKy >= hocPhan.SoLuongDuKien)
            {
                TempData["ErrorMessage"] = "Học phần này đã hết slot đăng ký!";
                return RedirectToAction(nameof(Index));
            }

            var chiTietDangKy = new ChiTietDangKy
            {
                MaDK = existingDangKy.MaDK,
                MaHP = maHP
            };

            _context.ChiTietDangKy.Add(chiTietDangKy);
            hocPhan.SoLuongDaDangKy++;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(DanhSachDaDangKy));
        }

        // GET: DangKy/DanhSachDaDangKy
        public async Task<IActionResult> DanhSachDaDangKy()
        {
            var maSV = User.Identity?.Name;
            if (string.IsNullOrEmpty(maSV))
            {
                return Unauthorized();
            }

            var dangKy = await _context.DangKy
                .Include(d => d.ChiTietDangKys)
                .ThenInclude(c => c.HocPhan)
                .FirstOrDefaultAsync(d => d.MaSV == maSV);

            if (dangKy == null)
            {
                return View(new List<DangKy>());
            }

            return View(new List<DangKy> { dangKy });
        }

        // POST: DangKy/XoaHocPhan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XoaHocPhan(int maDK, string maHP)
        {
            var chiTietDangKy = await _context.ChiTietDangKy
                .Include(c => c.HocPhan)
                .FirstOrDefaultAsync(c => c.MaDK == maDK && c.MaHP == maHP);

            if (chiTietDangKy == null)
            {
                return NotFound();
            }

            var maSV = User.Identity?.Name;
            if (string.IsNullOrEmpty(maSV))
            {
                return Unauthorized();
            }

            var dangKy = await _context.DangKy.FindAsync(maDK);
            if (dangKy?.MaSV != maSV)
            {
                return Forbid();
            }

            if (chiTietDangKy.HocPhan != null)
            {
                chiTietDangKy.HocPhan.SoLuongDaDangKy--;
            }

            _context.ChiTietDangKy.Remove(chiTietDangKy);

            var remainingCourses = await _context.ChiTietDangKy
                .Where(c => c.MaDK == maDK)
                .CountAsync();

            if (remainingCourses == 0)
            {
                _context.DangKy.Remove(dangKy);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(DanhSachDaDangKy));
        }

        // POST: DangKy/ThongTinDangKy
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThongTinDangKy()
        {
            var maSV = User.Identity?.Name;
            if (string.IsNullOrEmpty(maSV))
            {
                return Unauthorized();
            }

            var sinhVien = await _context.SinhVien
                .Include(s => s.NganhHoc)
                .FirstOrDefaultAsync(s => s.MaSV == maSV);

            if (sinhVien == null)
            {
                return NotFound();
            }

            var dangKy = await _context.DangKy
                .Include(d => d.ChiTietDangKys)
                .ThenInclude(c => c.HocPhan)
                .FirstOrDefaultAsync(d => d.MaSV == maSV);

            if (dangKy == null)
            {
                return NotFound();
            }

            ViewBag.SinhVien = sinhVien;
            return View(dangKy);
        }

        // POST: DangKy/LuuDangKy
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LuuDangKy()
        {
            var maSV = User.Identity?.Name;
            if (string.IsNullOrEmpty(maSV))
            {
                return Unauthorized();
            }

            var sinhVien = await _context.SinhVien
                .Include(s => s.NganhHoc)
                .FirstOrDefaultAsync(s => s.MaSV == maSV);

            if (sinhVien == null)
            {
                return NotFound();
            }

            TempData["MaSV"] = maSV;
            TempData["HoTen"] = sinhVien.HoTen;
            TempData["NgayDK"] = DateTime.Now;
            TempData["NganhHoc"] = sinhVien.NganhHoc?.TenNganh ?? "CNTT";

            return RedirectToAction(nameof(XacNhanDangKy));
        }

        // GET: DangKy/XacNhanDangKy
        public IActionResult XacNhanDangKy()
        {
            if (TempData["MaSV"] == null)
            {
                return RedirectToAction(nameof(DanhSachDaDangKy));
            }

            return View();
        }
    }
}

