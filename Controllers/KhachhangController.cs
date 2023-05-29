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
    public class KhachhangController : Controller
    {
        private readonly ApplicationDbContext _context;
         StringProcess strPro = new StringProcess();
        //private ExcelProcess strPro = new ExcelProcess();

        public KhachhangController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Khachhang
        public async Task<IActionResult> Index()
        {
              return _context.Khachhang != null ? 
                          View(await _context.Khachhang.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Khachhang'  is null.");
        }

        // GET: Khachhang/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Khachhang == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhang
                .FirstOrDefaultAsync(m => m.Makhachhang == id);
            if (khachhang == null)
            {
                return NotFound();
            }

            return View(khachhang);
        }

        // GET: Khachhang/Create
        public IActionResult Create()
        {
            var newID = "";
            if (_context.Khachhang.Count() == 0)
            {
                //khoi tao 1 ma moi
                newID = "KH001";
            }
            else
            {
                var id = _context.Khachhang.OrderByDescending(m => m.Makhachhang).First().Makhachhang;
                newID = strPro.AutoGenerateKey(id);
            }
            ViewBag.Makhachhang = newID;
            return View();
        }

        // POST: Khachhang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Makhachhang,Tenkhachhang,Diachi,Sodienthoai")] Khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khachhang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khachhang);
        }

        // GET: Khachhang/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Khachhang == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhang.FindAsync(id);
            if (khachhang == null)
            {
                return NotFound();
            }
            return View(khachhang);
        }

        // POST: Khachhang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Makhachhang,Tenkhachhang,Diachi,Sodienthoai")] Khachhang khachhang)
        {
            if (id != khachhang.Makhachhang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachhang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachhangExists(khachhang.Makhachhang))
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
            return View(khachhang);
        }

        // GET: Khachhang/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Khachhang == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhang
                .FirstOrDefaultAsync(m => m.Makhachhang == id);
            if (khachhang == null)
            {
                return NotFound();
            }

            return View(khachhang);
        }

        // POST: Khachhang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Khachhang == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Khachhang'  is null.");
            }
            var khachhang = await _context.Khachhang.FindAsync(id);
            if (khachhang != null)
            {
                _context.Khachhang.Remove(khachhang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhachhangExists(string id)
        {
          return (_context.Khachhang?.Any(e => e.Makhachhang == id)).GetValueOrDefault();
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
                            var std = new Khachhang();

                            std.Makhachhang= dt.Rows[i][0].ToString();
                            std.Tenkhachhang= dt.Rows[i][1].ToString();
                            std.Diachi = dt.Rows[i][2].ToString();
                            std.Sodienthoai = dt.Rows[i][3].ToString();
                            

                            _context.Khachhang.Add(std);
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
