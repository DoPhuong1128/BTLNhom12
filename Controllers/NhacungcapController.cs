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
    public class NhacungcapController : Controller
    {
        private readonly ApplicationDbContext _context;
         StringProcess strPro = new StringProcess();
        //private ExcelProcess strPro = new ExcelProcess();

        public NhacungcapController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Nhacungcap
        public async Task<IActionResult> Index()
        {
              return _context.Nhacungcap != null ? 
                          View(await _context.Nhacungcap.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Nhacungcap'  is null.");
        }

        // GET: Nhacungcap/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Nhacungcap == null)
            {
                return NotFound();
            }

            var nhacungcap = await _context.Nhacungcap
                .FirstOrDefaultAsync(m => m.MaNCC == id);
            if (nhacungcap == null)
            {
                return NotFound();
            }

            return View(nhacungcap);
        }

        // GET: Nhacungcap/Create
        public IActionResult Create()
        {
            var newID = "";
            if (_context.Nhacungcap.Count() == 0)
            {
                //khoi tao 1 ma moi
                newID = "NCC001";
            }
            else
            {
                var id = _context.Nhacungcap.OrderByDescending(m => m.MaNCC).First().MaNCC;
                newID = strPro.AutoGenerateKey(id);
            }
            ViewBag.MaNCC = newID;
            return View();
        }

        // POST: Nhacungcap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNCC,TenNCC,SDTNCC,DiaChi,Email")] Nhacungcap nhacungcap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhacungcap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhacungcap);
        }

        // GET: Nhacungcap/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Nhacungcap == null)
            {
                return NotFound();
            }

            var nhacungcap = await _context.Nhacungcap.FindAsync(id);
            if (nhacungcap == null)
            {
                return NotFound();
            }
            return View(nhacungcap);
        }

        // POST: Nhacungcap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaNCC,TenNCC,SDTNCC,DiaChi,Email")] Nhacungcap nhacungcap)
        {
            if (id != nhacungcap.MaNCC)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhacungcap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhacungcapExists(nhacungcap.MaNCC))
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
            return View(nhacungcap);
        }

        // GET: Nhacungcap/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Nhacungcap == null)
            {
                return NotFound();
            }

            var nhacungcap = await _context.Nhacungcap
                .FirstOrDefaultAsync(m => m.MaNCC == id);
            if (nhacungcap == null)
            {
                return NotFound();
            }

            return View(nhacungcap);
        }

        // POST: Nhacungcap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Nhacungcap == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Nhacungcap'  is null.");
            }
            var nhacungcap = await _context.Nhacungcap.FindAsync(id);
            if (nhacungcap != null)
            {
                _context.Nhacungcap.Remove(nhacungcap);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhacungcapExists(string id)
        {
          return (_context.Nhacungcap?.Any(e => e.MaNCC == id)).GetValueOrDefault();
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
                            var std = new Nhacungcap();

                            std.MaNCC= dt.Rows[i][0].ToString();
                            std.TenNCC= dt.Rows[i][1].ToString();
                            std.SDTNCC = dt.Rows[i][2].ToString();
                            std.DiaChi = dt.Rows[i][3].ToString();
                            std.Email = dt.Rows[i][4].ToString();
                            

                            _context.Nhacungcap.Add(std);
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
