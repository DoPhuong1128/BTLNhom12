using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTL_Nhom12.Data;
using BTL_Nhom12.Models;
using BTL_Nhom12.Models.Process;

namespace BTL_Nhom12.Controllers
{
    public class ThongTinNhanVienController : Controller
    {
        // khaibaps DbContext để làm việc với database
        private readonly ApplicationDbContext _context;
        StringProcess strPro = new StringProcess();
        //private ExcelProcess strPro = new ExcelProcess();
        public ThongTinNhanVienController(ApplicationDbContext context)
        {
            _context = context;
        }
        //Trả về view index danh sach thongtinnhanvien

        // GET: ThongTinNhanVien
        public async Task<IActionResult> Index()
        {
              return _context.ThongTinNhanVien != null ? 
                          View(await _context.ThongTinNhanVien.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ThongTinNhanVien'  is null.");
        }

        // GET: ThongTinNhanVien/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ThongTinNhanVien == null)
            {
                return NotFound();
            }

            var thongTinNhanVien = await _context.ThongTinNhanVien
                .FirstOrDefaultAsync(m => m.MaNhanVien == id);
            if (thongTinNhanVien == null)
            {
                return NotFound();
            }

            return View(thongTinNhanVien);
        }

        // GET: ThongTinNhanVien/Create
        public IActionResult Create()
        {
            var newID = "";
            if (_context.ThongTinNhanVien.Count() == 0)
            {
                //khoi tao 1 ma moi
                newID = "WORKER001";
            }
            else
            {
                var id = _context.ThongTinNhanVien.OrderByDescending(m => m.MaNhanVien).First().MaNhanVien;
                newID = strPro.AutoGenerateKey(id);
            }
            ViewBag.MaNhanVien = newID;
            return View();
        }

        // POST: ThongTinNhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNhanVien,TenNhanVien,GioiTinhNhanVien,DiaChi,Sdt")] ThongTinNhanVien thongTinNhanVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thongTinNhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(thongTinNhanVien);
        }

        // GET: ThongTinNhanVien/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ThongTinNhanVien == null)
            {
                return NotFound();
            }

            var thongTinNhanVien = await _context.ThongTinNhanVien.FindAsync(id);
            if (thongTinNhanVien == null)
            {
                return NotFound();
            }
            return View(thongTinNhanVien);
        }

        // POST: ThongTinNhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaNhanVien,TenNhanVien,GioiTinhNhanVien,DiaChi,Sdt")] ThongTinNhanVien thongTinNhanVien)
        {
            if (id != thongTinNhanVien.MaNhanVien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thongTinNhanVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThongTinNhanVienExists(thongTinNhanVien.MaNhanVien))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(thongTinNhanVien);
        }

        // GET: ThongTinNhanVien/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ThongTinNhanVien == null)
            {
                return NotFound();
            }

            var thongTinNhanVien = await _context.ThongTinNhanVien
                .FirstOrDefaultAsync(m => m.MaNhanVien == id);
            if (thongTinNhanVien == null)
            {
                return NotFound();
            }

            return View(thongTinNhanVien);
        }

        // POST: ThongTinNhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ThongTinNhanVien == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ThongTinNhanVien'  is null.");
            }
            var thongTinNhanVien = await _context.ThongTinNhanVien.FindAsync(id);
            if (thongTinNhanVien != null)
            {
                _context.ThongTinNhanVien.Remove(thongTinNhanVien);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //Kiểm tra xem id có tồn tại không

        private bool ThongTinNhanVienExists(string id)
        {
          return (_context.ThongTinNhanVien?.Any(e => e.MaNhanVien == id)).GetValueOrDefault();
        }
        
        //upload

        //Tạo action Upload file excel lên server
        private ExcelProcess _excelProcess = new ExcelProcess();
        public Task<IActionResult> Upload()
        {
            return Task.FromResult<IActionResult>(View());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file!=null)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            if (fileExtension!=".xls"&& fileExtension !=".xlsx")
            {
                ModelState.AddModelError("","Please choose excel file to upload!");
            }
            else
            {
                //rename file when upload to server
                var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels",fileName);
                var FileLocation = new FileInfo(filePath).ToString();
                using (var stream = new FileStream(filePath,FileMode.Create))
                {
                    //save file to server
                    await file.CopyToAsync(stream);
                    var dt = _excelProcess.ExcelToDataTable(FileLocation);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var std = new ThongTinNhanVien();

                            std.MaNhanVien = dt.Rows[i][0].ToString();
                            std.TenNhanVien= dt.Rows[i][1].ToString();
                            std.GioiTinhNhanVien = dt.Rows[i][2].ToString();
                            std.DiaChi = dt.Rows[i][3].ToString();
                            std.Sdt = dt.Rows[i][4].ToString();

                            _context.ThongTinNhanVien.Add(std);
                        }
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                }
            }
        }
        return View();
        } 
    }
}
