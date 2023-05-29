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
    public class SanphamController : Controller
    {
        private readonly ApplicationDbContext _context;
         StringProcess strPro = new StringProcess();
        //private ExcelProcess strPro = new ExcelProcess();

        public SanphamController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sanpham
        public async Task<IActionResult> Index()
        {
              return _context.Sanpham != null ? 
                          View(await _context.Sanpham.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Sanpham'  is null.");
        }

        // GET: Sanpham/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Sanpham == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanpham
                .FirstOrDefaultAsync(m => m.MaSanPham == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return View(sanpham);
        }

        // GET: Sanpham/Create
        public IActionResult Create()
        {
            var newID = "";
            if (_context.Sanpham.Count() == 0)
            {
                //khoi tao 1 ma moi
                newID = "SP001";
            }
            else
            {
                var id = _context.Sanpham.OrderByDescending(m => m.MaSanPham).First().MaSanPham;
                newID = strPro.AutoGenerateKey(id);
            }
            ViewBag.MaSanPham = newID;
            return View();
        }

        // POST: Sanpham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSanPham,TenSanPham,DVT,NSX,Kichco,Color,GiaTien,Soluongton")] Sanpham sanpham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanpham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sanpham);
        }

        // GET: Sanpham/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Sanpham == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanpham.FindAsync(id);
            if (sanpham == null)
            {
                return NotFound();
            }
            return View(sanpham);
        }

        // POST: Sanpham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSanPham,TenSanPham,DVT,NSX,Kichco,Color,GiaTien,Soluongton")] Sanpham sanpham)
        {
            if (id != sanpham.MaSanPham)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanpham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanphamExists(sanpham.MaSanPham))
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
            return View(sanpham);
        }

        // GET: Sanpham/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Sanpham == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanpham
                .FirstOrDefaultAsync(m => m.MaSanPham == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return View(sanpham);
        }

        // POST: Sanpham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Sanpham == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Sanpham'  is null.");
            }
            var sanpham = await _context.Sanpham.FindAsync(id);
            if (sanpham != null)
            {
                _context.Sanpham.Remove(sanpham);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanphamExists(string id)
        {
          return (_context.Sanpham?.Any(e => e.MaSanPham == id)).GetValueOrDefault();
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
                            var std = new Sanpham();

                            std.MaSanPham= dt.Rows[i][0].ToString();
                            std.TenSanPham= dt.Rows[i][1].ToString();
                            std.DVT = dt.Rows[i][2].ToString();
                            std.Kichco = dt.Rows[i][3].ToString();
                            std.Color = dt.Rows[i][4].ToString();
                            std.GiaTien = dt.Rows[i][5].ToString();
                            std.Soluongton = dt.Rows[i][6].ToString();

                            _context.Sanpham.Add(std);
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
