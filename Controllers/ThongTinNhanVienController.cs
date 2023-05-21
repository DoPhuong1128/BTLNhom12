using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTL_Nhom12.Data;
using BTL_Nhom12.Models;

namespace BTL_Nhom12.Controllers
{
    public class ThongTinNhanVienController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThongTinNhanVienController(ApplicationDbContext context)
        {
            _context = context;
        }

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

        private bool ThongTinNhanVienExists(string id)
        {
          return (_context.ThongTinNhanVien?.Any(e => e.MaNhanVien == id)).GetValueOrDefault();
        }
    }
}
