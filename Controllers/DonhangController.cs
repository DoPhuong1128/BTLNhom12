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
    public class DonhangController : Controller
    {
        private readonly ApplicationDbContext _context;
         StringProcess strPro = new StringProcess();
        //private ExcelProcess strPro = new ExcelProcess();

        public DonhangController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Donhang
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Donhang.Include(d => d.Khachhang);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Donhang/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Donhang == null)
            {
                return NotFound();
            }

            var donhang = await _context.Donhang
                .Include(d => d.Khachhang)
                .FirstOrDefaultAsync(m => m.Madonhang == id);
            if (donhang == null)
            {
                return NotFound();
            }

            return View(donhang);
        }

        // GET: Donhang/Create
        public IActionResult Create()
        {
            ViewData["Makhachhang"] = new SelectList(_context.Khachhang, "Makhachhang", "Makhachhang");
            return View();
        }

        // POST: Donhang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Madonhang,NgayBan,MaSanPham,Sanpham,Makhachhang")] Donhang donhang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donhang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Makhachhang"] = new SelectList(_context.Khachhang, "Makhachhang", "Makhachhang", donhang.Makhachhang);
            return View(donhang);
        }

        // GET: Donhang/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Donhang == null)
            {
                return NotFound();
            }

            var donhang = await _context.Donhang.FindAsync(id);
            if (donhang == null)
            {
                return NotFound();
            }
            ViewData["Makhachhang"] = new SelectList(_context.Khachhang, "Makhachhang", "Makhachhang", donhang.Makhachhang);
            return View(donhang);
        }

        // POST: Donhang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Madonhang,NgayBan,MaSanPham,Sanpham,Makhachhang")] Donhang donhang)
        {
            if (id != donhang.Madonhang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donhang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonhangExists(donhang.Madonhang))
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
            ViewData["Makhachhang"] = new SelectList(_context.Khachhang, "Makhachhang", "Makhachhang", donhang.Makhachhang);
            return View(donhang);
        }

        // GET: Donhang/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Donhang == null)
            {
                return NotFound();
            }

            var donhang = await _context.Donhang
                .Include(d => d.Khachhang)
                .FirstOrDefaultAsync(m => m.Madonhang == id);
            if (donhang == null)
            {
                return NotFound();
            }

            return View(donhang);
        }

        // POST: Donhang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Donhang == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Donhang'  is null.");
            }
            var donhang = await _context.Donhang.FindAsync(id);
            if (donhang != null)
            {
                _context.Donhang.Remove(donhang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonhangExists(string id)
        {
          return (_context.Donhang?.Any(e => e.Madonhang == id)).GetValueOrDefault();
        }
    }
}
